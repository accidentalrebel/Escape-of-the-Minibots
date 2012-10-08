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
            elemNew.SetAttribute("x", minibot.position.x.ToString());
            elemNew.SetAttribute("y", minibot.position.y.ToString());

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
            elemNew.SetAttribute("x", tile.position.x.ToString());
            elemNew.SetAttribute("y", tile.position.y.ToString());
            elemRoot.AppendChild(elemNew);                      // Make the transform node the parent
        }
		
		// We then loop through all boxes
		foreach (Transform box in boxesContainer.transform)
		{
			elemNew = xmlDoc.CreateElement("box");
			elemNew.SetAttribute("x", box.position.x.ToString());
			elemNew.SetAttribute("y", box.position.y.ToString());
			elemRoot.AppendChild(elemNew);
		}
		
		// We loop through all the doors
		foreach (Transform door in doorsContainer.transform)
		{
			elemNew = xmlDoc.CreateElement("door");
			elemNew.SetAttribute("x", door.position.x.ToString());
			elemNew.SetAttribute("y", door.position.y.ToString());
			Door doorScript = door.GetComponent<Door>();
			elemNew.SetAttribute("isOpen", BoolToString(doorScript.isOpen));
			elemRoot.AppendChild(elemNew);
		}
		
		// We loop through all gravity inverters
		foreach (Transform gravityInverter in gravityInvertersContainer.transform)
		{
			elemNew = xmlDoc.CreateElement("gravityInverter");
			elemNew.SetAttribute("x", gravityInverter.position.x.ToString());
			elemNew.SetAttribute("y", gravityInverter.position.y.ToString());
			elemRoot.AppendChild(elemNew);
		}
          
        xmlDoc.Save(filepath);
    }
}
