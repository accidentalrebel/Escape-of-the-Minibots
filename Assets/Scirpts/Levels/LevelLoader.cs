using UnityEngine;
using System.Collections;

[RequireComponent(typeof(FileReader))]
public class LevelLoader : MonoBehaviour {
        
	// Use this for initialization
	void Start () {
        BuildLevel(GetLevelData("1"));
	}

    private string GetLevelData(string levelLabel)
    {       
        return FileReader.GetData(levelLabel);
    }

    private void BuildLevel(string levelData)
    {
        Debug.Log("levelData is " + levelData);        
    }	
}
