using UnityEngine;
using System.Collections;
using System.IO;

public class XMLAccessor : MonoBehaviour {
    	
	public static string padZeroesIfNumberedLevel (string levelString)
	{
		int num = 0;
		if ( int.TryParse(levelString, out num))
			levelString = int.Parse(levelString).ToString("000");
		
		return levelString;
	}

	protected GameObject tilesContainer;
    protected GameObject minibotsContainer;
	protected GameObject boxesContainer;
	protected GameObject doorsContainer;
	protected GameObject gravityInvertersContainer;
	protected GameObject hazardsContainer;
	protected GameObject horizontalInvertersContainer;
	protected GameObject movingPlatformsContainer;
	protected GameObject stepSwitchesContainer;
	protected GameObject switchesContainer;
	protected GameObject triggerableBlocksContainer;
    protected GameObject triggerableHazardsContainer;
	
	virtual protected void Awake()
	{
		tilesContainer = Registry.map.tilesContainer;
		minibotsContainer = Registry.map.minibotsContainer;
		boxesContainer = Registry.map.boxesContainer;
		doorsContainer = Registry.map.doorsContainer;
		gravityInvertersContainer = Registry.map.gravityInvertersContainer;
		hazardsContainer = Registry.map.hazardsContainer;
		horizontalInvertersContainer = Registry.map.horizontalInvertersContainer;
		movingPlatformsContainer = Registry.map.movingPlatformsContainer;
		stepSwitchesContainer = Registry.map.stepSwitchesContainer;
		switchesContainer = Registry.map.switchesContainer;
		triggerableBlocksContainer = Registry.map.triggerableBlocksContainer;
        triggerableHazardsContainer = Registry.map.triggerableHazardsContainer;
	}

    public bool CheckIfFileExists(string fileName)
    {
		fileName = padZeroesIfNumberedLevel(fileName);

        string filepath = Application.dataPath + @"/Resources/Levels/" + fileName + ".xml";

        // If file does not exist. Create the xml file.
        if (!File.Exists(filepath))
        {
            Debug.LogWarning("Xml at " + filepath + " does not exist!");
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
