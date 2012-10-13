using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

    internal GameObject levelObjectsContainer;
	internal GameObject tilesContainer;
    internal GameObject minibotsContainer;
	internal GameObject boxesContainer;
	internal GameObject doorsContainer;
	internal GameObject gravityInvertersContainer;
	internal GameObject hazardsContainer;
	internal GameObject horizontalInvertersContainer;
	internal GameObject movingPlatformsContainer;
	internal GameObject stepSwitchesContainer;
	internal GameObject switchesContainer;
	internal GameObject triggerableBlocksContainer;

    internal XMLLevelReader levelReader;
    internal XMLLevelWriter levelWriter;
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

        levelReader = gameObject.GetComponent<XMLLevelReader>();
        if (levelReader == null)
            Debug.LogError("levelReader is not found!");
        levelWriter = gameObject.GetComponent<XMLLevelWriter>();
        if (levelWriter == null)
            Debug.LogError("levelWriter is not found!");
	}

    // ************************************************************************************
    // LEVEL MANIPULATION
    // ************************************************************************************

    /// <summary>
    /// Restarts the level. All level objects goes back to their original positions and states
    /// </summary>
    internal void RestartLevel()
    {
        foreach (Transform box in boxesContainer.transform)
        {
            box.GetComponent<Box>().ResetObject();
        }
        foreach (Transform minibot in minibotsContainer.transform)
        {
            minibot.GetComponent<Minibot>().ResetObject();
        }
        foreach (Transform door in doorsContainer.transform)
        {
            door.GetComponent<Door>().ResetObject();
        }
        foreach (Transform triggerableBlock in triggerableBlocksContainer.transform)
        {
            triggerableBlock.GetComponent<TriggerableBlocks>().ResetObject();
        }
    }

    /// <summary>
    /// Clears the whole level of all levelObjects
    /// </summary>
    internal void ClearLevel()
    {
        foreach (Transform tile in tilesContainer.transform)
        {
            GameObject.Destroy(tile.gameObject);
        }
        foreach (Transform hazard in hazardsContainer.transform)
        {
            GameObject.Destroy(hazard.gameObject);
        }
        foreach (Transform box in boxesContainer.transform)
        {
            GameObject.Destroy(box.gameObject);
        }
        foreach (Transform minibot in minibotsContainer.transform)
        {
            GameObject.Destroy(minibot.gameObject);
        }
        foreach (Transform door in doorsContainer.transform)
        {
            GameObject.Destroy(door.gameObject);
        }
        foreach (Transform triggerableBlock in triggerableBlocksContainer.transform)
        {
            GameObject.Destroy(triggerableBlock.gameObject);
        }
        foreach (Transform aSwitch in switchesContainer.transform)
        {
            GameObject.Destroy(aSwitch.gameObject);
        }
        foreach (Transform stepSwitch in stepSwitchesContainer.transform)
        {
            GameObject.Destroy(stepSwitch.gameObject);
        }
        foreach (Transform gravityInverter in gravityInvertersContainer.transform)
        {
            GameObject.Destroy(gravityInverter.gameObject);
        }
        foreach (Transform horizontalInverter in horizontalInvertersContainer.transform)
        {
            GameObject.Destroy(horizontalInverter.gameObject);
        }
        foreach (Transform movingPlatform in movingPlatformsContainer.transform)
        {
            GameObject.Destroy(movingPlatform.gameObject);
        }
    }

    // ************************************************************************************
    // HELPER FUNCTIONS
    // ************************************************************************************

    /// <summary>
    /// A helper function that gets a levelObject at the specified position
    /// </summary>
    /// <param name="posToCheck">The position to check</param>
    /// <returns></returns>
	internal LevelObject GetLevelObjectAtPosition(Vector3 posToCheck)
	{
		// We loop through doors
		foreach ( Transform door in doorsContainer.transform )
		{
			if ( door.position == posToCheck )
			{
				return door.gameObject.GetComponent<LevelObject>();	
			}
		}
		foreach ( Transform triggerableBlock in triggerableBlocksContainer.transform )
		{
			if ( triggerableBlock.position == posToCheck )
			{
				return triggerableBlock.gameObject.GetComponent<LevelObject>();	
			}
		}
		
		return null;
	}
}
