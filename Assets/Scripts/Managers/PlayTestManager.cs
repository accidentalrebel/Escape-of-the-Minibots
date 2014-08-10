using UnityEngine;
using System.Collections;
using System;

public class PlayTestManager : MonoBehaviour {

	const string PLAYTEST_EMAIL = "accidentalrebel_3avg@sendtodropbox.com";

    public bool enableSendingPlaytestData = true;

    void Awake()
    {
        Registry.playtestManager = this;        
    }

    public void SendPlaytestData(string currentUser, string theCompletionTime, string engineVersion, string mapPackVersion, string levelComment)
    {
        if (enableSendingPlaytestData)
        {
            string replayData = Registry.replayManager.GetReplayDataString();
            string username = currentUser;
            string completionTime = theCompletionTime;
            string timeStamp = DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Year + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute;
            string fileData = username + "^" + timeStamp + "^" + engineVersion + "^" + mapPackVersion + "^"
                + Registry.map.currentLevel + "^" + completionTime + "^" + levelComment + "^" + replayData;
            string level = Registry.map.currentLevel;
            StartCoroutine(UploadData(level, fileData, username, timeStamp));
        }
    }

    public IEnumerator UploadData(string level, string fileData, string username, string timeStamp)
    {
        Debug.Log("sending " + fileData + "-" + timeStamp + "-" + username);

        WWWForm wwwForm = new WWWForm();
		wwwForm.AddField("toEmail", PLAYTEST_EMAIL);
		wwwForm.AddField("emailSubject", username);
        wwwForm.AddField("emailTxt", username + "-" + level + "-" + timeStamp);
        wwwForm.AddField("fileName", level + "-" + timeStamp + ".txt");
        wwwForm.AddField("fileData", fileData);
		WWW www = new WWW("http://www.accidentalrebel.com/game-files/minibots/playtestmailer.php", wwwForm);
        yield return www;

		if (!String.IsNullOrEmpty(www.error))
			print("ERROR UPLOADING REPLAY: " + www.error);
		else
			print("Finished Uploading replay!");
	}
}
