using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class XMLLevelWriter : XMLAccessor {

    public void SaveLevel(string filename)
    {
		filename = XMLAccessor.padZeroesIfNumberedLevel(filename);

        string filepath = Application.dataPath + @"/Resources/Levels/" + filename + ".xml";

        // We first check if file exists
        if (!CheckIfFileExists(filename))
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
        XmlElement newXMLElement;

        xmlDoc.Load(filepath);
        Debug.Log("XML loaded.");

        XmlElement elemRoot = xmlDoc.DocumentElement;
        elemRoot.RemoveAll();                               // Remove all

        // We loop through all the minibots      
        foreach (Transform minibot in minibotsContainer.transform)
        {
            newXMLElement = xmlDoc.CreateElement("minibot");  // Create the rotation node
			Minibot minibotScript = minibot.gameObject.GetComponent<Minibot>();
            newXMLElement.SetAttribute("x", minibotScript.startingPos.x.ToString());
            newXMLElement.SetAttribute("y", minibotScript.startingPos.y.ToString());

            //MinibotController controllerScipt = minibot.GetComponent<MinibotController>();
			//GravityHandler gravityHandlerScript = minibot.GetComponent<GravityHandler>();
            string value = "";
			value = BoolToString(minibotScript.InitVerticalOrientation);
            newXMLElement.SetAttribute("invertGravity", value);            

			value = BoolToString(minibotScript.InitHorizontalOrientation);
            newXMLElement.SetAttribute("invertHorizontal", value);
            elemRoot.AppendChild(newXMLElement);                      // Make the transform node the parent
        }

        // We then loop through all the objects
        // First we loop through the tiles first        
        foreach (Transform tile in tilesContainer.transform)
        {
            newXMLElement = xmlDoc.CreateElement("tile");
			Tile tileScript = tile.gameObject.GetComponent<Tile>();
            newXMLElement.SetAttribute("x", tileScript.startingPos.x.ToString());
            newXMLElement.SetAttribute("y", tileScript.startingPos.y.ToString());
            elemRoot.AppendChild(newXMLElement);                      // Make the transform node the parent
        }
		
		// We then loop through all boxes
		foreach (Transform box in boxesContainer.transform)
		{
			newXMLElement = xmlDoc.CreateElement("box");
			Box boxScript = box.gameObject.GetComponent<Box>();
			newXMLElement.SetAttribute("x", Mathf.Ceil(boxScript.startingPos.x).ToString());
			newXMLElement.SetAttribute("y", Mathf.Ceil(boxScript.startingPos.y).ToString());

			string value = "";
			value = BoolToString(boxScript.InitVerticalOrientation);
			newXMLElement.SetAttribute("invertGravity", value);

			elemRoot.AppendChild(newXMLElement);
		}
		
		// We loop through all the doors
		foreach (Transform door in doorsContainer.transform)
		{
			newXMLElement = xmlDoc.CreateElement("door");
			LevelObject levelObjectScript = door.gameObject.GetComponent<LevelObject>();
			newXMLElement.SetAttribute("x", levelObjectScript.startingPos.x.ToString());
			newXMLElement.SetAttribute("y", levelObjectScript.startingPos.y.ToString());
			Door doorScript = door.GetComponent<Door>();
			newXMLElement.SetAttribute("isOpen", BoolToString(doorScript.IsOpen));
			elemRoot.AppendChild(newXMLElement);
		}
		
		// We loop through all the hazards
		foreach (Transform hazard in hazardsContainer.transform)
		{
			newXMLElement = xmlDoc.CreateElement("hazard");
			HazardTile tileScript = hazard.gameObject.GetComponent<HazardTile>();
			newXMLElement.SetAttribute("x", tileScript.startingPos.x.ToString());
			newXMLElement.SetAttribute("y", tileScript.startingPos.y.ToString());
			elemRoot.AppendChild(newXMLElement);	
		}
		
		// We loop through all the horizontalInverters
		foreach (Transform horizontalInverter in horizontalInvertersContainer.transform)
		{
			newXMLElement = xmlDoc.CreateElement("horizontalInverter");			
			LevelObject levelObjectScript = horizontalInverter.gameObject.GetComponent<LevelObject>();
			newXMLElement.SetAttribute("x", levelObjectScript.startingPos.x.ToString());
			newXMLElement.SetAttribute("y", levelObjectScript.startingPos.y.ToString());
			elemRoot.AppendChild(newXMLElement);	
		}
		
		// We loop through all the movingPlatforms
//		foreach (Transform movingPlatform in movingPlatformsContainer.transform )
//		{
//			elemNew = xmlDoc.CreateElement("movingPlatform");
//			elemNew.SetAttribute("x", movingPlatform.position.x.ToString());
//			elemNew.SetAttribute("y", movingPlatform.position.y.ToString());
//			elemRoot.AppendChild(elemNew);	
//		}
		
		// We loop through all the triggerableBlocks
		foreach (Transform triggerableBlock in triggerableBlocksContainer.transform )
		{
			newXMLElement = xmlDoc.CreateElement("triggerableBlock");
			LevelObject levelObjectScript = triggerableBlock.gameObject.GetComponent<LevelObject>();
			newXMLElement.SetAttribute("x", levelObjectScript.startingPos.x.ToString());
			newXMLElement.SetAttribute("y", levelObjectScript.startingPos.y.ToString());
			TriggerableBlocks tbScript = triggerableBlock.GetComponent<TriggerableBlocks>();
			newXMLElement.SetAttribute("isHidden", BoolToString(tbScript.IsHidden));
            newXMLElement.SetAttribute("width", tbScript.dynamicSizeComponent.blockSize.x.ToString());
            newXMLElement.SetAttribute("height", tbScript.dynamicSizeComponent.blockSize.y.ToString());
            elemRoot.AppendChild(newXMLElement);
		}

        foreach (Transform triggerableHazard in triggerableHazardsContainer.transform)
        {
            newXMLElement = xmlDoc.CreateElement("triggerableHazard");
            LevelObject levelObjectScript = triggerableHazard.gameObject.GetComponent<LevelObject>();
            newXMLElement.SetAttribute("x", levelObjectScript.startingPos.x.ToString());
            newXMLElement.SetAttribute("y", levelObjectScript.startingPos.y.ToString());
            TriggerableHazard thScript = triggerableHazard.GetComponent<TriggerableHazard>();
			newXMLElement.SetAttribute("isHidden", BoolToString(thScript.IsHidden));
            newXMLElement.SetAttribute("width", thScript.dynamicSizeComponent.blockSize.x.ToString());
            newXMLElement.SetAttribute("height", thScript.dynamicSizeComponent.blockSize.y.ToString());
            elemRoot.AppendChild(newXMLElement);
        }
		
		// We loop through all the stepSwitches
		foreach (Transform stepSwitch in stepSwitchesContainer.transform )
		{
			newXMLElement = xmlDoc.CreateElement("stepSwitch");
			LevelObject levelObjectScript = stepSwitch.gameObject.GetComponent<LevelObject>();
			newXMLElement.SetAttribute("x", levelObjectScript.startingPos.x.ToString());
			newXMLElement.SetAttribute("y", levelObjectScript.startingPos.y.ToString());

			StepSwitch stepSwitchScript = stepSwitch.gameObject.GetComponent<StepSwitch>();
			ParseAndSaveLinksForSwitch(stepSwitchScript, newXMLElement);
			elemRoot.AppendChild(newXMLElement);	
		}
		
		// We loop through all the switches
		foreach (Transform aSwitch in switchesContainer.transform )
		{
			newXMLElement = xmlDoc.CreateElement("switch");

			LevelObject levelObjectScript = aSwitch.gameObject.GetComponent<LevelObject>();
			newXMLElement.SetAttribute("x", levelObjectScript.startingPos.x.ToString());
			newXMLElement.SetAttribute("y", levelObjectScript.startingPos.y.ToString());

			Switch switchScript = aSwitch.gameObject.GetComponent<Switch>();
			ParseAndSaveLinksForSwitch(switchScript, newXMLElement);
			elemRoot.AppendChild(newXMLElement);	
		}

		// We loop through all gravity inverters
		foreach (Transform gravityInverter in gravityInvertersContainer.transform)
		{
			newXMLElement = xmlDoc.CreateElement("gravityInverter");
			LevelObject levelObjectScript = gravityInverter.gameObject.GetComponent<LevelObject>();
			newXMLElement.SetAttribute("x", levelObjectScript.startingPos.x.ToString());
			newXMLElement.SetAttribute("y", levelObjectScript.startingPos.y.ToString());

			GravitySwitch switchScript = gravityInverter.gameObject.GetComponent<GravitySwitch>();
			ParseAndSaveLinksForSwitch(switchScript, newXMLElement);
			elemRoot.AppendChild(newXMLElement);
		}
          
        xmlDoc.Save(filepath);
    }

	void ParseAndSaveLinksForSwitch(Switch switchScript, XmlElement newXMLElement)
	{
		int index = 1;
		foreach( LevelObject linkedObject in switchScript.LinkedObjects ) {
			if ( linkedObject != null )	{
				newXMLElement.SetAttribute("xPosOfObjectToActivate" + index, linkedObject.startingPos.x.ToString());
				newXMLElement.SetAttribute("yPosOfObjectToActivate" + index, linkedObject.startingPos.y.ToString());
			}
			index++;
		}
	}
}
