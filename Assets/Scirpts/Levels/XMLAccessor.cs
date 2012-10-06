using UnityEngine;
using System.Collections;
using System.IO;

public class XMLAccessor : MonoBehaviour {

    protected bool CheckIfFileExists(string filepath)
    {
        // If file does not exist. Create the xml file.
        if (!File.Exists(filepath))
        {
            Debug.LogWarning("Xml does not exist!");
            return false;
        }

        return true;
    }
}
