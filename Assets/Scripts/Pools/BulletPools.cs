using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// A collection of object pools for bullets
/// </summary>
public class BulletPools : MonoBehaviour
{
    //private static BulletPools instance;

    [SerializeField] private List<GameObject> bulletPrefabs;

    // Dictionary, where the key is prefab and the value is bullet pool
    private Dictionary<GameObject, ObjectPool<Bullet>> prefabToPoolDicitionary = new();

    public void InitializePrefabPoolDicitonary()
    {
        foreach (GameObject bulletPrefab in bulletPrefabs)
        {
            // Initialzie bullet pool
            ObjectPool<Bullet> bulletPool = new(() => OnSpawn(bulletPrefab), OnGet, OnRelease, OnKill, false, 10, 1000); // TODO: Set default pool capacity for each bullet 

            // Populate prefab to pool dictionary 
            prefabToPoolDicitionary.Add(bulletPrefab, bulletPool);
        }
    }

    private Bullet OnSpawn(GameObject prefab)
    {
        // Instantiate bullet instance
        Bullet bullet = Instantiate(prefab, transform).GetComponent<Bullet>();

        // Pass return to pool action
        bullet.ReturnToPoolAction = () => prefabToPoolDicitionary[prefab].Release(bullet);

        return bullet;
    }

    private void OnGet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnRelease(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnKill(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public ObjectPool<Bullet> GetCorrespondingPool(GameObject prefab)
    {
        // Return corresponding pool if it exists in dictionary
        if (prefabToPoolDicitionary.ContainsKey(prefab))
            return prefabToPoolDicitionary[prefab];

        // Throw an exception if nothing was found
        throw new KeyNotFoundException($"No Corresponding pool was found for {prefab}");
    }
}
