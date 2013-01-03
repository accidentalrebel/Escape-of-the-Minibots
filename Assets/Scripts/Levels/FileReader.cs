using UnityEngine;
using System.Collections;
using System.IO;

public static class FileReader
{
    public static string GetData(string filepath)
    {
        StreamReader file = new StreamReader(filepath);
        string fileContent = file.ReadToEnd();
        file.Close();

        return fileContent;
    }
}