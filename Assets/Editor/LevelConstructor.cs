using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

//public class LevelConstructor : EditorWindow // Перенести общий функционал в базовый класс и специализированные Level Constructor'ы наследовать от него
//{
//    private LevelDefinition sourceLevelDefinition;
//    private LevelDefinition currentLevelDefinition;
//    private GameObject[] spawnablesPrefabs;
//    private Transform spawnablesParent;
//    private bool isLoaded;
//    private const string spawnableParentTag = "Spawnable Parent";

//    [MenuItem("Window/Level Constructor")]
//    private static void CreateWindow()
//    {
//        GetWindow<LevelConstructor>();
//    }

//    private void Awake()
//    {
//        spawnablesParent = GameObject.FindGameObjectWithTag(spawnableParentTag).transform;
//    }

//    private void OnEnable()
//    {
//        EditorSceneManager.sceneSaved += OnSceneSaved;
//        LoadSpawnablesFromResources();
//    }

//    private void OnGUI()
//    {
//        #region GUI fields

//        sourceLevelDefinition = (LevelDefinition)EditorGUILayout.ObjectField("Level Definition", sourceLevelDefinition, typeof(LevelDefinition), false, null);

//        EditorGUILayout.Space(5);

//        spawnablesParent = (Transform)EditorGUILayout.ObjectField("Spawnables Parent", spawnablesParent, typeof(Transform), true, null);

//        #endregion

//        EditorGUILayout.Space(5);

//        #region Loading, unloading and reloading
//        if (sourceLevelDefinition != null)
//        {
//            if (!isLoaded)
//            {
//                LoadLevel(sourceLevelDefinition);
//            }

//            // if new level definition assigned // begin change check() and  end change check() //exclude playmode
//            if (currentLevelDefinition.name != sourceLevelDefinition.name)
//            {
//                ReloadLevel();
//            }
//        }

//        // if no level definition assigned
//        if (sourceLevelDefinition == null && isLoaded)
//        {
//            UnloadLevel();
//        }
//        #endregion

//        EditorGUILayout.Space(5);

//        #region Buttons
//        if (sourceLevelDefinition != null)
//        {
//            if (!isLoaded)
//            {
//                if (GUILayout.Button("Load Level", GUILayout.Height(30)))
//                {
//                    LoadLevel(sourceLevelDefinition);
//                }
//            }

//            EditorGUILayout.Space(5);

//            if (GUILayout.Button("Save Level Definition", GUILayout.Height(30)))
//            {
//                SaveLevelDefinition(currentLevelDefinition);
//            }
//        }
//        #endregion

//        EditorGUILayout.Space(5);

//        #region Spawnables Matrix

//        GUILayout.Label("Prefabs", new GUIStyle(GUI.skin.label)
//        {
//            alignment = TextAnchor.MiddleCenter,
//            fontStyle = FontStyle.Bold
//        });

//        //можно сделать, чтобы переносилось чере width factor /4 /5 /6 в зависимости от window width
//        DrawSpawnablesMatrix(4, (int)Mathf.Ceil((float)spawnablesPrefabs.Length / 4));

//        if (GUILayout.Button("Refresh", GUILayout.Height(20)))
//        {
//            LoadSpawnablesFromResources();
//        }

//        #endregion
//    }

//    private void OnSceneSaved(Scene scene)
//    {
//        if (sourceLevelDefinition != null)
//        {
//            SaveLevelDefinition(currentLevelDefinition);
//        }
//    }

//    private void LoadLevel(LevelDefinition source)
//    {
//        currentLevelDefinition = Instantiate(source);
//        currentLevelDefinition.name = source.name;

//        if (currentLevelDefinition.Spawnables.Length > 0)
//        {
//            foreach (SpawnableData spawnable in currentLevelDefinition.Spawnables)
//            {
//                var GO = PrefabUtility.InstantiatePrefab(spawnable.SourcePrefab, spawnablesParent) as GameObject;
//                GO.transform.position = spawnable.Position;
//                GO.transform.rotation = spawnable.Rotation;
//                GO.transform.localScale = spawnable.Scale;
//            }
//        }

//        isLoaded = true;
//    }

//    private void UnloadLevel()
//    {
//        Spawnable[] spawnables = GetSpawnablesInHierarchy();

//        foreach (Spawnable spawnable in spawnables)
//        {
//            DestroyImmediate(spawnable.gameObject);
//        }

//        isLoaded = false;
//    }

//    private void ReloadLevel()
//    {
//        UnloadLevel();
//        LoadLevel(sourceLevelDefinition);
//    }

//    //можно смотреть в методе, есть ли source и убрать из вызовов
//    private void SaveLevelDefinition(LevelDefinition current)
//    {
//        Spawnable[] spawnables = GetSpawnablesInHierarchy();
//        current.Spawnables = new SpawnableData[spawnables.Length];

//        for (int i = 0; i < spawnables.Length; i++)
//        {
//            var currentSpawnable = spawnables[i];

//            current.Spawnables[i] = new SpawnableData(
//                sourcePrefab: PrefabUtility.GetCorrespondingObjectFromOriginalSource(currentSpawnable.gameObject),
//                position: currentSpawnable.transform.position,
//                rotation: currentSpawnable.transform.rotation,
//                scale: currentSpawnable.transform.localScale
//                );
//        }
//        sourceLevelDefinition.Spawnables = current.Spawnables;
//        EditorUtility.SetDirty(sourceLevelDefinition);
//        AssetDatabase.SaveAssets();
//    }

//    private Spawnable[] GetSpawnablesInHierarchy()
//    {
//        return FindObjectsOfType(typeof(Spawnable)) as Spawnable[];
//    }

//    private void LoadSpawnablesFromResources()
//    {
//        spawnablesPrefabs = Resources.LoadAll<GameObject>("Prefabs/Spawnables");
//    }

//    private void DrawSpawnablesMatrix(int columns, int rows)
//    {
//        int currentSpawnableIndex = 0;

//        for (int i = 0; i < rows; i++)
//        {
//            EditorGUILayout.BeginHorizontal();

//            for (int j = 0; j < columns; j++, currentSpawnableIndex++)
//            {
//                GUIContent content = currentSpawnableIndex < spawnablesPrefabs.Length ? new GUIContent(AssetPreview.GetAssetPreview(spawnablesPrefabs[currentSpawnableIndex])) : new GUIContent("");

//                if (GUILayout.Button(content, GUILayout.Width(100), GUILayout.Height(100)) && content.image != null)
//                {
//                    InstantiateSpawnable(currentSpawnableIndex);
//                }

//                GUILayout.FlexibleSpace();
//            }

//            EditorGUILayout.EndHorizontal();
//        }
//    }

//    private void InstantiateSpawnable(int currentSpawnableIndex)
//    {
//        // Get the position where spawnable will be instantiated
//        Vector3 spawnPosition = GetSpawnPositionFromRay();

//        // Instantiate spawnable
//        GameObject GO = PrefabUtility.InstantiatePrefab(spawnablesPrefabs[currentSpawnableIndex], spawnablesParent) as GameObject;
        
//        // Set spawnable position
//        GO.transform.position = spawnPosition;

//        // Select instantiated spawnable
//        Selection.activeObject = GO;
//    }

//    /// <summary>
//    /// Casts a ray from the center of the screen
//    /// </summary>
//    /// <returns> Position where object should be spawned</returns>
//    private Vector3 GetSpawnPositionFromRay()
//    {
//        // Cast a ray from the center of the screen
//        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
//        RaycastHit hit;

//        if (Physics.Raycast(ray, out hit))
//        {
//            Spawnable spawnable = hit.transform.gameObject.GetComponent<Spawnable>();

//            if (spawnable != null) // Returns spawnable position if ray hits Spawnable
//            {
//                return spawnable.transform.position;
//            }
//            else // Returns point where ray hit the collider
//            {
//                return new Vector3(hit.point.x, hit.point.y, hit.point.z);
//            }
//        }
//        else // If nothing was hit
//        {
//            return Vector3.zero;
//        }
//    }

//    private void OnDisable()
//    {
//        EditorSceneManager.sceneSaved -= OnSceneSaved;
//    }
//}