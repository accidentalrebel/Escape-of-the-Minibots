using UnityEngine;
using System.Collections;

public class GraphicHandler : MonoBehaviour 
{
    internal Renderer theRenderer;
	internal Vector3 startingPos;

    void Awake()
    {
        theRenderer = gameObject.GetComponentInChildren<Renderer>();
        if (theRenderer == null)
            Debug.LogError("Could not find the renderer!");
		
		startingPos = gameObject.transform.position;
    }
}
