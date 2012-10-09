using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectList : MonoBehaviour {
	
	internal List<LevelObject> activeTriggerableObjects;
	
	void Awake () {
		activeTriggerableObjects = new List<LevelObject>();
	}
}
