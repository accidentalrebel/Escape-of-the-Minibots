using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class XMLWriter : MonoBehaviour {

    void Start()
    {
        string filepath = Application.dataPath + @"/Levels/test.xml";
        Debug.Log(filepath);

        XmlDocument xmlDoc = new XmlDocument();

        // If file does not exist. Create the xml file.
        if (!File.Exists(filepath))
        {
            Debug.LogWarning("File does not exist. Creating file");

            FileStream fs = File.Create(filepath);
            StreamWriter writer = new StreamWriter(fs);
            writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            writer.WriteLine("<tiles>");
            writer.Write("</tiles>");
            writer.Flush();
            fs.Close();
        }

        Debug.Log("loaded");
        xmlDoc.Load(filepath);
        XmlElement elemRoot = xmlDoc.DocumentElement;
        elemRoot.RemoveAll();                               // Remove all inside the transforms node
        XmlElement elemNew = xmlDoc.CreateElement("rotation");  // Create the rotation node
        //XmlAttribute 

        XmlElement rotationX = xmlDoc.CreateElement("x");   // Create the rotation x node
        rotationX.InnerText = "1";

        elemNew.AppendChild(rotationX);                     // Make the rotation node the parent
        elemRoot.AppendChild(elemNew);                      // Make the transform node the parent

        xmlDoc.Save(filepath);
    }
}
