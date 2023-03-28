using System.Collections;
using UnityEngine;

/// <summary>
/// This class is used to merge weapons
/// </summary>

[RequireComponent(typeof(WeaponAnimationManager))]

public class MergeWeaponsManager : MonoBehaviour
{
    private WeaponList weaponList;
    private WeaponAnimationManager weaponAnimationManager;

    private void Awake()
    {
        weaponList = GetComponentInParent<WeaponList>();

        weaponAnimationManager = GetComponent<WeaponAnimationManager>();
    }

    public void MergeWeapons(Weapon weapon, Weapon otherWeapon) // Recursive method // merge animation
    {
        // Get weapon upgrade
        WeaponConfiguration weaponUpgrade = weapon.WeaponConfiguration.Upgrade;

        // exit if no weapon upgrade
        if (weaponUpgrade == null) return;

        // Get merged weapon
        Weapon mergedWeapon = weaponList.SpawnWeapon(weaponUpgrade);

        // remove both weapons and add merged weapon to weaponList when animation is complete
        System.Action onComplete = () =>
        {
            // Remove both weapons
            weaponList.RemoveWeapon(weapon);
            weaponList.RemoveWeapon(otherWeapon);

            // Add merged weapon
            weaponList.AddWeapon(mergedWeapon);
        };

        // Play merging animation
        IEnumerator mergingAnimation = weaponAnimationManager.WeaponMergingAnimation(weapon.transform, otherWeapon.transform, onComplete);

        weaponAnimationManager.StartCoroutine(mergingAnimation);
    }
}
