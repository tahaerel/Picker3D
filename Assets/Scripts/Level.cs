using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Levels/New Level")]
public class Level : ScriptableObject
{
    public int levelIndex;
    public LevelStage firstStage;
    public LevelStage secondStage;
    public LevelStage finalStage;
}

[System.Serializable]
public class CollectableObject
{
    public CollectableObjectType type;
    public CollectableObjectShape shape;
    public Vector3 position;
}

[System.Serializable]
public class LevelStage
{
    [Tooltip("The minimum number of objects required to pass that stage.")]
    public int objectAmount;
    public CollectableObject[] collectableObject;
}

public enum CollectableObjectType
{
    CUBE,
    SPHERE,
    CAPSULE
}

public enum CollectableObjectShape
{
    ARROW,
    TRIANGLE,
    RECTANGLE
}
