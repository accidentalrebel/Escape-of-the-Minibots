using UnityEngine;
using System.Collections;
using System.Xml;

public class XMLReader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        XmlReader reader = XmlReader.Create(@"Assets\Levels\test.xml");
        Debug.Log(reader);

        while (reader.Read())
        {
            if (reader.IsStartElement("tile"))
            {
                Debug.Log(reader.GetAttribute("type"));
                Debug.Log(reader.GetAttribute("x"));
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
