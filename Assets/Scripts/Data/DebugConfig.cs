using UnityEngine;
using System.Collections;

public class DebugConfig : MonoBehaviour {

	public bool disableBGMOnStartup = false;
    public bool isCameraOrthographic = true;
    public string mapToLoadOnStartup = "1";
    
	void Awake() 
    {
		Registry.debugConfig = this; 
	}

    void Start()
    {
        if ( !isCameraOrthographic )
            Camera.main.orthographic = false;
    }
}
