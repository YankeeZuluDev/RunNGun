using UnityEngine;

/// <summary>
/// This class is used to start an interaction that corresponds to an interactable
/// </summary>
public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private GameEvent gameLostEvent;

    private InteractionDefinitions interactionDefinitions = new();

    private WeaponList weaponList;
    private WeaponPropertiesManager changeWeaponProperties;
    private WeaponAnimationManager weaponAnimationManager;
    private MoneyStorage moneyStorage;

    public void InitializeInteractionHandler(MoneyStorage moneyStorage)
    {
        this.moneyStorage = moneyStorage;
    }

    private void Awake()
    {
        weaponList = GetComponentInChildren<WeaponList>();
        changeWeaponProperties = GetComponentInChildren<WeaponPropertiesManager>();
        weaponAnimationManager = GetComponentInChildren<WeaponAnimationManager>();
    }

    /// <summary>
    /// Invokes the corresponding interaction for the given interactable
    /// </summary>
    public void StartInteraction(IInteractable interactable)
    {
        switch (interactable)
        {
            case Obstacle obstacle:
                interactionDefinitions.Interact(gameLostEvent);
                break;
            case Gate gate:
                interactionDefinitions.Interact(gate, changeWeaponProperties);
                break;
            case ItemHolder itemHolder:
                interactionDefinitions.Interact(itemHolder, weaponList, weaponAnimationManager, moneyStorage); // передавать money storage
                break;
            default:
                throw new System.ArgumentException($"Interaction for {interactable} is not defined");
        }
    }
}
