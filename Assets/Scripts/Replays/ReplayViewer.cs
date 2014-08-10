using UnityEngine;
using System.Collections;
using System.IO;

public class ReplayViewer : MonoBehaviour {

    public bool isEnabled = false;
    public string replayUserFolderName;

	private TextAsset[] _replayTextAsset;
	public TextAsset[] replayTextAsset
	{
		get { return _replayTextAsset; }
	}

	private TextAsset replayAsset;

	void Start () 
	{
        if ( !isEnabled )
			return;

		_replayTextAsset = GetReplayTextAssetList(replayUserFolderName);
		DecodeReplayAsset(_replayTextAsset[0]);
	}

    private void DecodeReplayAsset(TextAsset currentReplayAsset)
    {        
		string[] data = currentReplayAsset.text.Split('^');
		string thisLevel = data[4];
        string replayData = data[7];

        Registry.main.LoadNextLevel(thisLevel);
        ConvertToEvents(replayData);
        Registry.main.StartReplay();        
    }
	
    private void ConvertToEvents(string replayData)
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

	private TextAsset[] GetReplayTextAssetList(string path)
	{
		string pathToUse = Application.dataPath + "/Replays/" + path;
		Debug.Log ("AT STARTTTT " + pathToUse);
		string[] fileNameList = Directory.GetFiles(pathToUse);
		if ( fileNameList == null )
			Debug.LogError("Error getting files at " + pathToUse);

		if ( fileNameList.Length <= 0 )
			Debug.LogError(pathToUse + "directory path has no files in it!");

		TextAsset[] textAssetList = new TextAsset[fileNameList.Length];
		int currentIndex = 0;

		foreach(string fileName in fileNameList)
		{
			int index = fileName.LastIndexOf("/");
			string localPath = "Assets/Replays";
			
			if (index > 0)
				localPath += fileName.Substring(index);

			TextAsset textAsset= (TextAsset)Resources.LoadAssetAtPath(localPath, typeof(TextAsset));
			if(textAsset != null) {
				textAssetList[currentIndex] = textAsset;
			}

			currentIndex++;
		}

		return textAssetList;
	}
}
