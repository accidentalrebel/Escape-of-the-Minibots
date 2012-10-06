using UnityEngine;
using System.Collections;

public class GUILevelEditor : MonoBehaviour {

    bool mapEditMode = false;
    bool MapEditMode
    {
        set { mapEditMode = value; }
        get{ return mapEditMode; }
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
