using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(GraphicHandler))]
public class LevelObject : MonoBehaviour {

    protected GraphicHandler graphicHandler;
    protected SpriteManager spriteManager;
	internal Vector3 startingPos;
	
	virtual protected void Awake()
	{
		startingPos = gameObject.transform.position;

		spriteManager = gameObject.GetComponentInChildren<SpriteManager>();
		if (spriteManager == null)
			Debug.LogWarning("spriteManger at " + gameObject.name + " can not be found!");
	}
	
	virtual internal void Initialize(Vector3 theStartingPos)
	{
		startingPos = theStartingPos;
		gameObject.transform.position = startingPos;
	}
	
    virtual protected void Start()
    {
        graphicHandler = gameObject.GetComponent<GraphicHandler>();
        if (graphicHandler == null)
            Debug.LogError("theGraphicHandler at " + gameObject.name + "can not be found!");
    }

    virtual internal void Use(bool setToValue)
    {
        Debug.LogWarning("Has not been overriden!");
    }

    virtual internal void Use()
    {
        Debug.LogWarning("Has not been overridden!");
    }

    virtual internal void ResetObject()
    {
        gameObject.transform.position = startingPos;        
    }

    virtual internal void GetEditableAttributes(LevelEditor levelEditor)
    {
        Debug.LogWarning("Has not been overriden!");
    }
}
