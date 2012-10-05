using UnityEngine;
using System.Collections;
using System.IO;

public static class FileReader
{
    public static string GetData(string filename)
    {
        StreamReader file = new StreamReader(Application.dataPath + @"\Levels\" + filename + ".txt");
        string fileContent = file.ReadToEnd();
        file.Close();

        return fileContent;
    }
}
