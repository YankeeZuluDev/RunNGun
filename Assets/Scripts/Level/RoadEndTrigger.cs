using UnityEngine;

/// <summary>
/// This is class for road end trigger, that invokes game won event once player enters the trigger
/// </summary>
public class RoadEndTrigger : MonoBehaviour
{
    [SerializeField] private GameEvent gameWonEvent;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

        // Exit if other is nor player or game won event is not assigned
        if (playerMovement == null || gameWonEvent == null) return;

        // Trigger game won event
        gameWonEvent.Raise();

        // Play sound
        AudioManager.Instance.PlaySFX(AudioID.Won);
    }
}
