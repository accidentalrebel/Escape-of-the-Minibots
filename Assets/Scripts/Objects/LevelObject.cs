using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GraphicHandler))]
[RequireComponent(typeof(SpriteManager))]
public class LevelObject : MonoBehaviour {

    protected GraphicHandler _graphicHandler;
    protected SpriteManager _spriteManager;
	public SpriteManager SpriteManager {
		get {
			return _spriteManager;
		}
	}

	public Vector3 startingPos;
	
	virtual protected void Awake()
	{
		startingPos = gameObject.transform.position;
		_spriteManager = gameObject.GetComponentInChildren<SpriteManager>();
		_graphicHandler = gameObject.GetComponent<GraphicHandler>();
	}
	
	virtual public void Initialize(Vector3 theStartingPos)
	{
		startingPos = theStartingPos;
		gameObject.transform.position = startingPos;
	}

	virtual protected void Start() {
		// Intentionally left blank
	}

    virtual public void Use(bool setToValue)
    {
        Debug.LogWarning("Has not been overriden!");
    }

	virtual public void Use()
    {
        Debug.LogWarning("Has not been overridden!");
    }

    virtual public void ResetObject()
    {
        gameObject.transform.position = startingPos;        
    }

    virtual public void GetEditableAttributes(LevelEditor levelEditor)
    {
        Debug.LogWarning("Has not been overriden!");
    }

	protected void HandleSpriteFlipping()
	{
		Vector3 topPosition = transform.position + Vector3.up;
		Vector3 bottomPosition = transform.position + Vector3.down;
		LevelObject levelObjectAtTop = Registry.map.GetLevelObjectAtPosition(topPosition);
		LevelObject levelObjectAtBottom = Registry.map.GetLevelObjectAtPosition(bottomPosition);
		SpriteFlipper spriteFlipper = gameObject.GetComponentInChildren<SpriteFlipper>();

		if ( levelObjectAtBottom == null && levelObjectAtTop != null && levelObjectAtTop is Tile ) {
			spriteFlipper.SetFlippedY(true);
		}
	}
}
