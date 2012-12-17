using UnityEngine;
using System.Collections;

public class PlayTestManager : MonoBehaviour {

    void Awake()
    {
        StartCoroutine("UploadData");
    }

    public IEnumerator UploadData()
    {
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("replayData", "replay data");
        wwwForm.AddField("timeStamp", "09/26/1986");
        wwwForm.AddField("user", "user name");
        WWW www = new WWW("http://www.accidentalrebel.com/minibots/playtestmailer.php", wwwForm);
        yield return www;
        Debug.Log("Uploaded replay data!");
    }
}
