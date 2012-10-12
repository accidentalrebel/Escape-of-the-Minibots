using UnityEngine;
using System.Collections;

public class LevelEditor : MonoBehaviour {

    enum ObjectType { None, Tile, Minibot, Box, Door };

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
                Object prefabToSpawn = null;
                Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Transform parentTransform = null ;

                if (objectToSpawn == ObjectType.Tile)
                {
                    prefabToSpawn = Registry.prefabHandler.pfTile;
                    parentTransform = Registry.map.tilesContainer.transform;
                }
                else if (objectToSpawn == ObjectType.Minibot)
                {
                    prefabToSpawn = Registry.prefabHandler.pfMinibot;
                    parentTransform = Registry.map.minibotsContainer.transform;
                }
                else if (objectToSpawn == ObjectType.Box)
                {                    
                    prefabToSpawn = Registry.prefabHandler.pfBox;
                    parentTransform = Registry.map.boxesContainer.transform;
                }
                else if (objectToSpawn == ObjectType.Door)
                {
                    prefabToSpawn = Registry.prefabHandler.pfDoor;
                    parentTransform = Registry.map.doorsContainer.transform;
                }

                GameObject spawnedObject = (GameObject)Instantiate(prefabToSpawn);  // We initialize the object
                spawnedObject.transform.parent = parentTransform;                    // We then place the new object to its respecive container

                if (objectToSpawn == ObjectType.Tile)
                {
                    spawnedObject.GetComponent<Tile>().Initialize(new Vector3
                        (Mathf.Round(spawnPos.x)
                        , Mathf.Round(spawnPos.y), 0));                    
                }
                else if (objectToSpawn == ObjectType.Minibot)
                {
                    spawnedObject.GetComponent<Minibot>().Initialize(new Vector3
                        (Mathf.Round(spawnPos.x)
                        , Mathf.Round(spawnPos.y), 0)
                        , false, false);
                }
                else if (objectToSpawn == ObjectType.Box)
                {
                    spawnedObject.GetComponent<Box>().Initialize(new Vector3
                        (Mathf.Round(spawnPos.x)
                        , Mathf.Round(spawnPos.y), 0));
                }
                else if (objectToSpawn == ObjectType.Door)
                {
                    spawnedObject.GetComponent<Door>().Initialize(new Vector3
                        (Mathf.Round(spawnPos.x)
                        , Mathf.Round(spawnPos.y), 0));
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
            
            if (GUI.Button(new Rect(10, Screen.height-40, 100, 30), "Spawn Tile"))
            {
                Debug.Log("Spawn tile clicked");
                objectToSpawn = ObjectType.Tile;
            }
            if (GUI.Button(new Rect(110, Screen.height - 40, 100, 30), "Spawn Minibot"))
            {
                Debug.Log("Spawn minibot clicked");
                objectToSpawn = ObjectType.Minibot;
            }
            if (GUI.Button(new Rect(210, Screen.height - 40, 100, 30), "Spawn Box"))
            {
                Debug.Log("Spawn box clicked");
                objectToSpawn = ObjectType.Box;
            }
            if (GUI.Button(new Rect(310, Screen.height - 40, 100, 30), "Spawn Door"))
            {
                Debug.Log("Spawn door clicked");
                objectToSpawn = ObjectType.Door;
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
