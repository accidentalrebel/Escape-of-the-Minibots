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

        Debug.Log("replay data is " + replayData);
    }	
}
