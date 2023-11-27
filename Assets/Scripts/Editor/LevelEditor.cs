using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public class LevelEditor : EditorWindow
{
    private Vector2 scrollPosition;
    private SerializedObject serializedLevelData;

    public Level levelData;
    private int nextLevelIndex;

    [MenuItem("Window/Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditor>("Level Editor");
    }

    void OnEnable()
    {
        // Create a serialized object for accessing the Level Data
        serializedLevelData = new SerializedObject(this);
        nextLevelIndex = FindNextLevelIndex();
    }

    void OnGUI()
    {
        serializedLevelData.Update();

        // Start a scroll view for the GUI
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        EditorGUILayout.LabelField("Level Editor", EditorStyles.boldLabel);

        // Access Level Data using SerializedObject
        EditorGUILayout.PropertyField(serializedLevelData.FindProperty("levelData"), GUIContent.none, true);

        // Button to create a new Level
        if (GUILayout.Button("New Level"))
        {
            CreateNewLevel();
        }

        if (levelData != null)
        {
            EditorGUILayout.LabelField("Level Index: " + levelData.levelIndex);

            // Editable Level Index
            levelData.levelIndex = EditorGUILayout.IntField("Edit Level Index", levelData.levelIndex);

            DrawLevelStage("First Stage", ref levelData.firstStage);
            DrawLevelStage("Second Stage", ref levelData.secondStage);
            DrawLevelStage("Final Stage", ref levelData.finalStage);
        }
        else
        {
            EditorGUILayout.HelpBox("Level Data not selected!", MessageType.Info);
        }

        EditorGUILayout.EndScrollView();

        serializedLevelData.ApplyModifiedProperties();
    }

    void DrawLevelStage(string stageName, ref LevelStage stage)
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField(stageName, EditorStyles.boldLabel);
        if (stage != null)
        {
            // Editable Object Amount
            stage.objectAmount = EditorGUILayout.IntField("Object Amount", stage.objectAmount);

            // Initialize the collectableObject array if it's null
            if (stage.collectableObject == null)
            {
                stage.collectableObject = new CollectableObject[0];
            }

            // Editable Collectable Objects
            for (int i = 0; i < stage.collectableObject.Length; i++)
            {
                EditorGUILayout.LabelField($"Object {i + 1}");
                stage.collectableObject[i].type = (CollectableObjectType)EditorGUILayout.EnumPopup("Type", stage.collectableObject[i].type);
                stage.collectableObject[i].shape = (CollectableObjectShape)EditorGUILayout.EnumPopup("Shape", stage.collectableObject[i].shape);
                stage.collectableObject[i].position = EditorGUILayout.Vector3Field("Position", stage.collectableObject[i].position);
            }

            // Add and Remove Collectable Objects
            if (GUILayout.Button("Add Object"))
            {
                ArrayUtility.Add(ref stage.collectableObject, new CollectableObject());
            }

            if (GUILayout.Button("Remove Object") && stage.collectableObject.Length > 0)
            {
                ArrayUtility.RemoveAt(ref stage.collectableObject, stage.collectableObject.Length - 1);
            }
        }
        else
        {
            EditorGUILayout.HelpBox(stageName + " not set!", MessageType.Info);
        }
    }

    void CreateNewLevel()
    {
        levelData = CreateInstance<Level>();
        levelData.levelIndex = nextLevelIndex;
        AssetDatabase.CreateAsset(levelData, $"Assets/Resources/Levels/Level{nextLevelIndex}.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        nextLevelIndex++;
    }

    int FindNextLevelIndex()
    {
        string[] levelAssets = AssetDatabase.FindAssets("t:Level", new[] { "Assets/Resources/Levels" });
        int[] existingIndices = levelAssets
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(path => AssetDatabase.LoadAssetAtPath<Level>(path))
            .Where(level => level != null)
            .Select(level => level.levelIndex)
            .ToArray();

        if (existingIndices.Length > 0)
        {
            return existingIndices.Max() + 1;
        }
        else
        {
            return 0; 
        }
    }
}
