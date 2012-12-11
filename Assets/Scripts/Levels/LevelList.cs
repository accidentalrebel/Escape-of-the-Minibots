using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LevelList : MonoBehaviour {

    List<string> levelList = new List<string>();

	// Use this for initialization
	void Start () {
        string directoryPath = Application.dataPath + @"/Resources/Levels/";
        DirectoryInfo directory = new DirectoryInfo(directoryPath);
        FileInfo[] info = directory.GetFiles("*.xml");

        foreach (FileInfo file in info)
        {
            levelList.Add(file.Name.Substring(0, file.Name.Length - file.Extension.Length));
        }

        levelList.Sort();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
