using UnityEngine;
using System.Collections;

public class LevelEditor : MonoBehaviour {

    enum ObjectType { None, Tile, Minibot, Box, Door, GravityInverter, Hazard, TriggerableHazard, HorizontalInverter
        , MovingPlatform, StepSwitch, Switch, TriggerableBlock };
    public enum LevelEditorMode { PlacementMode, DeletionMode, EditingMode, PickToEditMode, PickToLinkMode, None };

    bool mapEditMode = false;

    public bool MapEditMode
    {
        set { mapEditMode = value; }
        get { return mapEditMode; }
    }
    string levelFileName;
    public string LevelFileName
    {
        set { levelFileName = value; }
    }
    bool isSimulating = true;

    LevelEditorMode currentMode = LevelEditorMode.PlacementMode;
    public LevelEditorMode CurrentMode
    {
        get { return currentMode; }
        set { currentMode = value; }
    }
    
    XMLLevelReader levelReader;
    XMLLevelWriter levelWriter;
    Renderer originMarkerRenderer;
    ObjectType objectToSpawn = ObjectType.None;
    Map map;
    LevelObject objectToDisplay;

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
        
        InitializeOriginMarker();
    }

    void Update()
    {        
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        // Only allow map editing if the game is not simulating
        if (!isSimulating && mapEditMode)
        {
            // If a mouse click is detected while in edit mode
            if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
            {
                // We get any levelObject at the position from where the mouse is clicked                
                LevelObject objectAtMousePosition = GetObjectAtMousePosition();

                // This handles the levelObjectPlacement
                if (objectToSpawn != ObjectType.None                    // We check if we have something to spawn
                    && CurrentMode == LevelEditorMode.PlacementMode   // We also check if the current mode is objectPlacement
                    && objectAtMousePosition == null)                  // Finally we check if there is an object at the current position
                {
                    // If there is none, then continue the object placement
                    HandleLevelObjectPlacement();
                }

                // This handles the picking of level Objects for editing
                else if (objectAtMousePosition != null
                    && CurrentMode == LevelEditorMode.PickToEditMode
                    && CurrentMode != LevelEditorMode.PickToLinkMode )     // If there is an object
                {
                    // We open the attribute window
                    OpenAttributeWindow(objectAtMousePosition);
                }

                // This handles the picking of level objects for linking
                else if (CurrentMode == LevelEditorMode.PickToLinkMode)
                {
                    objectToDisplay.GetComponent<Switch>().SetObjectToActivate(objectAtMousePosition);
                    CurrentMode = LevelEditorMode.EditingMode;
                }
            }

            // Pressing the right mouse button deletes the object at mouse position
            else if (Input.GetMouseButtonDown(1) && GUIUtility.hotControl == 0)
            {
                HandleLevelObjectDeletion();
            }

            // Pressing the middle mouse button edits the object at mouse position
            else if (Input.GetMouseButtonDown(2) && GUIUtility.hotControl == 0)
            {
                // We get any levelObjsect at the position from where the mouse is clicked                
                LevelObject objectAtMousePosition = GetObjectAtMousePosition();

                OpenAttributeWindow(objectAtMousePosition);
            }
        }

        // This only shows the origin marker when mapEditMode is enabled
        if (mapEditMode)
        {
            Camera.main.orthographic = true;
            originMarkerRenderer.enabled = true;
        }
        else
        {
            Camera.main.orthographic = false;
            originMarkerRenderer.enabled = false;
        }
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
    // LEVEL EDITING
    // ************************************************************************************
    private void HandleLevelObjectDeletion()
    {
        LevelObject clickedObject = GetObjectAtMousePosition();
        
        if (clickedObject == null || clickedObject is SuroundingTile)
            return;

		//TODO: Add a destroy funciton in LevelObject
		GameObject.Destroy(clickedObject.gameObject);
		clickedObject.transform.parent = null;

        if (clickedObject is Minibot)
            Registry.main.GetMinibotsInLevel();     // We tell main to recount the number of minibots

		//TODO: For perfomance, consider creating a fucntion that would update the neighbors form a specific position only
		if ( clickedObject is Tile )
			Registry.map.UpdateNeighborsForAllWallTiles();
    }

    private LevelObject GetObjectAtMousePosition()
    {
        Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return map.GetLevelObjectAtPosition(new Vector3
            (Mathf.Round(clickPos.x)
            , Mathf.Round(clickPos.y), 0));
    }

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
        else if (objectToSpawn == ObjectType.TriggerableHazard)
        {
            prefabToSpawn = Registry.prefabHandler.pfTriggerableHazard;
            parentTransform = Registry.map.triggerableHazardsContainer.transform;
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
			Registry.map.UpdateNeighborsForAllWallTiles();
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
            Registry.main.GetMinibotsInLevel();     // We tell main to recount the number of minibots
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
                , Mathf.Round(spawnPos.y), 0), false, new Vector2(1,1));
        }
        else if (objectToSpawn == ObjectType.TriggerableHazard)
        {
            spawnedObject.GetComponent<TriggerableHazard>().Initialize(new Vector3
                  (Mathf.Round(spawnPos.x)
                  , Mathf.Round(spawnPos.y), 0), false, new Vector2(1, 1));
        }
    }

    // ************************************************************************************
    // Attribute Window
    // ************************************************************************************

    private void OpenAttributeWindow(LevelObject objectAtMousePosition)
    {
        Debug.Log("Attribute window opened");
        SetCurrentMode(LevelEditorMode.EditingMode);
        PlaceInAttributeWindow(objectAtMousePosition);
    }

    /// <summary>
    /// Places the values of received object onto the atribute window
    /// </summary>
    /// <param name="objectAtMousePosition"></param>
    private void PlaceInAttributeWindow(LevelObject theObjectToDisplay)
    {
        objectToDisplay = theObjectToDisplay;
    }

    // ************************************************************************************
    // GUI
    // ************************************************************************************
    void OnGUI()
    {
        // Play and Stop Buttons
        string btnText = "";
        if (isSimulating)
            btnText = "Stop";
        else
            btnText = "Play";        
        if (GUI.Button(new Rect(10, 10, 100, 30), btnText))
        {
            if (isSimulating)
                StopSimulation();
            else
                StartSimulation();
        }

        // Displays the current level
        // GUI.Label(new Rect(Screen.width - 80, 10, 70, 20), "Level: " + map.currentLevel);

        GUI.enabled = true;

        MapEditMode = GUI.Toggle(new Rect(10, 50, 100, 20), MapEditMode, "Map Edit Mode");

        if (MapEditMode)
        {
            HandleEditingModeGUI();

            // Disable the GUI if the game is simulating
            if (isSimulating || currentMode != LevelEditorMode.PlacementMode)
                GUI.enabled = false;
            else
                GUI.enabled = true;

            // Make a background box for the spawning buttons
            GUI.Box(new Rect(0, Screen.height-100, 620, 100), "Level Objects");

            if (GUI.Button(new Rect(10, Screen.height - 140, 100, 30), "Edit Object"))
            {
                Debug.Log("Edit tile clicked");
                SetCurrentMode(LevelEditorMode.PickToEditMode);
            }

            // Spawning buttons
            if (GUI.Button(new Rect(10, Screen.height - 70, 100, 30), "Tile"))
            {
                Debug.Log("Spawn tile clicked");
                SetCurrentMode(LevelEditorMode.PlacementMode);
                objectToSpawn = ObjectType.Tile;
            }
            if (GUI.Button(new Rect(110, Screen.height - 70, 100, 30), "Hazard"))
            {
                Debug.Log("Spawn hazard clicked");
                SetCurrentMode(LevelEditorMode.PlacementMode);
                objectToSpawn = ObjectType.Hazard;
            }
            if (GUI.Button(new Rect(210, Screen.height - 70, 100, 30), "Minibot"))
            {
                Debug.Log("Spawn minibot clicked");
                SetCurrentMode(LevelEditorMode.PlacementMode);
                objectToSpawn = ObjectType.Minibot;
            }
            if (GUI.Button(new Rect(310, Screen.height - 70, 100, 30), "Box"))
            {
                Debug.Log("Spawn box clicked");
                SetCurrentMode(LevelEditorMode.PlacementMode);
                objectToSpawn = ObjectType.Box;
            }
            if (GUI.Button(new Rect(410, Screen.height - 70, 100, 30), "Door"))
            {
                Debug.Log("Spawn door clicked");
                SetCurrentMode(LevelEditorMode.PlacementMode);
                objectToSpawn = ObjectType.Door;
            }        

            // 2nd row
            if (GUI.Button(new Rect(10, Screen.height - 40, 100, 30), "Switch"))
            {
                Debug.Log("Spawn switch clicked");
                SetCurrentMode(LevelEditorMode.PlacementMode);
                objectToSpawn = ObjectType.Switch;
            }
            if (GUI.Button(new Rect(110, Screen.height - 40, 100, 30), "StepSwitch"))
            {
                Debug.Log("Spawn stepSwitch clicked");
                SetCurrentMode(LevelEditorMode.PlacementMode);
                objectToSpawn = ObjectType.StepSwitch;
            }            
            if (GUI.Button(new Rect(210, Screen.height - 40, 100, 30), "GravInv"))
            {
                Debug.Log("Spawn gravity inverter clicked");
                SetCurrentMode(LevelEditorMode.PlacementMode);
                objectToSpawn = ObjectType.GravityInverter;
            }            
            if (GUI.Button(new Rect(310, Screen.height - 40, 100, 30), "HorInv"))
            {
                Debug.Log("Spawn horizontal inverter clicked");
                SetCurrentMode(LevelEditorMode.PlacementMode);
                objectToSpawn = ObjectType.HorizontalInverter;
            }
            if (GUI.Button(new Rect(410, Screen.height - 40, 100, 30), "TrigBlocks"))
            {
                Debug.Log("Spawn triggerableBlocks clicked");
                SetCurrentMode(LevelEditorMode.PlacementMode);
                objectToSpawn = ObjectType.TriggerableBlock;
            }
            if (GUI.Button(new Rect(510, Screen.height - 40, 100, 30), "TrigHazard"))
            {
                Debug.Log("Spawn triggerableHazard clicked");
                SetCurrentMode(LevelEditorMode.PlacementMode);
                objectToSpawn = ObjectType.TriggerableHazard;
            }
            //if (GUI.Button(new Rect(510, Screen.height - 40, 100, 30), "MovPlatform"))
            //{
            //    Debug.Log("Spawn moving platform clicked");
            //    objectToSpawn = ObjectType.MovingPlatform;
            //}

            // We then enable the GUI of the rest
            GUI.enabled = true;

            // Clears the whole map
            if (GUI.Button(new Rect(Screen.width - 110, 40, 100, 30), "New Map"))
            {
                Debug.Log("New map clicked");
                map.ClearLevel();
                if (levelReader.CheckIfFileExists("template"))
                {
                    levelReader.LoadLevel("template");
                }
            }
            if (GUI.Button(new Rect(Screen.width - 110, 70, 100, 30), "Clear Map"))
            {
                Debug.Log("Clear map clilcked");
                map.ClearLevel();
            }
            
            // Save map mode
            levelFileName = GUI.TextField(new Rect(Screen.width - 110, 120, 100, 30), levelFileName);
            if (GUI.Button(new Rect(Screen.width - 110, 150, 100, 30), "Save Map"))
            {
                Debug.Log("Save map clicked");
                levelWriter.SaveLevel(levelFileName);
                map.currentLevel = levelFileName;
            }

            // Load map mode
            levelFileName = GUI.TextField(new Rect(Screen.width - 110, 200, 100, 30), levelFileName);
            if (GUI.Button(new Rect(Screen.width - 110, 230, 100, 30), "Load Map"))
            {
                Debug.Log("Load map clicked");
                map.ClearLevel();
                if (levelReader.CheckIfFileExists(levelFileName))
                {
                    levelReader.LoadLevel(levelFileName);
                }
            }

            if (GUI.Button(new Rect(Screen.width - 50, 270, 40, 30), ">>"))
            {
                if ( map.GetNextLevel() )
                {
                    Debug.Log("Next map clicked");
                }
            }

            if (GUI.Button(new Rect(Screen.width - 110, 270, 40, 30), "<<"))
            {
                if (map.GetPreviousLevel())
                {
                    Debug.Log("Previous map clicked");
                }
            }
        }
    }

    private void HandleEditingModeGUI()
    {
        // This handles the GUI for the editing mode
        if (currentMode == LevelEditorMode.EditingMode)
        {
            GUI.Box(new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 150, 300, 300), "Edit Object");

            if (objectToDisplay != null)
            {
                objectToDisplay.GetComponent<LevelObject>().GetEditableAttributes(this);
            }
            else
            {
                GUI.Label(new Rect((Screen.width / 2), (Screen.height / 2) - 110, 100, 30), "Nothing to edit");
            }

            // Cancel button
            if (GUI.Button(new Rect((Screen.width / 2) -50, (Screen.height / 2) + 110, 100, 30), "Close"))
            {
                Debug.Log("Close window clicked");
                SetCurrentMode(LevelEditorMode.PlacementMode);
            }
        }
    }

    private void SetCurrentMode(LevelEditorMode theNewMode)
    {
        currentMode = theNewMode;

        // We stop the simulation only if it is objectPlacement mode
        //if ( theNewMode == LevelEditorMode.ObjectPlacement )
        //    StopSimulation();
    }

    private void StartSimulation()
    {
        Registry.replayManager.StartRecording();
        
        Time.timeScale = 1;
        isSimulating = true;
    }

    private void StopSimulation()
    {
        Registry.replayManager.StopRecording();

        Time.timeScale = 0;
        isSimulating = false;
        map.RestartLevel();
    }
}
