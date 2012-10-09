using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class XMLLevelWriter : XMLAccessor {

    GameObject tilesContainer;
    GameObject minibotsContainer;
	GameObject boxesContainer;
	GameObject doorsContainer;
	GameObject gravityInvertersContainer;
	GameObject hazardsContainer;
	GameObject horizontalInvertersContainer;
	GameObject movingPlatformsContainer;
	GameObject stepSwitchesContainer;
	GameObject switchesCointainer;
	GameObject triggerableBlocksContainer;

    void Start()
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
    }

    internal void SaveLevel(string filename)
    {
        string filepath = Application.dataPath + @"/Levels/" + filename + ".xml";
        Debug.Log(filepath);

        // We first check if file exists
        if (!CheckIfFileExists(filepath))
        {
            // if not, we create the file
            Debug.Log("Creating a new XML file.");

            FileStream fs = File.Create(filepath);
            StreamWriter writer = new StreamWriter(fs);
            writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            writer.WriteLine("<tiles>");
            writer.Write("</tiles>");
            writer.Flush();
            fs.Close();
        }

        // Then we proceed with the saving
        XmlDocument xmlDoc = new XmlDocument();
        XmlElement elemNew;

        xmlDoc.Load(filepath);
        Debug.Log("XML loaded.");

        XmlElement elemRoot = xmlDoc.DocumentElement;
        elemRoot.RemoveAll();                               // Remove all

        // We loop through all the minibots      
        foreach (Transform minibot in minibotsContainer.transform)
        {
            elemNew = xmlDoc.CreateElement("minibot");  // Create the rotation node
			Minibot minibotScript = minibot.gameObject.GetComponent<Minibot>();
            elemNew.SetAttribute("x", minibotScript.startingPos.x.ToString());
            elemNew.SetAttribute("y", minibotScript.startingPos.y.ToString());

            RigidBodyFPSController controllerScipt
                = minibot.GetComponent<RigidBodyFPSController>();
            string value = "";
            value = BoolToString(controllerScipt.InvertGravity);
            elemNew.SetAttribute("invertGravity", value);            

            value = BoolToString(controllerScipt.invertHorizontal);
            elemNew.SetAttribute("invertHorizontal", value);
            elemRoot.AppendChild(elemNew);                      // Make the transform node the parent
        }

        // We then loop through all the objects
        // First we loop through the tiles first        
        foreach (Transform tile in tilesContainer.transform)
        {
            elemNew = xmlDoc.CreateElement("tile");
			Tile tileScript = tile.gameObject.GetComponent<Tile>();
            elemNew.SetAttribute("x", tileScript.startingPos.x.ToString());
            elemNew.SetAttribute("y", tileScript.startingPos.y.ToString());
            elemRoot.AppendChild(elemNew);                      // Make the transform node the parent
        }
		
		// We then loop through all boxes
		foreach (Transform box in boxesContainer.transform)
		{
			elemNew = xmlDoc.CreateElement("box");
			LevelObject levelObjectScript = box.gameObject.GetComponent<LevelObject>();
			elemNew.SetAttribute("x", Mathf.Ceil(levelObjectScript.startingPos.x).ToString());
			elemNew.SetAttribute("y", Mathf.Ceil(levelObjectScript.startingPos.y).ToString());
			elemRoot.AppendChild(elemNew);
		}
		
		// We loop through all the doors
		foreach (Transform door in doorsContainer.transform)
		{
			elemNew = xmlDoc.CreateElement("door");
			LevelObject levelObjectScript = door.gameObject.GetComponent<LevelObject>();
			elemNew.SetAttribute("x", levelObjectScript.startingPos.x.ToString());
			elemNew.SetAttribute("y", levelObjectScript.startingPos.y.ToString());
			Door doorScript = door.GetComponent<Door>();
			elemNew.SetAttribute("isOpen", BoolToString(doorScript.isOpen));
			elemRoot.AppendChild(elemNew);
		}
		
		// We loop through all gravity inverters
		foreach (Transform gravityInverter in gravityInvertersContainer.transform)
		{
			elemNew = xmlDoc.CreateElement("gravityInverter");
			LevelObject levelObjectScript = gravityInverter.gameObject.GetComponent<LevelObject>();
			elemNew.SetAttribute("x", levelObjectScript.startingPos.x.ToString());
			elemNew.SetAttribute("y", levelObjectScript.startingPos.y.ToString());
			elemRoot.AppendChild(elemNew);
		}
		
		// We loop through all the hazards
		foreach (Transform hazard in hazardsContainer.transform)
		{
			elemNew = xmlDoc.CreateElement("hazard");
			Tile tileScript = hazard.gameObject.GetComponent<Tile>();
			elemNew.SetAttribute("x", tileScript.startingPos.x.ToString());
			elemNew.SetAttribute("y", tileScript.startingPos.y.ToString());
			elemRoot.AppendChild(elemNew);	
		}
		
		// We loop through all the horizontalInverters
		foreach (Transform horizontalInverter in horizontalInvertersContainer.transform)
		{
			elemNew = xmlDoc.CreateElement("horizontalInverter");			
			LevelObject levelObjectScript = horizontalInverter.gameObject.GetComponent<LevelObject>();
			elemNew.SetAttribute("x", levelObjectScript.startingPos.x.ToString());
			elemNew.SetAttribute("y", levelObjectScript.startingPos.y.ToString());
			elemRoot.AppendChild(elemNew);	
		}
		
		// We loop through all the movingPlatforms
//		foreach (Transform movingPlatform in movingPlatformsContainer.transform )
//		{
//			elemNew = xmlDoc.CreateElement("movingPlatform");
//			elemNew.SetAttribute("x", movingPlatform.position.x.ToString());
//			elemNew.SetAttribute("y", movingPlatform.position.y.ToString());
//			elemRoot.AppendChild(elemNew);	
//		}
		
		// We loop through all the stepSwitches
		foreach (Transform stepSwitch in stepSwitchesContainer.transform )
		{
			elemNew = xmlDoc.CreateElement("stepSwitch");
			LevelObject levelObjectScript = stepSwitch.gameObject.GetComponent<LevelObject>();
			elemNew.SetAttribute("x", levelObjectScript.startingPos.x.ToString());
			elemNew.SetAttribute("y", levelObjectScript.startingPos.y.ToString());
			elemRoot.AppendChild(elemNew);	
		}
		
		// We loop through all the switches
		foreach (Transform aSwitch in switchesCointainer.transform )
		{
			elemNew = xmlDoc.CreateElement("switch");
			LevelObject levelObjectScript = aSwitch.gameObject.GetComponent<LevelObject>();
			elemNew.SetAttribute("x", levelObjectScript.startingPos.x.ToString());
			elemNew.SetAttribute("y", levelObjectScript.startingPos.y.ToString());
			elemRoot.AppendChild(elemNew);	
		}
		
		// We loop through all the triggerableBlocks
		foreach (Transform triggerableBlock in triggerableBlocksContainer.transform )
		{
			elemNew = xmlDoc.CreateElement("triggerableBlock");
			LevelObject levelObjectScript = triggerableBlock.gameObject.GetComponent<LevelObject>();
			elemNew.SetAttribute("x", levelObjectScript.startingPos.x.ToString());
			elemNew.SetAttribute("y", levelObjectScript.startingPos.y.ToString());
			TriggerableBlocks tbScript = triggerableBlock.GetComponent<TriggerableBlocks>();
			elemNew.SetAttribute("isHidden", BoolToString(tbScript.isHidden));
			elemRoot.AppendChild(elemNew);		
		}
          
        xmlDoc.Save(filepath);
    }
}
