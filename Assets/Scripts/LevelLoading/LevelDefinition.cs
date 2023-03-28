using UnityEngine;

/// <summary>
/// A data class for level definition
/// </summary>

[CreateAssetMenu(fileName ="Level Definition")]
public class LevelDefinition : ScriptableObject
{
    [SerializeField] private float roadLenght;
    [SerializeField] private float roadWidth;
    [SerializeField] private SpawnableData[] spawnableData;

    public float RoadLenght { get => roadLenght; set => roadLenght = value; }
    public float RoadWidth { get => roadWidth; set => roadWidth = value; }
    public SpawnableData[] SpawnableData { get => spawnableData; set => spawnableData = value; }
}
