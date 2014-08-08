using UnityEngine;
using System.Collections;

public class RepeatingTexture : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.renderer.material.mainTextureScale = new Vector2(20 , 20 );
	}
}
