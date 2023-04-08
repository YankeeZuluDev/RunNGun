using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// This class is used to add, spawn and remove weapons
/// </summary>

[RequireComponent(typeof(MergeWeaponsManager))]
[RequireComponent(typeof(WeaponPositionManager))]
[RequireComponent(typeof(WeaponPropertiesManager))]

public class WeaponList : MonoBehaviour, IResettable
{
    [SerializeField] private WeaponConfiguration defaultWeaponConfiguration;
    [SerializeField] private int maxWeaponAmount;

    private List<Weapon> weapons = new List<Weapon>();
    private MergeWeaponsManager mergeWeaponsManager;
    private WeaponPositionManager weaponPositionManager;
    private WeaponPropertiesManager weaponPropertiesManager;
    private BulletPools bulletPools;

    public List<Weapon> Weapons => weapons;
    public bool IsFull => weapons.Count >= maxWeaponAmount;

    public void InitializeWeaponList(BulletPools bulletPools)
    {
        this.bulletPools = bulletPools;
    }

    private void Awake()
    {
        mergeWeaponsManager = GetComponent<MergeWeaponsManager>();
        weaponPositionManager = GetComponent<WeaponPositionManager>();
        weaponPropertiesManager = GetComponent<WeaponPropertiesManager>();
    }

    private void Start()
    {
        SpawnDefaultWeapon();
    }

    public void AddWeapon(Weapon newWeapon)
    {
        // Get matching weapon from weapons list and merge if it exitsts
        weapons.TryGetWeapon(newWeapon, out Weapon matchingWeapon);

        // Add new weapon to the list
        weapons.Add(newWeapon);

        // Play sound
        AudioManager.Instance.PlaySFX(AudioID.AddWeapon);

        // Apply property changes made by gates
        if (weaponPropertiesManager.HasChangedProperties)
            weaponPropertiesManager.ApplyChangedProperties(newWeapon);

        // If matching weapon exists, then merge it with added weapon
        if (matchingWeapon != null) 
            mergeWeaponsManager.MergeWeapons(newWeapon, matchingWeapon);

        // Equally destribute weapon transform positions along area
        weaponPositionManager.UpdateWeaponPositions();

        // Get corresponding bullet pool for current weapon and pass to new weapon
        ObjectPool<Bullet> bulletPool = bulletPools.GetCorrespondingPool(newWeapon.WeaponConfiguration.BulletPrefab);
        newWeapon.SetBulletPool(bulletPool);

        // Start shooting
        newWeapon.StartCoroutine(newWeapon.Shoot());
    }

    public void RemoveWeapon(Weapon weapon)
    {
        weapons.Remove(weapon);
        Destroy(weapon.gameObject);
    }

    public Weapon SpawnWeapon(WeaponConfiguration weaponConfiguration)
    {
        return Instantiate(weaponConfiguration.WeaponPrefab, transform.position, transform.rotation, transform).GetComponent<Weapon>();
    }

    public void SpawnDefaultWeapon()
    {
        // Spawn default weapon
        Weapon defaultWeapon = SpawnWeapon(defaultWeaponConfiguration);

        // Add default weapon to list
        weapons.Add(defaultWeapon);

        // Get corresponding bullet pool for current weapon and pass to new weapon
        ObjectPool<Bullet> bulletPool = bulletPools.GetCorrespondingPool(defaultWeapon.WeaponConfiguration.BulletPrefab);
        defaultWeapon.SetBulletPool(bulletPool);
    } 

    public void ResetGameObject()
    {
        // Iterate backwards to allow collection modification while iterating
        for (int i = weapons.Count - 1; i >= 0; i--)
        {
            RemoveWeapon(weapons[i]);
        }
    }
}
