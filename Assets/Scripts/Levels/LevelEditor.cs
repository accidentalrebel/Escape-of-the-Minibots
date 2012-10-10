using UnityEngine;
using System.Collections;

public class LevelEditor : MonoBehaviour {

    enum ObjectType { None, Tile, Minibot };

    bool mapEditMode = false;
    bool MapEditMode
    {
        set { mapEditMode = value; }
        get{ return mapEditMode; }
    }
    bool saveMapMode = false;

    XMLLevelReader levelReader;
    XMLLevelWriter levelWriter;
    string levelFileName;
    ObjectType objectToSpawn = ObjectType.None;
    Map map;
    bool isSimulating = true;

    void Start()
    {
        map = Registry.map;
        if (map == null)
            Debug.LogError("map is not found!");

        levelReader = map.GetComponent<XMLLevelReader>();
        if (levelReader == null)
            Debug.LogError("levelReader is not found!");

        levelWriter = map.GetComponent<XMLLevelWriter>();
        if (levelWriter == null)
            Debug.LogError("levelWriter is not found!");

        levelFileName = levelReader.levelToLoad;
    }

    void Update()
    {        
        if (objectToSpawn != ObjectType.None )
        {
            if (Input.GetMouseButtonDown(0) && mapEditMode)
            {
                // We determine which prefab to spawn
                Object prefabToSpawn = Registry.prefabHandler.pfTile;
                if (objectToSpawn == ObjectType.Tile)
                {
                    // We handle the actual initialization of the object
                    GameObject spawnedObject = (GameObject)Instantiate(Registry.prefabHandler.pfTile);
                    Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    spawnedObject.GetComponent<Tile>().Initialize(new Vector3
                        (Mathf.Round(spawnPos.x)
                        , Mathf.Round(spawnPos.y), 0));

                    // We then place the new object to its respecive container
                    spawnedObject.transform.parent = Registry.map.tilesContainer.transform;
                }
                else if (objectToSpawn == ObjectType.Minibot)
                {
                    // We handle the actual initialization of the object
                    GameObject spawnedObject = (GameObject)Instantiate(Registry.prefabHandler.pfMinibot);
                    Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    spawnedObject.GetComponent<Minibot>().Initialize(new Vector3
                        (Mathf.Round(spawnPos.x)
                        , Mathf.Round(spawnPos.y), 0)
                        , false, false);

                    // We then place the new object to its respecive container
                    spawnedObject.transform.parent = Registry.map.minibotsContainer.transform;
                }
                    

                // If the left key is currently pressed
                // Or if the leftShift key has just been released
                if (!Input.GetKey(KeyCode.LeftShift)
                    || Input.GetKeyUp(KeyCode.LeftShift))
                    objectToSpawn = ObjectType.None;   // We set the objectToSpawn to none
            }
        }        
    }

    void OnGUI()
    {
        MapEditMode = GUI.Toggle(new Rect(10, 10, 200, 20), MapEditMode, "Map Edit Mode");
        
        if (MapEditMode)
        {
            // Play and Stop Buttons
            string btnText = "";
            if (isSimulating)
                btnText = "Stop";
            else
                btnText = "Play";

            if (GUI.Button(new Rect(10, 40, 100, 30), btnText))
            {
                if (isSimulating)
                {
                    Time.timeScale = 0;
                    isSimulating = false;
                    map.RestartLevel();
                }
                else
                {
                    Time.timeScale = 1;
                    isSimulating = true;
                }
            }

            // Spawning buttons
            if (GUI.Button(new Rect(10, 550, 100, 30), "Spawn Tile"))
            {
                Debug.Log("Spawn tile clicked");
                objectToSpawn = ObjectType.Tile;
            }
            if (GUI.Button(new Rect(110, 550, 100, 30), "Spawn Minibot"))
            {
                Debug.Log("Spawn minibot clicked");
                objectToSpawn = ObjectType.Minibot;
            }

            // Save map mode
            saveMapMode = GUI.Toggle(new Rect(10, 80, 200, 20), saveMapMode, "Save map?");
            if (saveMapMode)
            {
                GUI.Label(new Rect(30, 105, 100, 50), "Level name: ");
                levelFileName = GUI.TextField(new Rect(115, 105, 50, 20), levelFileName);
                if (GUI.Button(new Rect(30, 130, 100, 30), "Save Map"))
                {
                    Debug.Log("Save map clicked");
                    levelWriter.SaveLevel(levelFileName);
                }
            }
        }
    }

    private void HandleTimeScale()
    {
        throw new System.NotImplementedException();
    }
}
