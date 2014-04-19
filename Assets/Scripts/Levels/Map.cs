using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

    public GameObject levelObjectsContainer;
	public GameObject tilesContainer;
    public GameObject minibotsContainer;
	public GameObject boxesContainer;
	public GameObject doorsContainer;
	public GameObject gravityInvertersContainer;
	public GameObject hazardsContainer;
	public GameObject horizontalInvertersContainer;
	public GameObject movingPlatformsContainer;
	public GameObject stepSwitchesContainer;
	public GameObject switchesContainer;
	public GameObject triggerableBlocksContainer;
    public GameObject triggerableHazardsContainer;
    private List<Transform> levelObjectContainerList = new List<Transform>();

    public XMLLevelReader levelReader;
    public XMLLevelWriter levelWriter;
    public string currentLevel = "";    

    // ************************************************************************************
    // MAIN
    // ************************************************************************************

	// Use this for initialization
	void Awake () {
		Registry.map = this;
		
        levelObjectsContainer = gameObject.transform.FindChild("LevelObjects").gameObject;
        if (levelObjectsContainer == null)
            Debug.LogError("levelObjectsContainer not found!");
		tilesContainer = levelObjectsContainer.transform.FindChild("Tiles").gameObject;
        if (tilesContainer == null)
            Debug.LogError("tilesContainer not found!");
        minibotsContainer = GameObject.Find("Minibots");
        if (minibotsContainer == null)
            Debug.LogError("minibotsContainer not found!");
		boxesContainer = levelObjectsContainer.transform.FindChild("Boxes").gameObject;
		if (boxesContainer == null)
			Debug.LogError("boxesContainer not found!");
		doorsContainer = levelObjectsContainer.transform.FindChild("Doors").gameObject;
		if (doorsContainer == null)
			Debug.LogError("doorsContainer not found!");
		gravityInvertersContainer = levelObjectsContainer.transform.FindChild("GravityInverters").gameObject;
		if (gravityInvertersContainer == null )
			Debug.LogError("gravityInvertersContainer not found!");
		hazardsContainer = levelObjectsContainer.transform.FindChild("Hazards").gameObject;
		if (hazardsContainer == null )
			Debug.LogError("hazardsContainer not found!");
		horizontalInvertersContainer = levelObjectsContainer.transform.FindChild("HorizontalInverters").gameObject;
		if (horizontalInvertersContainer == null)
			Debug.LogError("horizontalInvertersContainer not found!");
		movingPlatformsContainer = levelObjectsContainer.transform.FindChild("MovingPlatforms").gameObject;
		if (movingPlatformsContainer == null)
			Debug.LogError("movingPlatformsContainer not found!");
		stepSwitchesContainer = levelObjectsContainer.transform.FindChild("StepSwitches").gameObject;
		if (stepSwitchesContainer == null )
			Debug.LogError("stepSwitchesContainer not found!");
		switchesContainer = levelObjectsContainer.transform.FindChild("Switches").gameObject;
		if (switchesContainer == null)
			Debug.LogError("switchesContainer not found!");
		triggerableBlocksContainer = levelObjectsContainer.transform.FindChild("TriggerableBlocks").gameObject;
		if (triggerableBlocksContainer == null)
			Debug.LogError("triggerableBlocksContainer not found!");
        triggerableHazardsContainer = levelObjectsContainer.transform.FindChild("TriggerableHazards").gameObject;
        if (triggerableHazardsContainer == null)
            Debug.LogError("triggerableHazardsContainer not found!");
        PopulateLevelObjectContainerList();

        levelReader = gameObject.GetComponent<XMLLevelReader>();
        if (levelReader == null)
            Debug.LogError("levelReader is not found!");
        levelWriter = gameObject.GetComponent<XMLLevelWriter>();
        if (levelWriter == null)
            Debug.LogError("levelWriter is not found!");
	}

    private void PopulateLevelObjectContainerList()
    {
        levelObjectContainerList.Add(doorsContainer.transform);
        levelObjectContainerList.Add(triggerableBlocksContainer.transform);
        levelObjectContainerList.Add(tilesContainer.transform);
        levelObjectContainerList.Add(hazardsContainer.transform);
        levelObjectContainerList.Add(minibotsContainer.transform);
        levelObjectContainerList.Add(boxesContainer.transform);
        levelObjectContainerList.Add(switchesContainer.transform);
        levelObjectContainerList.Add(stepSwitchesContainer.transform);
        levelObjectContainerList.Add(gravityInvertersContainer.transform);
        levelObjectContainerList.Add(horizontalInvertersContainer.transform);
        levelObjectContainerList.Add(triggerableHazardsContainer.transform);
        //levelObjectContainerList.Add(movingPlatformsContainer.transform);
    }

    // ************************************************************************************
    // LEVEL LOADING
    // ************************************************************************************

    public bool GetNextLevel()
    {
        int levelToCheck = 0;
        try
        {
            levelToCheck = int.Parse(currentLevel);
        }
        catch
        {
            Debug.LogWarning("Error in getting the next level! Could not parse currentLevel to int!");
        }

        levelToCheck++;
        while (levelToCheck < 100)
        {
            if (levelReader.CheckIfFileExists(levelToCheck.ToString()))
            {   // If file exists
                
                ClearLevel();       // Clear the kevek
                levelReader.LoadLevel(levelToCheck.ToString());
                break;              // We get out of this loop
            }

            levelToCheck++;
        } 

        return true;
    }

	public bool GetPreviousLevel()
    {
        int levelToCheck = 0;
        try
        {
            levelToCheck = int.Parse(currentLevel);
        }
        catch
        {
            Debug.LogWarning("Error in getting the next level! Could not parse currentLevel to int!");
        }

        levelToCheck--;
        while (levelToCheck > 0)
        {
            if (levelReader.CheckIfFileExists(levelToCheck.ToString()))
            {   // If file exists

                ClearLevel();       // Clear the kevek
                levelReader.LoadLevel(levelToCheck.ToString());
                break;              // We get out of this loop
            }

            levelToCheck--;
        }

        return true;
    }

    // ************************************************************************************
    // LEVEL MANIPULATION
    // ************************************************************************************
	public void RestartLevel()
    {
		ResetObjectsInContainer(boxesContainer.transform);
		ResetObjectsInContainer(minibotsContainer.transform);
		ResetObjectsInContainer(stepSwitchesContainer.transform);
		ResetObjectsInContainer(doorsContainer.transform);
		ResetObjectsInContainer(triggerableBlocksContainer.transform);
    }

    public void ClearLevel()
    {
		RemoveAllChildrenOfContainer(tilesContainer.transform);
		RemoveAllChildrenOfContainer(hazardsContainer.transform);
		RemoveAllChildrenOfContainer(boxesContainer.transform);
		RemoveAllChildrenOfContainer(minibotsContainer.transform);
		RemoveAllChildrenOfContainer(doorsContainer.transform);
		RemoveAllChildrenOfContainer(triggerableBlocksContainer.transform);
		RemoveAllChildrenOfContainer(triggerableHazardsContainer.transform);
		RemoveAllChildrenOfContainer(switchesContainer.transform);
		RemoveAllChildrenOfContainer(stepSwitchesContainer.transform);
		RemoveAllChildrenOfContainer(gravityInvertersContainer.transform);
		RemoveAllChildrenOfContainer(horizontalInvertersContainer.transform);
		RemoveAllChildrenOfContainer(movingPlatformsContainer.transform);
    }

	private void ResetObjectsInContainer(Transform container)
	{
		foreach (Transform transform in container.transform)
		{
			transform.GetComponent<LevelObject>().ResetObject();
		}
	}

	private void RemoveAllChildrenOfContainer(Transform container)
	{
		for (var i = container.childCount - 1 ; i >= 0 ; i--)
		{
			Transform objectToDestroy = container.GetChild(i);
			GameObject.Destroy(objectToDestroy.gameObject);
			objectToDestroy.parent = null;
		}
	}

	public void UpdateNeighborsForAllWallTiles ()
	{
		foreach( Transform tileObject in tilesContainer.transform )
		{
			TileFrontManager tileFrontManager = tileObject.GetComponent<Tile>().tileFrontManager;
			if ( tileFrontManager != null )
				tileFrontManager.UpdateNeighbors();
		}
	}

    // ************************************************************************************
    // LEVEL OBJECT PICKING
    // ************************************************************************************

    private LevelObject GetLevelObject(Transform containerToCheck, Vector3 posToCheck)
    {
        foreach (Transform theTransform in containerToCheck)
        {
            if (theTransform.position == posToCheck)
            {
                return theTransform.gameObject.GetComponent<LevelObject>();
            }
        }

        return null;
    }

	public LevelObject GetLevelObjectAtPosition(Vector3 posToCheck)
	{		
        LevelObject theLevelObject;

        foreach(Transform containerToCheck in levelObjectContainerList)
        {
            theLevelObject = GetLevelObject(containerToCheck, posToCheck);
            if (theLevelObject != null)
                return theLevelObject;
        }
		
		return null;
	}    
}
