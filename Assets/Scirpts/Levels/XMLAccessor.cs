using UnityEngine;
using System.Collections;
using System.IO;

public class XMLAccessor : MonoBehaviour {

    protected bool CheckIfFileExists(string levelToCheck)
    {
        string filepath = Application.dataPath + @"/Levels/" + levelToCheck + ".xml";

        // If file does not exist. Create the xml file.
        if (!File.Exists(filepath))
        {
            Debug.LogWarning("File " + levelToCheck + ".xml does not exist!");
            return false;
        }

        return true;
    }
}
