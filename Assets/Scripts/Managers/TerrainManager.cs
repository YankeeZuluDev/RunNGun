using UnityEngine;

/// <summary>
/// This class is used to adjust terrain texture tiling
/// </summary>
public class TerrainManager : MonoBehaviour
{
    [SerializeField] private MeshRenderer roadMeshRenderer;
    [SerializeField] private float levelLenghtToTilingFactor;
    [SerializeField] private Transform levelStartMarkerTransform;
    [SerializeField] private Transform levelEndMarkerTransform;

    public Transform LevelStartMarkerTransform => levelStartMarkerTransform;
    public Transform LevelEndMarkerTransform => levelEndMarkerTransform;

    public void InitializeTerrain(float levelLenght, float levelWidth)
    {
        // Set terrain width and lenght
        transform.localScale = new Vector3(levelWidth, 1, levelLenght);

        // Adjust tiling of terrain texture
        AdjustTextureTilingY(levelLenght);
    }

    private void AdjustTextureTilingY(float levelLenght)
    {
        float tilingY = levelLenght / levelLenghtToTilingFactor;

        roadMeshRenderer.material.mainTextureScale = new Vector2(roadMeshRenderer.material.mainTextureScale.x, tilingY);
    }
}
