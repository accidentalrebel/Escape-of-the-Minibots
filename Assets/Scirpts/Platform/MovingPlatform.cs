using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
    	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Translate(Vector3.right * Time.deltaTime);   
	}
}
