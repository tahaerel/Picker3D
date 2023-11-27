using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject levelUI;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI nextLevelText;

    [SerializeField] private Image[] stageImages;

    public static Action levelFailed;

    private int currentLevel = 1;
    private int currentStage = 0;

    private Color orangeColor;

    void Start()
    {
       if (GameManager.Instance.IsStarting())
        {

        startPanel.SetActive(true);
        currentLevel = PlayerPrefs.GetInt("Level", 1);
        orangeColor = new Color(0.9333333f, 0.4078431f, 0.1137255f);

        SetLevelTexts();

        levelFailed += LevelFailed;

        MovingPlatform.gatesUp += NextStage;
        }
    }

    private void Update()
    {
        if (startPanel.activeSelf && Input.GetMouseButton(0))
        {
            startPanel.SetActive(false);
            levelUI.SetActive(true);
            GameManager.Instance.GamePlaying();
        }
    }

    private void LevelFailed()
    {
        GameManager.Instance.GameOver();
        losePanel.SetActive(true);
    }

    private void LevelComplete()
    {
        GameManager.Instance.GameWon();
        winPanel.SetActive(true);
    }

    private void LevelUp()
    {
        currentLevel++;
        PlayerPrefs.SetInt("Level", currentLevel);

        SetLevelTexts();

        for (int i = 0; i < stageImages.Length; i++)
        {
            stageImages[i].color = Color.white;
        }

        currentStage = 0;
    }

    private void SetLevelTexts()
    {
        currentLevelText.text = currentLevel.ToString();
        nextLevelText.text = (currentLevel + 1).ToString();
    }

    private void NextStage()
    {
        Image currentStageImage = stageImages[currentStage];

        if (currentStageImage != null)
            currentStageImage.color = orangeColor;

        currentStage++;

        if (currentStage == 1)
        {
            if (LevelGenerator.NewLevel != null)
                LevelGenerator.NewLevel();
        }
        else if (currentStage >= 3)
        {
            StartCoroutine(WaitForNextLevel());
        }
    }

    private IEnumerator WaitForNextLevel()
    {
        yield return new WaitForSeconds(1f);

        LevelUp();

        yield return new WaitForSeconds(0.5f);

        LevelComplete();
    }

    public void RestartLevel()
    {
        GameManager.Instance.GameStart();
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        GameManager.Instance.GamePlaying();
        winPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        levelFailed -= LevelFailed;
        MovingPlatform.gatesUp -= NextStage;
    }
}
