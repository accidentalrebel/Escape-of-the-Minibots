using UnityEngine;
using System.Collections;

public class GUILevelEditor : MonoBehaviour {

    bool mapEditMode = false;
    bool MapEditMode
    {
        set { mapEditMode = value; }
        get{ return mapEditMode; }
    }

    XMLLevelReader levelReader;
    XMLLevelWriter levelWriter;

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
    }

    void OnGUI()
    {
        MapEditMode = GUI.Toggle(new Rect(10, 10, 200, 20), MapEditMode, "Map Edit Mode");
        
        if (MapEditMode)
        {
            GUI.Label(new Rect(30, 40, 100, 50), "Level name: ");
            string stringToEdit = GUI.TextField (new Rect(105, 40, 50, 20), "1");
            if (GUI.Button(new Rect(30, 80, 100, 30), "Save Map"))
            {
                Debug.Log("Save map clicked");
            }
        }
    }
}
