using UnityEngine;
using System.Collections;
using System.Xml;

public class XMLLevelReader : XMLAccessor {
    
    public string levelToLoad = "1";
    
    private Object pfTile;

	// Use this for initialization
	void Start () {
        pfTile = Resources.Load(@"Prefabs/pfTile");
        if (pfTile == null)
            Debug.LogError("Can not find pfTile prefab!");

        string filepath = Application.dataPath + @"/Levels/" + levelToLoad + ".xml";
        if (CheckIfFileExists(filepath))
            LoadLevel(filepath);
	}   

    void LoadLevel(string filepath)
    {
        XmlReader reader = XmlReader.Create(filepath);
        Debug.Log(reader);

        while (reader.Read())
        {
            if (reader.IsStartElement("minibot"))
            {
                GameObject newTile = (GameObject)Instantiate(pfTile);
                newTile.transform.position
                    = new Vector3(float.Parse(reader.GetAttribute("x"))
                        , float.Parse(reader.GetAttribute("y"), 0));
            }
            else if (reader.IsStartElement("tile"))
            {
                GameObject newTile = (GameObject)Instantiate(pfTile);
                newTile.transform.position
                    = new Vector3(float.Parse(reader.GetAttribute("x"))
                        , float.Parse(reader.GetAttribute("y"), 0));
            }
        }
    }
}
