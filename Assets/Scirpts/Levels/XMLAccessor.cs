using UnityEngine;
using System.Collections;
using System.IO;

public class XMLAccessor : MonoBehaviour {

	protected Object pfTile;
    protected Object pfMinibot;
	protected Object pfBox;
	protected Object pfDoor;
	protected Object pfGravityInverter;
	protected Object pfHazard;
	protected Object pfHorizontalInverter;
	protected Object pfMovingPlatform;
	protected Object pfStepSwitch;
	protected Object pfSwitch;
	protected Object pfTriggerableBlock;
	
	protected GameObject tilesContainer;
    protected GameObject minibotsContainer;
	protected GameObject boxesContainer;
	protected GameObject doorsContainer;
	protected GameObject gravityInvertersContainer;
	protected GameObject hazardsContainer;
	protected GameObject horizontalInvertersContainer;
	protected GameObject movingPlatformsContainer;
	protected GameObject stepSwitchesContainer;
	protected GameObject switchesCointainer;
	protected GameObject triggerableBlocksContainer;
	
	void Awake()
	{
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
		
		Debug.Log("XMLACCESSOR minibots container is " + minibotsContainer );
	}
	
    protected bool CheckIfFileExists(string filepath)
    {
        // If file does not exist. Create the xml file.
        if (!File.Exists(filepath))
        {
            Debug.LogWarning("Xml does not exist!");
            return false;
        }

        return true;
    }

    protected string BoolToString(bool theParameter)
    {
        if (theParameter)
            return "1";
        else
            return "0";
    }

    protected bool StringToBool(string theValue)
    {
        if (theValue == "0")
            return false;
        else if (theValue == "1")
            return true;

        return false;
    }
}
