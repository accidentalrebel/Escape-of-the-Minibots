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

    protected string BoolToString(bool theParameter)
    {
        if (theParameter)
            return "1";
        else
            return "0";
    }

    protected bool StringToBool(string theValue)
    {
        if (theValue == "0")
            return false;
        else if (theValue == "1")
            return true;

        return false;
    }
}