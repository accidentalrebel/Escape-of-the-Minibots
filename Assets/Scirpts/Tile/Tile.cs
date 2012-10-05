using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour 
{
    internal Renderer theRenderer;

    void Awake()
    {
        theRenderer = gameObject.GetComponentInChildren<Renderer>();
        if (theRenderer == null)
            Debug.LogError("Could not find the renderer!");
    }
}
