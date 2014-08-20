using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {

    private int _initialLevelToLoad = 0;
    public string currentUser = "User";

    public int InitialLevelToLoad
    {
        get { return _initialLevelToLoad;  }
        set { _initialLevelToLoad = value; }
    }

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        currentUser = PlayerPrefs.GetString("profileName");
	}
}
