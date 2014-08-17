using UnityEngine;
using System.Collections;

public class DebugConfig : MonoBehaviour {

	public bool disableBGMOnStartup = false;

	void Awake() {
		Registry.debugConfig = this; 
	}
}
