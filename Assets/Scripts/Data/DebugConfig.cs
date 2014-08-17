using UnityEngine;
using System.Collections;

public class DebugConfig : MonoBehaviour {

	public bool disableBGMOnStartup = false;
	public string mapToLoadOnStartup = "1";

	void Awake() {
		Registry.debugConfig = this; 
	}
}
