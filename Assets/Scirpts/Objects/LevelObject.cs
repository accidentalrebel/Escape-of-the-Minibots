using UnityEngine;
using System.Collections;

public class LevelObject : MonoBehaviour {

    protected Tile theTile;
	internal Vector3 startingPos;
	
	protected void Awake()
	{
		startingPos = gameObject.transform.position;
		Debug.Log (gameObject.name + startingPos);
	}
	
	protected void Initialize(Vector3 theStartingPos)
	{
		startingPos = theStartingPos;
		gameObject.transform.position = startingPos;
	}
	
    protected void Start()
    {
        theTile = gameObject.GetComponent<Tile>();
        if (theTile == null)
            Debug.LogError("theTile can not be found!");
    }

    virtual internal void Use()
    {
        Debug.LogWarning("Has not been overriden!");
    }
}
