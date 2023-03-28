using UnityEngine;

/// <summary>
/// This is a class for UI manager
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private float planeDistance;
    [SerializeField] private LevelProgressTrackerUI levelProgressTrackerUI;
    [SerializeField] private MoneyUI moneyUI;

    public MoneyUI MoneyUI => moneyUI;

    public void InitializeUIManager(Camera camera, PlayerMovement playerMovement, Transform levelStartMarker, Transform levelEndMarker)
    {
        canvas.worldCamera = camera;
        canvas.planeDistance = planeDistance;
        levelProgressTrackerUI.InitializeLevelProgressTrackerUI(playerMovement, levelStartMarker, levelEndMarker);
    }
}
