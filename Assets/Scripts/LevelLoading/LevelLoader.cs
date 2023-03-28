using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for loading and initializing the game
/// </summary>
public class LevelLoader : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private GameObject bulletPoolsPrefab;
    [SerializeField] private GameObject moneyStoragePrefab;
    [SerializeField] private GameObject uIManagerPrefab;
    [SerializeField] private GameObject audioManagerPrefab;

    [Header("Level")]
    [SerializeField] private GameObject cameraPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject terrainPrefab;

    [Space()]
    [SerializeField] private List<LevelDefinition> levelDefinitionsList;

    private int currentLevelID;

    #region Dependecies

    private Transform managersParent;
    private Transform spawnablesParent;
    private TerrainManager terrainManager;
    private MoneyStorage moneyStorage;
    private PlayerMovement playerMovement;
    private InteractionHandler playerInteractionHandler;
    private FollowCamera followCamera;
    private UIManager uIManager;
    private AudioManager audioManager;
    private MoneyUI moneyUI;
    private Camera cam;
    private BulletPools bulletPools;

    #endregion

    private void Awake()
    {
        // Get current level ID from PlayerPrefs
        currentLevelID = PlayerPrefs.GetInt("Level", 0);

        // Get current level definition from ID
        LevelDefinition currentLevel = GetCurrentLevelDefinition(currentLevelID);

        // Load and initialize game
        CreateTransformParents();
        LoadGame();
        InitializeGame(currentLevel);
        LoadCurrentLevel();
    }

    #region Load game

    private void LoadGame()
    {
        // Load bullet pools
        bulletPools = Instantiate(bulletPoolsPrefab, managersParent).GetComponent<BulletPools>();

        // Load money storage
        moneyStorage = Instantiate(moneyStoragePrefab, managersParent).GetComponent<MoneyStorage>();

        // Load terrain
        terrainManager = Instantiate(terrainPrefab).GetComponent<TerrainManager>();

        // Load player
        GameObject playerGameObject = Instantiate(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation);
        playerMovement = playerGameObject.GetComponent<PlayerMovement>();
        playerInteractionHandler = playerGameObject.GetComponent<InteractionHandler>();

        // Load camera
        GameObject cameraGameObject = Instantiate(cameraPrefab, cameraPrefab.transform.position, cameraPrefab.transform.rotation);
        cam = cameraGameObject.GetComponent<Camera>();
        followCamera = cameraGameObject.GetComponent<FollowCamera>();

        // Load UI 
        uIManager = Instantiate(uIManagerPrefab, managersParent).GetComponent<UIManager>();
        moneyUI = uIManager.MoneyUI;

        // Load audio manager
        audioManager = Instantiate(audioManagerPrefab, managersParent).GetComponent<AudioManager>();
    }

    private void InitializeGame(LevelDefinition currentLevel)
    {
        // Initialize bullet pools
        bulletPools.InitializePrefabPoolDicitonary();

        // Initialize money storage
        moneyStorage.InitializeMoneyStorage(moneyUI);

        // Initialize terrain
        terrainManager.InitializeTerrain(currentLevel.RoadLenght, currentLevel.RoadWidth);

        // Initialize player
        playerMovement.InitializePlayerMovement(currentLevel.RoadWidth);
        playerInteractionHandler.InitializeInteractionHandler(moneyStorage);

        // Initialize camera
        followCamera.InitialzieCamera(playerMovement);

        // Initialize UI
        uIManager.InitializeUIManager(cam, playerMovement, terrainManager.LevelStartMarkerTransform, terrainManager.LevelEndMarkerTransform);

        // Initialize audio manager
        audioManager.InitializeAudioManagerDictionary();
    }

    private void CreateTransformParents()
    {
        managersParent = new GameObject("ManagersParent").transform;
        spawnablesParent = new GameObject("SpawnablesParent").transform;
    }

    #endregion

    #region Load, Save and increment level

    public void LoadCurrentLevel()
    {
        // Get currentLevelDefinition
        LevelDefinition currentLevel = GetCurrentLevelDefinition(currentLevelID);

        // Initialize terrain
        terrainManager.InitializeTerrain(currentLevel.RoadLenght, currentLevel.RoadWidth);

        // Load spawnables
        foreach (SpawnableData spawnableData in currentLevel.SpawnableData)
        {
            GameObject spawnableGameObject = Instantiate(spawnableData.SpawnablePrefab, spawnableData.Position, spawnableData.Rotation, spawnablesParent);

            spawnableGameObject.transform.localScale = spawnableData.Scale;
        }
    }

    public void SaveCurrentLevel()
    {
        // Save current level ID to PlayerPrefs
        PlayerPrefs.SetInt("Level", currentLevelID);
    }

    public void IncrementCurrentLevelID()
    {
        // Exit if current level ID exceeds amount of level definition in list
        if (currentLevelID >= levelDefinitionsList.Count)
            return;

        // Increment current level ID
        currentLevelID++;
    }

    private LevelDefinition GetCurrentLevelDefinition(int levelID) //прибавл€ть ID. ѕотом ID прибавл€ть // или сюда передавать ID, 
    {
        // If level ID exceeds amount of level definition in list, load random
        if (levelID >= levelDefinitionsList.Count)
        {
            return levelDefinitionsList[Random.Range(0, levelDefinitionsList.Count)];
        }

        // Else return corresponding level definition
        return levelDefinitionsList[levelID];
    }

    #endregion
}
