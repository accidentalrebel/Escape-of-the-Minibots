using UnityEngine;
using System.Collections;

public class LevelEditor : MonoBehaviour {

    enum ObjectType { None, Tile, Minibot, Box, Door, GravityInverter, Hazard, HorizontalInverter
        , MovingPlatform, StepSwitch, Switch, TriggerableBlock };

    bool mapEditMode = false;
    bool MapEditMode
    {
        set { mapEditMode = value; }
        get{ return mapEditMode; }
    }
    bool saveMapMode = false;
    string levelFileName;
    bool isSimulating = true;

    XMLLevelReader levelReader;
    XMLLevelWriter levelWriter;
    Renderer originMarkerRenderer;
    ObjectType objectToSpawn = ObjectType.None;
    Map map;        

    // ************************************************************************************
    // MAIN
    // ************************************************************************************

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

        InitializeOriginMarker();
    }

    void Update()
    {
        if (objectToSpawn != ObjectType.None)
        {
            if (Input.GetMouseButtonDown(0) && mapEditMode)
            {
                HandleLevelObjectPlacement();

                // If the left key is currently pressed
                // Or if the leftShift key has just been released
                if (!Input.GetKey(KeyCode.LeftShift)
                    || Input.GetKeyUp(KeyCode.LeftShift))
                    objectToSpawn = ObjectType.None;   // We set the objectToSpawn to none
            }
        }

        // This only shows the origin marker when mapEditMode is enabled
        if (mapEditMode)
            originMarkerRenderer.enabled = true;
        else
            originMarkerRenderer.enabled = false;
    }

    // ************************************************************************************
    // Initializations
    // ************************************************************************************

    private void InitializeOriginMarker()
    {
        Object pfOriginMarker = Resources.Load(@"Prefabs/pfOriginMarker");
        if (pfOriginMarker == null)
            Debug.LogError("Can not find pfOriginMarker prefab!");

        // We create an instance of the coordinate marker
        GameObject originMarkerObject = (GameObject)Instantiate(pfOriginMarker);
        originMarkerObject.transform.parent = map.transform;
        originMarkerObject.name = "Origin Marker";
        originMarkerRenderer = originMarkerObject.GetComponent<GraphicHandler>().theRenderer;
        originMarkerRenderer.enabled = false;
    }

    // ************************************************************************************
    // OBJECT PLACEMENT
    // ************************************************************************************

    private void HandleLevelObjectPlacement()
    {
        // We determine which prefab to spawn
        Object prefabToSpawn = null;
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Transform parentTransform = null;

        if (objectToSpawn == ObjectType.Tile)
        {
            prefabToSpawn = Registry.prefabHandler.pfTile;
            parentTransform = Registry.map.tilesContainer.transform;
        }
        else if (objectToSpawn == ObjectType.Hazard)
        {
            prefabToSpawn = Registry.prefabHandler.pfHazard;
            parentTransform = Registry.map.hazardsContainer.transform;
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
        else if (objectToSpawn == ObjectType.Switch)
        {
            prefabToSpawn = Registry.prefabHandler.pfSwitch;
            parentTransform = Registry.map.switchesContainer.transform;
        }
        else if (objectToSpawn == ObjectType.StepSwitch)
        {
            prefabToSpawn = Registry.prefabHandler.pfStepSwitch;
            parentTransform = Registry.map.stepSwitchesContainer.transform;
        }
        else if (objectToSpawn == ObjectType.GravityInverter)
        {
            prefabToSpawn = Registry.prefabHandler.pfGravityInverter;
            parentTransform = Registry.map.gravityInvertersContainer.transform;
        }
        else if (objectToSpawn == ObjectType.HorizontalInverter)
        {
            prefabToSpawn = Registry.prefabHandler.pfHorizontalInverter;
            parentTransform = Registry.map.horizontalInvertersContainer.transform;
        }
        else if (objectToSpawn == ObjectType.TriggerableBlock)
        {
            prefabToSpawn = Registry.prefabHandler.pfTriggerableBlock;
            parentTransform = Registry.map.triggerableBlocksContainer.transform;
        }

        GameObject spawnedObject = (GameObject)Instantiate(prefabToSpawn);  // We initialize the object
        spawnedObject.transform.parent = parentTransform;                    // We then place the new object to its respecive container

        if (objectToSpawn == ObjectType.Tile)
        {
            spawnedObject.GetComponent<Tile>().Initialize(new Vector3
                (Mathf.Round(spawnPos.x)
                , Mathf.Round(spawnPos.y), 0));
        }
        else if (objectToSpawn == ObjectType.Hazard)
        {
            spawnedObject.GetComponent<HazardTile>().Initialize(new Vector3
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
        else if (objectToSpawn == ObjectType.Switch)
        {
            spawnedObject.GetComponent<Switch>().Initialize(new Vector3
                (Mathf.Round(spawnPos.x)
                , Mathf.Round(spawnPos.y), 0));
        }
        else if (objectToSpawn == ObjectType.StepSwitch)
        {
            spawnedObject.GetComponent<StepSwitch>().Initialize(new Vector3
                (Mathf.Round(spawnPos.x)
                , Mathf.Round(spawnPos.y), 0));
        }
        else if (objectToSpawn == ObjectType.GravityInverter)
        {
            spawnedObject.GetComponent<GravitySwitch>().Initialize(new Vector3
                (Mathf.Round(spawnPos.x)
                , Mathf.Round(spawnPos.y), 0));
        }
        else if (objectToSpawn == ObjectType.HorizontalInverter)
        {
            spawnedObject.GetComponent<HorizontalSwitch>().Initialize(new Vector3
                (Mathf.Round(spawnPos.x)
                , Mathf.Round(spawnPos.y), 0));
        }
        else if (objectToSpawn == ObjectType.TriggerableBlock)
        {
            spawnedObject.GetComponent<TriggerableBlocks>().Initialize(new Vector3
                (Mathf.Round(spawnPos.x)
                , Mathf.Round(spawnPos.y), 0));
        }
    }

    // ************************************************************************************
    // GUI
    // ************************************************************************************

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

            if (GUI.Button(new Rect(10, Screen.height - 70, 100, 30), "Tile"))
            {
                Debug.Log("Spawn tile clicked");
                objectToSpawn = ObjectType.Tile;
            }
            if (GUI.Button(new Rect(110, Screen.height - 70, 100, 30), "Hazard"))
            {
                Debug.Log("Spawn hazard clicked");
                objectToSpawn = ObjectType.Hazard;
            }
            if (GUI.Button(new Rect(210, Screen.height - 70, 100, 30), "Minibot"))
            {
                Debug.Log("Spawn minibot clicked");
                objectToSpawn = ObjectType.Minibot;
            }
            if (GUI.Button(new Rect(310, Screen.height - 70, 100, 30), "Box"))
            {
                Debug.Log("Spawn box clicked");
                objectToSpawn = ObjectType.Box;
            }
            if (GUI.Button(new Rect(410, Screen.height - 70, 100, 30), "Door"))
            {
                Debug.Log("Spawn door clicked");
                objectToSpawn = ObjectType.Door;
            }        

            // 2nd row
            if (GUI.Button(new Rect(10, Screen.height - 40, 100, 30), "Switch"))
            {
                Debug.Log("Spawn switch clicked");
                objectToSpawn = ObjectType.Switch;
            }
            if (GUI.Button(new Rect(110, Screen.height - 40, 100, 30), "StepSwitch"))
            {
                Debug.Log("Spawn stepSwitch clicked");
                objectToSpawn = ObjectType.StepSwitch;
            }            
            if (GUI.Button(new Rect(210, Screen.height - 40, 100, 30), "GravInv"))
            {
                Debug.Log("Spawn gravity inverter clicked");
                objectToSpawn = ObjectType.GravityInverter;
            }            
            if (GUI.Button(new Rect(310, Screen.height - 40, 100, 30), "HorInv"))
            {
                Debug.Log("Spawn horizontal inverter clicked");
                objectToSpawn = ObjectType.HorizontalInverter;
            }
            if (GUI.Button(new Rect(410, Screen.height - 40, 100, 30), "TrigBlocks"))
            {
                Debug.Log("Spawn triggerableBlocks clicked");
                objectToSpawn = ObjectType.TriggerableBlock;
            }
            //if (GUI.Button(new Rect(510, Screen.height - 40, 100, 30), "MovPlatform"))
            //{
            //    Debug.Log("Spawn moving platform clicked");
            //    objectToSpawn = ObjectType.MovingPlatform;
            //}

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
}
