using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;

public class ReplayViewer : MonoBehaviour {

    public string replayUserFolderName;

	//**==================================== PUBLIC METHODS ====================================**//
	public void StartNextReplay()
	{
		_currentlyPlayingIndex++;
		Registry.map.ClearLevel();
		Registry.replayManager.ClearReplayData();
		DecodeReplayAsset(_replayTextAsset[_currentlyPlayingIndex]);
	}

	//**==================================== PRIVATE VARIABLES ====================================**//
	TextAsset[] _replayTextAsset;
	int _currentlyPlayingIndex = -1;

	TextAsset replayAsset;

	//**==================================== MAIN ====================================**//
	void Awake()
	{
		Registry.replayViewer = this;
	}

	void Start () 
	{
		_replayTextAsset = GetReplayTextAssetList(replayUserFolderName);
		StartNextReplay();
	}

	//**==================================== HELPERS ====================================**//
    void DecodeReplayAsset(TextAsset currentReplayAsset)
    {        
		string[] data = currentReplayAsset.text.Split('^');
		string thisLevel = data[4];
        string replayData = data[7];

		Debug.Log ("Playing the replay of level " + thisLevel);

		Registry.map.levelReader.LoadLevel(thisLevel);
        ConvertToEvents(replayData);
        Registry.main.StartReplay();
    }
	
    void ConvertToEvents(string replayData)
    {
        string[] eventStrings = replayData.Split('#');                          // We split each to eventStrings
        foreach (string eventString in eventStrings)                            // Each event string has two parameters ( Timestamp and the eventType )
        {
            if (eventString != "")                                              // This is in place so that it would skip null values
            {
                string[] eventDetails = eventString.Split('%');                 // We split the eventString so we can access the two parameters
                Registry.replayManager.AddEvent(float.Parse(eventDetails[0]),   // We convert the first parameter to float
                    ((ReplayEvent.EventType)(int.Parse(eventDetails[1]))));     // We convert the second parameter to ReplayEvent.EventType
            }
        }
    }	

	TextAsset[] GetReplayTextAssetList(string path)
	{
		string pathToUse = Application.dataPath + "/Replays/" + path;
		string[] fileNameList = Directory.GetFiles(pathToUse, "*.txt");

		if ( fileNameList == null )
			Debug.LogError("Error getting files at " + pathToUse);

		if ( fileNameList.Length <= 0 )
			Debug.LogError(pathToUse + "directory path has no files in it!");

		fileNameList.Select(fn => new FileInfo(fn)).OrderBy(f => f.Name);

		TextAsset[] textAssetList = new TextAsset[fileNameList.Length];
		int currentIndex = 0;

		foreach(string fileName in fileNameList)
		{
			int index = fileName.LastIndexOf("/");
			string localPath = "Assets/Replays";
			
			if (index > 0)
				localPath += fileName.Substring(index);

			Debug.Log("GETTING TEXT ASSET FROM " + localPath);

			TextAsset textAsset= (TextAsset)Resources.LoadAssetAtPath(localPath, typeof(TextAsset));
			if(textAsset != null) {
				textAssetList[currentIndex] = textAsset;
				Debug.Log ("ADDED at " + currentIndex);
			}

			currentIndex++;
		}

		return textAssetList;
	}
}
