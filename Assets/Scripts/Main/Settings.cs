using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {

    int initialLevelToLoad = 0;

    internal int InitialLevelToLoad
    {
        get { return initialLevelToLoad;  }
        set { initialLevelToLoad = value; }
    }

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
}
