using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Random = System.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject baseLevel;
    [SerializeField] private Transform collectibleObjectParent;

    [Header("Level Changes")]
    [SerializeField][Min(1)] private int currentLevel = 1;
    [Tooltip("If you want to change the level, please mark this as true.")]
    [SerializeField] private bool changeTheLevel;

    private Level level;
    private List<Level> levels;

    private List<GameObject> collectableObjectPrefabs;
    private List<GameObject> containers;
    private List<GameObject> baseLevelObjects;

    private Vector3 basePosition;
    private int zMultiplier = 0;
    private bool isFirstLevel = true;
    private int highestLevelIndex;
    public static Action NewLevel;

    private void Awake()
    {
        if (changeTheLevel)
            PlayerPrefs.SetInt("Level", currentLevel);
        else
            currentLevel = PlayerPrefs.GetInt("Level", 1);
    }

    void Start()
    {
        NewLevel += CreateAndDestroyLevel;

        GetCollectableObjects();
        GetLevels();
    }

    private void SetContainerText()
    {
        containers = new List<GameObject>();

        // Find and set text for each movingplatform in the base level

        for (int i = 0; i < 3; i++)
        {
            Transform tempContainer = baseLevel.transform.GetChild(i).Find("Container");

            if (tempContainer != null)
                containers.Add(tempContainer.gameObject);
        }

        for (int i = 0; i < containers.Count; i++)
        {
            TextMeshPro textMesh = containers[i].GetComponentInChildren<TextMeshPro>();

            if (textMesh != null)
            {
                if (i == 0)
                    textMesh.text = "0 / " + level.firstStage.objectAmount;
                else if (i == 1)
                    textMesh.text = "0 / " + level.secondStage.objectAmount;
                else if (i == 2)
                    textMesh.text = "0 / " + level.finalStage.objectAmount;
            }
        }
    }

    private void InstantiateCollectibleObjects(CollectableObject[] collectableObjects)
    {
        for (int i = 0; i < collectableObjects.Length; i++)
        {
            string type = collectableObjects[i].type.ToString()[0] + collectableObjects[i].type.ToString().Substring(1).ToLowerInvariant();
            string shape = collectableObjects[i].shape.ToString()[0] + collectableObjects[i].shape.ToString().Substring(1).ToLowerInvariant();

            string objectName = type + shape;

            GameObject temp = collectableObjectPrefabs.Where(obj => obj.name == objectName).SingleOrDefault();
            Instantiate(temp, basePosition + collectableObjects[i].position, Quaternion.identity, collectibleObjectParent);
        }
    }

    private void DestroyCollectibleObjects()
    {
        int totalObject = level.firstStage.collectableObject.Length + level.secondStage.collectableObject.Length + level.finalStage.collectableObject.Length;

        for (int i = 0; i < totalObject; i++)
        {
            Destroy(collectibleObjectParent.GetChild(i).gameObject);
        }
    }

    private void GetCollectableObjects()
    {
        collectableObjectPrefabs = new List<GameObject>();

        collectableObjectPrefabs = Resources.LoadAll<GameObject>("CollectableObjects").ToList();
    }


#if UNITY_EDITOR
    [ContextMenu("Validate Levels")]
    private void ValidateLevels()
    {

        Debug.Log("Validate Level");

        EditorUtility.DisplayDialog("Level Validation Warning", "Üretilen level sayısı ve index sayısı uyuşmuyor. Doğru leveli çağırmakta hata yaşayabilirsiniz. Lütfen 'Levels' klasörünü kontrol edin.", "Tamam");
        Debug.LogWarning("Üretilen level sayısı ve index sayısı uyuşmuyor. Lütfen kontrol edin.");

    }
#endif


    private void GetLevels()
    {
        levels = new List<Level>();
        baseLevelObjects = new List<GameObject>();

        levels = Resources.LoadAll<Level>("Levels").ToList();
        levels = levels.OrderBy(w => w.levelIndex).ToList();

        highestLevelIndex = FindHighestLevelIndex();

        Debug.Log("Highest Level Index: " + highestLevelIndex);
        Debug.Log("Levels Count" + levels.Count);

#if UNITY_EDITOR
        if (highestLevelIndex != levels.Count)
        {
            ValidateLevels();
        }
#endif

        CreateAndDestroyLevel();
        isFirstLevel = false;
    }
    private int FindHighestLevelIndex()
    {
        highestLevelIndex = 0;

        foreach (var level in levels)
        {
            if (level.levelIndex > highestLevelIndex)
            {
                highestLevelIndex = level.levelIndex;
            }
        }

        return highestLevelIndex;
    }
    private void CreateAndDestroyLevel()
    {
        currentLevel = PlayerPrefs.GetInt("Level", 1);

        Debug.Log("Current Level=" + currentLevel);
        // Determine the level to be loaded
        if (currentLevel < levels.Count)
        {
            if (isFirstLevel)
            {
                level = levels[currentLevel - 1];
            }

            else
            {
                level = levels[currentLevel];
            }
        }
        else
        {
            Debug.Log("cont random level");
            ContinueWithRandomLevel();
        }

        // Destroy previous collectible objects and base level
        if (baseLevelObjects.Count == 2)
        {
            DestroyCollectibleObjects();

            Destroy(baseLevelObjects[0]);
            baseLevelObjects.RemoveAt(0);
        }

        basePosition = new Vector3(0, 0, 171.6f * zMultiplier);
        zMultiplier++;

        SetContainerText();

        baseLevelObjects.Add(Instantiate(baseLevel, basePosition, Quaternion.identity));

        InstantiateCollectibleObjects(level.firstStage.collectableObject);
        InstantiateCollectibleObjects(level.secondStage.collectableObject);
        InstantiateCollectibleObjects(level.finalStage.collectableObject);
    }

    // Continue with a random level if the current level exceeds the available levels
    private void ContinueWithRandomLevel()
    {
        Debug.Log("contuinerandom");
        var random = new Random();
        int index = random.Next(levels.Count);

        level = levels[index];

        if (isFirstLevel)
            level.levelIndex = currentLevel;

        else
            level.levelIndex = currentLevel + 1;

        levels.Add(level);

    }

    private void OnDestroy()
    {
        Debug.Log("on destroy");
        NewLevel -= CreateAndDestroyLevel;
    }
}

