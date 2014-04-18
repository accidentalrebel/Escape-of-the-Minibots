using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LevelList : MonoBehaviour {

    public List<string> members = new List<string>();

	// Use this for initialization
	void Start () {
        string directoryPath = Application.dataPath + @"/Resources/Levels/";
        DirectoryInfo directory = new DirectoryInfo(directoryPath);
        FileInfo[] info = directory.GetFiles("*.xml");

        foreach (FileInfo file in info)
        {
            string theString = file.Name.Substring(0, file.Name.Length - file.Extension.Length);
            
            // We pad with zeroes
            if (theString.Length == 1)
                theString = "00" + theString;
            else if (theString.Length == 2)
                theString = "0" + theString;
               
            members.Add(theString);
        }

        members.Sort();
	}
}
