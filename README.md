# RunNGun
A repository for "Run N Gun: Merge" game source code

[:camera: **See Screenshots**](#screenshots)

[:movie_camera: **See gameplay video**](https://www.youtube.com/watch?v=uu2AngDUh1s)

[:video_game: **Play**](https://play.google.com/store/apps/details?id=com.YankeeZulu.RunNGun)

## About the game
Genre: Hyper-casual

Mechanics: running, shooting, merging

Unity version: 2021.3.18f1 (LTS)

Accessibility: Project can be freely explored in unity

## Screenshots

<div style="display:flex;">
  <img src="https://user-images.githubusercontent.com/129124150/230711931-dc82b082-d3b2-4878-a8ad-6f046a1bdceb.jpg" alt="screenshot_1" width="270" height="480">
  <img src="https://user-images.githubusercontent.com/129124150/230711970-45ec7c31-1996-43eb-b313-b547ff35ba8f.jpg" alt="screenshot_2" width="270" height="480">
  <img src="https://user-images.githubusercontent.com/129124150/230711976-99469490-2178-48a3-8ea2-95858d799111.jpg" alt="screenshot_3" width="270" height="480">
  <img src="https://user-images.githubusercontent.com/129124150/230711980-4272fdfb-7d16-4e4d-889b-61de90dbf6e3.jpg" alt="screenshot_4" width="270" height="480">
</div>

## Best сode practices in this project

### Object pooling
This project uses object pooling to efficiently manage and reuse bullet objects within the game. Object pooling minimizes the overhead of creating and destroying bullet objects dynamically, resulting in improved performance and reduced memory allocation. 
```
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
```

### Smooth follow camera


### Split responsibilities


### Game event system


### Custom level constructor window


### Load time dependency injection


### Static classes

