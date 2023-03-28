using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is a class for level progress bar UI, located on the top of the screen
/// </summary>
public class LevelProgressTrackerUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private PlayerMovement playerMovement;
    private Transform levelStartMarker;
    private Transform levelEndMarker;

    public void InitializeLevelProgressTrackerUI(PlayerMovement playerMovement, Transform levelStartMarker, Transform levelEndMarker)
    {
        this.playerMovement = playerMovement;
        this.levelStartMarker = levelStartMarker;
        this.levelEndMarker = levelEndMarker;
    }

    private void Update()
    {
        fillImage.fillAmount = CalculateLevelProgressFillAmount();
    }

    private float CalculateLevelProgressFillAmount()
    {
        // Calculate where player z position lies between level start z and level end z markers
        return Mathf.InverseLerp(levelStartMarker.position.z, levelEndMarker.position.z, playerMovement.GetPlayerPositon().z);
    }
}
