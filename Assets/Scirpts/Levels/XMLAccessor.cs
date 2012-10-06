using UnityEngine;
using System.Collections;
using System.IO;

public class XMLAccessor : MonoBehaviour {

    protected void CheckIfFileExists(string levelToLoad)
    {
        string filepath = Application.dataPath + @"/Levels/" + levelToLoad + ".xml";

        // If file does not exist. Create the xml file.
        if (!File.Exists(filepath))
        {
            Debug.LogError("File " + levelToLoad + ".xml does not exist!");
        }
    }
}
