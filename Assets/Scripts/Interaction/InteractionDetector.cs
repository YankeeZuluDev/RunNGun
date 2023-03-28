using UnityEngine;

/// <summary>
/// A class, that is responsible for managing player`s collision detection
/// </summary>

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(InteractionHandler))]

public class InteractionDetector : MonoBehaviour
{
    private InteractionHandler interactionHandler;
    private void Awake()
    {
        // Initialize
        interactionHandler = GetComponent<InteractionHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Exit if other is not interactable
        if (!other.TryGetComponent<IInteractable>(out var interactable)) return;

        // Interact
        interactionHandler.StartInteraction(interactable);
    }
}
