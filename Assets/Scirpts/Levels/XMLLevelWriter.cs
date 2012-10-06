using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class XMLLevelWriter : XMLAccessor {

    GameObject tilesContainer;
    GameObject minibotsContainer;

    void Start()
    {
        tilesContainer = gameObject.transform.FindChild("Tiles").gameObject;
        if (tilesContainer == null)
            Debug.LogError("tilesContainer not found!");
        minibotsContainer = GameObject.Find("Minibots");
        if (minibotsContainer == null)
            Debug.LogError("minibotsContainer not found!");
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
            elemNew = xmlDoc.CreateElement("tile");  // Create the rotation node
            elemNew.SetAttribute("x", tile.position.x.ToString());
            elemNew.SetAttribute("y", tile.position.y.ToString());
            elemRoot.AppendChild(elemNew);                      // Make the transform node the parent
        }
          
        xmlDoc.Save(filepath);
    }
}
