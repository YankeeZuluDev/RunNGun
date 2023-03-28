using UnityEngine;

/// <summary>
/// This class is used to enable and disable shooting
/// </summary>

[RequireComponent(typeof(WeaponList))]

public class WeaponShootManager : MonoBehaviour
{
    private WeaponList weaponList;

    private void Awake()
    {
        weaponList = GetComponent<WeaponList>();
    }

    public void StartShooting()
    {
        foreach (Weapon weapon in weaponList.Weapons)
        {
            weapon.StartCoroutine(weapon.Shoot());
        }
    }

    public void StopShooting()
    {
        foreach (Weapon weapon in weaponList.Weapons)
        {
            weapon.StopAllCoroutines();
        }
    }
}
