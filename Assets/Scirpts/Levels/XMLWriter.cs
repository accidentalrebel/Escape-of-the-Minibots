using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class XMLWriter : MonoBehaviour {

    void Start()
    {
        string filepath = Application.dataPath + @"/Levels/1.xml";
        Debug.Log(filepath);

        XmlDocument xmlDoc = new XmlDocument();
       
        if (File.Exists(filepath))
        {
            Debug.Log("loaded");
            xmlDoc.Load(filepath);
            XmlElement elemRoot = xmlDoc.DocumentElement;
            elemRoot.RemoveAll();                               // Remove all inside the transforms node
            XmlElement elemNew = xmlDoc.CreateElement("rotation");  // Create the rotation node

            XmlElement rotationX = xmlDoc.CreateElement("x");   // Create the rotation x node
            rotationX.InnerText = "1";

            elemNew.AppendChild(rotationX);                     // Make the rotation node the parent
            elemRoot.AppendChild(elemNew);                      // Make the transform node the parent

            xmlDoc.Save(filepath);
        }
    }
}
