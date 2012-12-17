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
        string timeStamp = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute;
        string fileData = Registry.map.currentLevel + "^" + username + "^" + replayData;
        string level = Registry.map.currentLevel;
        StartCoroutine(UploadData(level, fileData, username, timeStamp));
    }

    public IEnumerator UploadData(string level, string fileData, string username, string timeStamp)
    {
        Debug.Log("sending " + fileData + "-" + timeStamp + "-" + username);

        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("emailSubject", level + "-" + username + "-" + timeStamp);
        wwwForm.AddField("emailTxt", level + "-" + username + "-" + timeStamp);
        wwwForm.AddField("fileData", fileData);
        WWW www = new WWW("http://www.accidentalrebel.com/minibots/playtestmailer.php", wwwForm);
        yield return www;
        Debug.Log("Uploaded replay data!");
    }
}
