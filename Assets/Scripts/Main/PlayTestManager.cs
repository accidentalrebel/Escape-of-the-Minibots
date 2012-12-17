using UnityEngine;
using System.Collections;
using System;

public class PlayTestManager : MonoBehaviour {

    void Start()
    {
        Registry.playtestManager = this;
    }

    internal void SendPlaytestData()
    {
        string replayData = Registry.replayManager.GetReplayDataString();
        string username = "Karlo";
        string timeStamp = DateTime.Now.ToShortDateString() + "@" + DateTime.Now.Hour + "@" + DateTime.Now.Minute;
        string fileData = Registry.map.currentLevel + "^" + username + "^" + replayData;        
        StartCoroutine(UploadData(fileData, username, timeStamp));
    }

    public IEnumerator UploadData(string fileData, string username, string timeStamp)
    {
        Debug.Log("sending " + fileData + "-" + timeStamp + "-" + username);

        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("fileData", fileData);
        wwwForm.AddField("timeStamp", timeStamp);
        wwwForm.AddField("user", username);
        WWW www = new WWW("http://www.accidentalrebel.com/minibots/playtestmailer.php", wwwForm);
        yield return www;
        Debug.Log("Uploaded replay data!");
    }
}
