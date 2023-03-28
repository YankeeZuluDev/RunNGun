using System.Collections;

/// <summary>
/// A collection of player interactions
/// </summary>

public class InteractionDefinitions
{
    #region Interactions

    /// <summary>
    /// Interact with obstacle
    /// </summary>
    public void Interact(GameEvent gameLostEvent)
    {
        // Invoke the game lost event
        gameLostEvent.Raise();

        // Play sound
        AudioManager.Instance.PlaySFX(AudioID.Lost);
    }

    /// <summary>
    /// Interact with gate
    /// </summary>
    public void Interact(Gate gate, WeaponPropertiesManager weaponPropertiesManager)
    {
        // Exit if gate is already triggered
        if (gate.IsTriggered) return;

        // Chang weapon property
        weaponPropertiesManager.ChangeProperty(gate.PropertyType, gate.RealValue);

        gate.IsTriggered = true;
    }

    /// <summary>
    /// Interact with item holder
    /// </summary>
    public void Interact(ItemHolder itemHolder, WeaponList weaponList, WeaponAnimationManager weaponAnimationManager, MoneyStorage moneyStorage)
    {
        // Exit if item list is full
        if (weaponList.IsFull) return;

        // Exit if item is not IPickupable
        if (!itemHolder.Item.TryGetComponent<IPickupable>(out var pickupable)) return;

        switch (pickupable)
        {
            case Weapon weapon:
                AnimateAndAddWeapon(weapon, weaponList, itemHolder, weaponAnimationManager);
                break;
            case Money money:
                AddMoney(moneyStorage, money.Amount, itemHolder);
                break;
            default:
                throw new System.ArgumentException($"Interaction with {pickupable} is not defined");
        }
    }

    #endregion

    #region Util

    private void AnimateAndAddWeapon(Weapon weapon, WeaponList weaponList, ItemHolder itemHolder, WeaponAnimationManager weaponAnimationManager)
    {
        // Set weapon parent to weapon list
        weapon.transform.SetParent(weaponList.transform);

        // Reset item holder
        itemHolder.ResetGameObject();

        // Play adding animation and add weapon on complete
        IEnumerator addingAnimation = weaponAnimationManager.WeaponAddingAnimation(
            transform: weapon.transform,
            targetTransform: weaponList.transform,
            onComplete: () => { weaponList.AddWeapon(weapon); });

        // Play adding animation and add to weapon list
        weaponAnimationManager.StartCoroutine(addingAnimation);
    }

    private void AddMoney(MoneyStorage moneyStorage, int amount, ItemHolder itemHolder)
    {
        // Add money
        moneyStorage.AddMoney(amount);

        // Play sound
        AudioManager.Instance.PlaySFX(AudioID.Money);

        // Reset item holder
        itemHolder.ResetGameObject();
    }

    #endregion
}
