using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// This is a class for weapon
/// </summary>
public class Weapon : MonoBehaviour, IPickupable
{
    [SerializeField] private WeaponConfiguration weaponConfiguration;
    [SerializeField] private Transform barrelTransform;

    private GameObject bulletPrefab;
    private ObjectPool<Bullet> bulletPool;
    private List<WeaponProperty> weaponProperties;

    public WeaponConfiguration WeaponConfiguration => weaponConfiguration;
    public List<WeaponProperty> WeaponProperties => weaponProperties;

    private void Awake()
    {
        bulletPrefab = weaponConfiguration.BulletPrefab;

        // Create a copy of weapon properties from weapon configuration and assign it to weaponProperties
        weaponProperties = InitializeWeaponProperties();
    }

    private List<WeaponProperty> InitializeWeaponProperties()
    {
        // Create new list with the size of WeaponProperties.Count
        List<WeaponProperty> list = new(weaponConfiguration.WeaponProperties.Count);

        // Traverse each property in weapon configuration
        foreach (WeaponProperty property in weaponConfiguration.WeaponProperties)
        {
            // Create a copy of a property and add it to th list
            list.Add(new WeaponProperty(property));
        }

        // Return new property list
        return list;
    }

    private void Start()
    {
        // Get bullet pool
        if (!BulletPools.Instance.TryGetCorrespondingPool(bulletPrefab, out bulletPool))
            throw new System.NullReferenceException("No Corresponding pool was found for bullet");
    }

    public IEnumerator Shoot()
    {
        while (true)
        {
            SpawnAndMoveBullet();
            yield return new WaitForSeconds(weaponProperties[(int)WeaponPropertyType.FireRate].Amount);
        }
    }

    private void SpawnAndMoveBullet()
    {
        // Calculate the target position based on postion, forward direction of barrel and shot range of weapon
        Vector3 targetPosition = barrelTransform.position + (barrelTransform.forward * weaponProperties[(int)WeaponPropertyType.ShotRange].Amount);

        // Get bullet from pool
        Bullet bullet = bulletPool.Get();

        // Set bullet damage to weapon damage
        bullet.Damage = weaponProperties[(int)WeaponPropertyType.Damage].Amount;

        // Set bullet position to barrel position
        bullet.transform.position = barrelTransform.position;

        // Move bullet towards target position
        bullet.Move(targetPosition, weaponProperties[(int)WeaponPropertyType.ShotSpeed].Amount);
    }
}
