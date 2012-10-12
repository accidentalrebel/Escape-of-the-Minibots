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
	}
	
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
}
