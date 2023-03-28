using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

/// <summary>
/// This is an editor window that is used to construct a level and write level data to the level definition
/// </summary>
public class RunnerLevelConstructor : EditorWindow
{
    private LevelDefinition sourceLevelDefinition;
    private LevelDefinition currentLevelDefinition;
    private Transform tempSpawnablesParent;
    private GameObject terrainPrefab;
    private GameObject terrainGameObject;
    private float levelLenght;
    private float levelWidth;
    private bool isLoaded;


    [MenuItem("Window/Runner Level Constructor")]
    private static void CreateWindow()
    {
        GetWindow<RunnerLevelConstructor>();
    }

    #region Event subscribtion

    private void OnEnable()
    {
        EditorSceneManager.sceneSaved += OnSceneSaved;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private void OnDisable()
    {
        EditorSceneManager.sceneSaved -= OnSceneSaved;
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }

    #endregion

    private void OnGUI()
    {
        #region GUI fields

        sourceLevelDefinition = (LevelDefinition)EditorGUILayout.ObjectField("Level Definition", sourceLevelDefinition, typeof(LevelDefinition), false, null);

        EditorGUILayout.Space(5);

        tempSpawnablesParent = (Transform)EditorGUILayout.ObjectField("Spawnables Parent", tempSpawnablesParent, typeof(Transform), true, null);

        EditorGUILayout.Space(5);

        terrainPrefab = (GameObject)EditorGUILayout.ObjectField("Terrain Prefab", terrainPrefab, typeof(GameObject), false, null);

        EditorGUILayout.Space(5);

        levelLenght = EditorGUILayout.FloatField("Level Lenght", levelLenght);

        EditorGUILayout.Space(5);

        levelWidth = EditorGUILayout.FloatField("Level Width", levelWidth);

        #endregion

        EditorGUILayout.Space(5);

        #region Loading, unloading and reloading GUI

        if (sourceLevelDefinition != null)
        {
            if (!isLoaded)
            {
                LoadLevelInEditorTime(sourceLevelDefinition);
            }

            if (currentLevelDefinition.name != sourceLevelDefinition.name)
            {
                ReloadLevel();
            }
        }

        // if no level definition assigned
        if (sourceLevelDefinition == null && isLoaded)
        {
            UnloadLevel();
        }

        #endregion

        EditorGUILayout.Space(5);

        #region Buttons

        if (sourceLevelDefinition != null)
        {
            if (!isLoaded)
            {
                if (GUILayout.Button("Load Level", GUILayout.Height(30)))
                {
                    LoadLevelInEditorTime(sourceLevelDefinition);
                }
            }

            EditorGUILayout.Space(5);

            if (GUILayout.Button("Save Level Definition", GUILayout.Height(30)))
            {
                SaveLevelDefinition(currentLevelDefinition);
            }
        }

        #endregion
    }

    #region Event handlers

    private void OnSceneSaved(Scene scene)
    {
        if (sourceLevelDefinition != null)
        {
            SaveLevelDefinition(currentLevelDefinition);
        }
    }

    private void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode && sourceLevelDefinition != null)
        {
            EditorUtility.DisplayDialog("Cannot Enter Play Mode", "You cannot enter play mode while editing level, please unassign level definition", "OK");
            EditorApplication.isPlaying = false;
        }

    }

    #endregion

    #region Loading, unloading, reloading and saving logic

    private void LoadLevelInEditorTime(LevelDefinition sourceLevelDefinition)
    {
        Debug.Log("Loading...");

        // Spawn terrain
        if (terrainPrefab != null)
            terrainGameObject = (GameObject)PrefabUtility.InstantiatePrefab(terrainPrefab);

        // Set terraing scale if it is set not null
        if (terrainGameObject != null)
            terrainGameObject.transform.localScale = new Vector3(levelWidth, 1, levelLenght);

        // Create a copy of level definition instance
        currentLevelDefinition = Instantiate(sourceLevelDefinition);
        currentLevelDefinition.name = sourceLevelDefinition.name;

        // Load spawnables
        foreach (SpawnableData spawnableData in currentLevelDefinition.SpawnableData)
        {
            // Instantiate spawnable game object
            GameObject spawnableGameObject = (GameObject)PrefabUtility.InstantiatePrefab(spawnableData.SpawnablePrefab, tempSpawnablesParent);

            // Set position, rotation and scale of spawnable game object
            spawnableGameObject.transform.SetPositionAndRotation(spawnableData.Position, spawnableData.Rotation);
            spawnableGameObject.transform.localScale = spawnableData.Scale;
        }

        isLoaded = true;
    }

    private void UnloadLevel()
    {
        Debug.Log("Unloading...");

        //Destroy terrain if it is set not null
        if (terrainGameObject != null)
            DestroyImmediate(terrainGameObject);

        Spawnable[] spawnablesInScene = GetSpawnablesInScene();

        foreach (Spawnable spawnable in spawnablesInScene)
        {
            DestroyImmediate(spawnable.gameObject);
        }

        isLoaded = false;
    }

    private void ReloadLevel()
    {
        Debug.Log("Reloading...");

        UnloadLevel();
        LoadLevelInEditorTime(sourceLevelDefinition);
    }

    private void SaveLevelDefinition(LevelDefinition currentLevelDefinition)
    {
        Debug.Log("Saving...");

        // Get all spawnables in the scene
        Spawnable[] spawnablesInScene = GetSpawnablesInScene();

        currentLevelDefinition.SpawnableData = new SpawnableData[spawnablesInScene.Length];

        // Create spawnable data for each spawnable in scene and add it to current level definition
        for (int i = 0; i < spawnablesInScene.Length; i++)
        {
            Spawnable currentSpawnableInScene = spawnablesInScene[i];

            SpawnableData spawnableData = CreateSpawnableData(currentSpawnableInScene);

            currentLevelDefinition.SpawnableData[i] = spawnableData;
        }

        // Save level lenght and level width
        sourceLevelDefinition.RoadLenght = levelLenght;
        sourceLevelDefinition.RoadWidth = levelWidth;

        // Save level definition to disk
        sourceLevelDefinition.SpawnableData = currentLevelDefinition.SpawnableData;
        EditorUtility.SetDirty(sourceLevelDefinition);
        AssetDatabase.SaveAssets();

        // Reload level
        ReloadLevel();
    }

    #endregion

    #region Get spawnables and create spawnable data

    private Spawnable[] GetSpawnablesInScene()
    {
        return FindObjectsOfType<Spawnable>();
    }

    private SpawnableData CreateSpawnableData(Spawnable spawnable)
    {
        // Get spawnable prefab from game object
        GameObject spawnablePrefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(spawnable.gameObject);

        return new SpawnableData(spawnablePrefab, spawnable.transform.position, spawnable.transform.rotation, spawnable.transform.localScale);
    }

    #endregion
}
