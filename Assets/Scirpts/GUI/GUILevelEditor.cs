using UnityEngine;
using System.Collections;

public class GUILevelEditor : MonoBehaviour {

    bool mapEditMode = false;
    bool MapEditMode
    {
        set { mapEditMode = value; }
        get{ return mapEditMode; }
    }
    bool saveMapMode = false;

    XMLLevelReader levelReader;
    XMLLevelWriter levelWriter;
    string levelFileName;

    void Start()
    {
        GameObject map = GameObject.Find("Map");
        if (map == null)
            Debug.LogError("map is not found!");

        levelReader = map.GetComponent<XMLLevelReader>();
        if (levelReader == null)
            Debug.LogError("levelReader is not found!");

        levelWriter = map.GetComponent<XMLLevelWriter>();
        if (levelWriter == null)
            Debug.LogError("levelWriter is not found!");

        levelFileName = levelReader.levelToLoad;
    }

    void OnGUI()
    {
        MapEditMode = GUI.Toggle(new Rect(10, 10, 200, 20), MapEditMode, "Map Edit Mode");
        
        if (MapEditMode)
        {
            if (GUI.Button(new Rect(10, 550, 100, 30), "Spawn Player"))
            {
                Debug.Log("Spawn player clicked");
            }

            saveMapMode = GUI.Toggle(new Rect(10, 40, 200, 20), saveMapMode, "Save map?");

            if (saveMapMode)
            {
                GUI.Label(new Rect(30, 80, 100, 50), "Level name: ");
                levelFileName = GUI.TextField(new Rect(115, 80, 50, 20), levelFileName);
                if (GUI.Button(new Rect(40, 110, 100, 30), "Save Map"))
                {
                    Debug.Log("Save map clicked");
                    levelWriter.SaveLevel(levelFileName);
                }
            }
        }
    }
}
