using UnityEngine;
using System.Collections;
using System.Xml;

public class XMLLevelReader : XMLAccessor {
    
    public string levelToLoad = "1";

	// Use this for initialization
	void Start () {
        CheckIfFileExists(levelToLoad);
	}   

    void LoadLevel(string filename)
    {
        XmlReader reader = XmlReader.Create(@"Assets\Levels\" + filename + ".xml");
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
}
