using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to change weapon properties and apply changes to the added weapons
/// </summary>

[RequireComponent(typeof(WeaponList))]

public class WeaponPropertiesManager : MonoBehaviour, IResettable
{
    private WeaponList weaponList;
    private Dictionary<WeaponPropertyType, float> changedPropertyDictionary = new();

    public bool HasChangedProperties => changedPropertyDictionary.Count != 0;

    private void Awake()
    {
        weaponList = GetComponent<WeaponList>();
    }

    /// <summary>
    /// Increase or decrease property value for each weapon in weapon list
    /// </summary>
    public void ChangeProperty(WeaponPropertyType propertyType, float propertyChangeAmount)
    {
        foreach (Weapon weapon in weaponList.Weapons)
        {
            // Get corresponding property
            WeaponProperty property = weapon.WeaponProperties[(int)propertyType];

            // Calculate new amount
            float newAmount = property.Amount + propertyChangeAmount;

            // Clamp newAmount within it`s min and max amount
            newAmount = Mathf.Clamp(newAmount, property.MinAmount, property.MaxAmount);

            // Change actual prperty amount
            property.Amount = newAmount;

            // Add change amount to change dictionary
            if (changedPropertyDictionary.TryGetValue(propertyType, out float currentValue))
                changedPropertyDictionary[propertyType] = currentValue + propertyChangeAmount;
            else
                changedPropertyDictionary.Add(propertyType, propertyChangeAmount);
        }
    }

    /// <summary>
    /// // Apply property changes made by gates
    /// </summary>
    public void ApplyChangedProperties(Weapon weapon)
    {
        foreach (KeyValuePair<WeaponPropertyType, float> changedProperty in changedPropertyDictionary)
        {
            weapon.WeaponProperties[(int)changedProperty.Key].Amount += changedProperty.Value;
        }
    }

    public void ResetGameObject()
    {
        // Reset all the property changes
        changedPropertyDictionary.Clear();
    }
}
