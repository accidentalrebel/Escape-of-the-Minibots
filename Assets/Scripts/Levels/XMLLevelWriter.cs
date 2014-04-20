using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class XMLLevelWriter : XMLAccessor {

    public void SaveLevel(string filename)
    {
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

            MinibotController controllerScipt
                = minibot.GetComponent<MinibotController>();
            string value = "";
            value = BoolToString(controllerScipt.IsInvertedVertically);
            elemNew.SetAttribute("invertGravity", value);            

            value = BoolToString(controllerScipt.IsInvertedHorizontally);
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
			HazardTile tileScript = hazard.gameObject.GetComponent<HazardTile>();
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
		
		// We loop through all the triggerableBlocks
		foreach (Transform triggerableBlock in triggerableBlocksContainer.transform )
		{
			elemNew = xmlDoc.CreateElement("triggerableBlock");
			LevelObject levelObjectScript = triggerableBlock.gameObject.GetComponent<LevelObject>();
			elemNew.SetAttribute("x", levelObjectScript.startingPos.x.ToString());
			elemNew.SetAttribute("y", levelObjectScript.startingPos.y.ToString());
			TriggerableBlocks tbScript = triggerableBlock.GetComponent<TriggerableBlocks>();
			elemNew.SetAttribute("isHidden", BoolToString(tbScript.IsHidden));
            elemNew.SetAttribute("width", tbScript.dynamicSizeComponent.blockSize.x.ToString());
            elemNew.SetAttribute("height", tbScript.dynamicSizeComponent.blockSize.y.ToString());
            elemRoot.AppendChild(elemNew);
		}

        foreach (Transform triggerableHazard in triggerableHazardsContainer.transform)
        {
            elemNew = xmlDoc.CreateElement("triggerableHazard");
            LevelObject levelObjectScript = triggerableHazard.gameObject.GetComponent<LevelObject>();
            elemNew.SetAttribute("x", levelObjectScript.startingPos.x.ToString());
            elemNew.SetAttribute("y", levelObjectScript.startingPos.y.ToString());
            TriggerableHazard thScript = triggerableHazard.GetComponent<TriggerableHazard>();
			elemNew.SetAttribute("isHidden", BoolToString(thScript.IsHidden));
            elemNew.SetAttribute("width", thScript.dynamicSizeComponent.blockSize.x.ToString());
            elemNew.SetAttribute("height", thScript.dynamicSizeComponent.blockSize.y.ToString());
            elemRoot.AppendChild(elemNew);
        }
		
		// We loop through all the stepSwitches
		foreach (Transform stepSwitch in stepSwitchesContainer.transform )
		{
			elemNew = xmlDoc.CreateElement("stepSwitch");
			LevelObject levelObjectScript = stepSwitch.gameObject.GetComponent<LevelObject>();
			elemNew.SetAttribute("x", levelObjectScript.startingPos.x.ToString());
			elemNew.SetAttribute("y", levelObjectScript.startingPos.y.ToString());
			StepSwitch switchScript = stepSwitch.gameObject.GetComponent<StepSwitch>();
			elemNew.SetAttribute("xPosOfObjectToActivate", switchScript.posOfObjectToActivate1.x.ToString());
			elemNew.SetAttribute("yPosOfObjectToActivate", switchScript.posOfObjectToActivate1.y.ToString());
            elemNew.SetAttribute("xPosOfObjectToActivate2", switchScript.posOfObjectToActivate2.x.ToString());
            elemNew.SetAttribute("yPosOfObjectToActivate2", switchScript.posOfObjectToActivate2.y.ToString());
            elemNew.SetAttribute("xPosOfObjectToActivate3", switchScript.posOfObjectToActivate3.x.ToString());
            elemNew.SetAttribute("yPosOfObjectToActivate3", switchScript.posOfObjectToActivate3.y.ToString());
			elemRoot.AppendChild(elemNew);	
		}
		
		// We loop through all the switches
		foreach (Transform aSwitch in switchesContainer.transform )
		{
			elemNew = xmlDoc.CreateElement("switch");
			LevelObject levelObjectScript = aSwitch.gameObject.GetComponent<LevelObject>();
			elemNew.SetAttribute("x", levelObjectScript.startingPos.x.ToString());
			elemNew.SetAttribute("y", levelObjectScript.startingPos.y.ToString());
			Switch switchScript = aSwitch.gameObject.GetComponent<Switch>();
			elemNew.SetAttribute("xPosOfObjectToActivate", switchScript.posOfObjectToActivate1.x.ToString());
			elemNew.SetAttribute("yPosOfObjectToActivate", switchScript.posOfObjectToActivate1.y.ToString());
            elemNew.SetAttribute("xPosOfObjectToActivate2", switchScript.posOfObjectToActivate2.x.ToString());
            elemNew.SetAttribute("yPosOfObjectToActivate2", switchScript.posOfObjectToActivate2.y.ToString());
            elemNew.SetAttribute("xPosOfObjectToActivate3", switchScript.posOfObjectToActivate3.x.ToString());
            elemNew.SetAttribute("yPosOfObjectToActivate3", switchScript.posOfObjectToActivate3.y.ToString());
			elemRoot.AppendChild(elemNew);	
		}
          
        xmlDoc.Save(filepath);
    }
}
