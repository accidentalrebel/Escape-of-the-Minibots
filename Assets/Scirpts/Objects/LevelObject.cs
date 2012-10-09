using UnityEngine;
using System.Collections;

public class LevelObject : MonoBehaviour {

    protected Tile theTile;
	internal Vector3 startingPos;

    protected void Start()
    {
        theTile = gameObject.GetComponent<Tile>();
        if (theTile == null)
            Debug.LogError("theTile can not be found!");
		
		startingPos = gameObject.transform.position;
    }

    virtual internal void Use()
    {
        Debug.LogWarning("Has not been overriden!");
    }
}
