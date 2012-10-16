using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GraphicHandler))]
public class LevelObject : MonoBehaviour {

    protected GraphicHandler theGraphicHandler;
	internal Vector3 startingPos;
	
	virtual protected void Awake()
	{
		startingPos = gameObject.transform.position;
	}
	
	virtual internal void Initialize(Vector3 theStartingPos)
	{
		startingPos = theStartingPos;
		gameObject.transform.position = startingPos;
	}
	
    virtual protected void Start()
    {
        theGraphicHandler = gameObject.GetComponent<GraphicHandler>();
        if (theGraphicHandler == null)
            Debug.LogError("theGraphicHandler at " + gameObject.name + "can not be found!");
    }

    virtual internal void Use()
    {
        Debug.LogWarning("Has not been overriden!");
    }

    virtual internal void ResetObject()
    {
        gameObject.transform.position = startingPos;        
    }
}