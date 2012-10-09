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
		tilesContainer = Registry.map.tilesContainer;
		minibotsContainer = Registry.map.minibotsContainer;
		boxesContainer = Registry.map.boxesContainer;
		doorsContainer = Registry.map.doorsContainer;
		gravityInvertersContainer = Registry.map.gravityInvertersContainer;
		hazardsContainer = Registry.map.hazardsContainer;
		horizontalInvertersContainer = Registry.map.horizontalInvertersContainer;
		movingPlatformsContainer = Registry.map.movingPlatformsContainer;
		switchesCointainer = Registry.map.switchesCointainer;
		triggerableBlocksContainer = Registry.map.triggerableBlocksContainer;
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
