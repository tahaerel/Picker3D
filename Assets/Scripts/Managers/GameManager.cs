using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    //This code was created for features to be added in the future. It is currently used only for time and event control.

    public enum GameState { Start, Play, Wait, Lose, Win };

    [SerializeField] private GameState gameState;

    public event Action<GameState> OnGameStateChange;

    private void Start()
    {
        GameStart();
    }

    public bool IsStarting() => gameState == GameState.Start;

    public void GameStart()
    {
        SetGameState(GameState.Start);
        Time.timeScale = 0;
    }

    public void GameWait()
    {
        SetGameState(GameState.Wait);
    }
    public void GamePlaying()
    {
        SetGameState(GameState.Play);
        Time.timeScale = 1;
    }
    public void GameWon()
    {
        SetGameState(GameState.Win);
        Time.timeScale = 0;
    }
    public void GameOver()
    {
        SetGameState(GameState.Lose);
        Time.timeScale = 0;
    }
    private void SetGameState(GameState newState)
    {
        gameState = newState;
        OnGameStateChange?.Invoke(gameState);
    }

}