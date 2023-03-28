using UnityEngine;

/// <summary>
/// This is data holder for spawnable
/// </summary>

[System.Serializable]
public class SpawnableData
{
    public GameObject SpawnablePrefab;
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;

    public SpawnableData(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        SpawnablePrefab = prefab;
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }
}
