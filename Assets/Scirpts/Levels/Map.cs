using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	
	internal GameObject tilesContainer;
    internal GameObject minibotsContainer;
	internal GameObject boxesContainer;
	internal GameObject doorsContainer;
	internal GameObject gravityInvertersContainer;
	internal GameObject hazardsContainer;
	internal GameObject horizontalInvertersContainer;
	internal GameObject movingPlatformsContainer;
	internal GameObject stepSwitchesContainer;
	internal GameObject switchesCointainer;
	internal GameObject triggerableBlocksContainer;
	
	// Use this for initialization
	void Awake () {
		Registry.map = this;
		
		tilesContainer = gameObject.transform.FindChild("Tiles").gameObject;
        if (tilesContainer == null)
            Debug.LogError("tilesContainer not found!");
        minibotsContainer = GameObject.Find("Minibots");
        if (minibotsContainer == null)
            Debug.LogError("minibotsContainer not found!");
		boxesContainer = gameObject.transform.FindChild("Boxes").gameObject;
		if (boxesContainer == null)
			Debug.LogError("boxesContainer not found!");
		doorsContainer = gameObject.transform.FindChild("Doors").gameObject;
		if (doorsContainer == null)
			Debug.LogError("doorsContainer not found!");
		gravityInvertersContainer = gameObject.transform.FindChild("GravityInverters").gameObject;
		if (gravityInvertersContainer == null )
			Debug.LogError("gravityInvertersContainer not found!");
		hazardsContainer = gameObject.transform.FindChild("Hazards").gameObject;
		if (hazardsContainer == null )
			Debug.LogError("hazardsContainer not found!");
		horizontalInvertersContainer = gameObject.transform.FindChild("HorizontalInverters").gameObject;
		if (horizontalInvertersContainer == null)
			Debug.LogError("horizontalInvertersContainer not found!");
		movingPlatformsContainer = gameObject.transform.FindChild("MovingPlatforms").gameObject;
		if (movingPlatformsContainer == null)
			Debug.LogError("movingPlatformsContainer not found!");
		stepSwitchesContainer = gameObject.transform.FindChild("StepSwitches").gameObject;
		if (stepSwitchesContainer == null )
			Debug.LogError("stepSwitchesContainer not found!");
		switchesCointainer = gameObject.transform.FindChild("Switches").gameObject;
		if (switchesCointainer == null)
			Debug.LogError("switchesContainer not found!");
		triggerableBlocksContainer = gameObject.transform.FindChild("TriggerableBlocks").gameObject;
		if (triggerableBlocksContainer == null)
			Debug.LogError("triggerableBlocksContainer not found!");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	internal LevelObject GetLObjectAtPosition(Vector3 posToCheck)
	{
		return null;
	}
}