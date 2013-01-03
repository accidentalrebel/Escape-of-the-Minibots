using UnityEngine;
using System.Collections;

public class ReplayViewer : MonoBehaviour {

    public bool isEnabled = false;
    public TextAsset replayAsset;

	// Use this for initialization
	void Start () {
        if ( isEnabled )
            DecodeReplayAsset();
	}

    private void DecodeReplayAsset()
    {        
        string[] data = replayAsset.text.Split('^');

        string username = data[0];
        string dateStamp = data[1];
        string engineVersion = data[2];
        string mapPackVersion = data[3];
        string thisLevel = data[4];
        string timeFinished = data[5];
        string levelComment = data[6];
        string replayData = data[7];

        Registry.main.LoadNextLevel(thisLevel);
        ConvertToEvents(replayData);
        Registry.main.StartReplay();        
    }

    /// <summary>
    /// Converts the replayData in the form of a string to actual events which is added to the replayManager
    /// </summary>
    /// <param name="replayData"></param>
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
}
