using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class SpriteManager : MonoBehaviour {

    Renderer theRenderer;

	// Use this for initialization
	void Start () {
        theRenderer = gameObject.GetComponent<Renderer>().renderer;
        if (theRenderer == null)
            Debug.LogError("theRenderer is not found!");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
