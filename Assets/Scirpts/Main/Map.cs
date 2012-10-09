using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	
	internal ObjectList objectList;
	
	// Use this for initialization
	void Start () {
		Registry.map = this;
		
		objectList = gameObject.GetComponent<ObjectList>();
	}
}
