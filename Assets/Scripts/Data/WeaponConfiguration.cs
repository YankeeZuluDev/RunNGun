using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A data class for weapon
/// </summary>

[CreateAssetMenu(fileName ="Weapon Configuration")]
public class WeaponConfiguration : ScriptableObject
{
    [Header("Prfabs")]
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Properties")]
    [SerializeField] private List<WeaponProperty> weaponProperties;

    [Header("Next weapon level")]
    [SerializeField] private WeaponConfiguration upgrade;

    public GameObject WeaponPrefab => weaponPrefab;
    public GameObject BulletPrefab => bulletPrefab;
    public List<WeaponProperty> WeaponProperties => weaponProperties;
    public WeaponConfiguration Upgrade => upgrade;
}
