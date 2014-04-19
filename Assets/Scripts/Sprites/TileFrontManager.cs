using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteManager))]
public class TileFrontManager : MonoBehaviour {

	private enum TileSide {
		MIDDLE 			= 1,
		TOP 			= 2,
		TOP_RIGHT 		= 3,
		RIGHT 			= 4,
		BOTTOM_RIGHT 	= 5,
		BOTTOM 			= 6,
		BOTTOM_LEFT		= 7,
		LEFT 			= 8,
		TOP_LEFT 		= 9,
		INWARD_TOP_RIGHT 	= 10,
		INWARD_BOTTOM_RIGHT = 11,
		INWARD_BOTTOM_LEFT 	= 12,
		INWARD_TOP_LEFT 	= 13
	};

	private Dictionary <TileSide, Vector2> _tileSideDictionary;
	private SpriteManager _spriteManager;
	private Vector2 _currentPosition;

	void Awake () {
		_spriteManager = gameObject.GetComponent<SpriteManager>();

		Tile myTileClass = transform.parent.GetComponent<Tile> ();
		if ( myTileClass == null )
			Debug.LogWarning("Parent of this object should have a Tile class");
		
		myTileClass.tileFrontManager = this;

		SetupTileSideDictionary();
	}

	void SetupTileSideDictionary ()
	{
		_tileSideDictionary = new Dictionary<TileSide, Vector2>();
		_tileSideDictionary.Add (TileSide.TOP, new Vector2(0, 1));
		_tileSideDictionary.Add (TileSide.TOP_RIGHT, new Vector2(1, 1));
		_tileSideDictionary.Add (TileSide.RIGHT, new Vector2(1, 0));
		_tileSideDictionary.Add (TileSide.BOTTOM_RIGHT, new Vector2(1, -1));
		_tileSideDictionary.Add (TileSide.BOTTOM, new Vector2(0, 1));
		_tileSideDictionary.Add (TileSide.BOTTOM_LEFT, new Vector2(-1, -1));
		_tileSideDictionary.Add (TileSide.LEFT, new Vector2(-1, 0));
		_tileSideDictionary.Add (TileSide.TOP_LEFT, new Vector2(-1, 1));
	}

	public void UpdateNeighbors () {

		if ( GetLevelObjectAtDirection(TileSide.TOP) == null )
			_spriteManager.SetFrameTo("default", (int)TileSide.TOP);
		else if ( GetLevelObjectAtDirection(TileSide.RIGHT) == null )
			_spriteManager.SetFrameTo("default", (int)TileSide.RIGHT);
		else if ( GetLevelObjectAtDirection(TileSide.BOTTOM) == null ) 
			_spriteManager.SetFrameTo("default", (int)TileSide.BOTTOM);
		else if ( GetLevelObjectAtDirection(TileSide.LEFT) == null )
			_spriteManager.SetFrameTo("default", (int)TileSide.LEFT);
		else
			_spriteManager.SetFrameTo("default", (int)TileSide.MIDDLE);
	}

	private LevelObject GetLevelObjectAtDirection(TileSide tileSideDirection) {

		Vector2 directionOffset = _tileSideDictionary[tileSideDirection];
		Vector3 _currentPosition = transform.parent.position;
		Vector3 topPosition = new Vector3(_currentPosition.x + directionOffset.x
		                                  , _currentPosition.y + directionOffset.y
		                                  , _currentPosition.z);
		return Registry.map.GetLevelObjectAtPosition(topPosition);
	}
}
