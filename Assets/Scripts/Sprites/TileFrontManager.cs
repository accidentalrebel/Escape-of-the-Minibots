using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteManager))]
public class TileFrontManager : MonoBehaviour {

	private enum Direction4 { 
		TOP,
		RIGHT,
		BOTTOM,
		LEFT
	};

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
	private Direction4[] _noNeighborDirectionList;
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

		if ( GetLevelObjectAtDirection(Direction4.TOP) == null )
			_spriteManager.SetFrameTo("default", (int)TileSide.TOP);
		else if ( GetLevelObjectAtDirection(Direction4.RIGHT) == null )
			_spriteManager.SetFrameTo("default", (int)TileSide.RIGHT);
		else if ( GetLevelObjectAtDirection(Direction4.BOTTOM) == null ) 
			_spriteManager.SetFrameTo("default", (int)TileSide.BOTTOM);
		else if ( GetLevelObjectAtDirection(Direction4.LEFT) == null )
			_spriteManager.SetFrameTo("default", (int)TileSide.LEFT);
		else
			_spriteManager.SetFrameTo("default", (int)TileSide.MIDDLE);
	}

	private LevelObject GetLevelObjectAtDirection(Direction4 direction) {

		Vector2 directionOffset = Vector2.zero;
		if ( direction == Direction4.TOP )
			directionOffset = new Vector2(0, 1);
		else if ( direction == Direction4.RIGHT )
			directionOffset = new Vector2(1, 0);
		else if ( direction == Direction4.BOTTOM )
			directionOffset = new Vector2(0, -1);
		else if ( direction == Direction4.LEFT )
			directionOffset = new Vector2(-1, 0);

		Vector3 _currentPosition = transform.parent.position;
		Vector3 topPosition = new Vector3(_currentPosition.x + directionOffset.x
		                                  , _currentPosition.y + directionOffset.y
		                                  , _currentPosition.z);
		return Registry.map.GetLevelObjectAtPosition(topPosition);
	}
}
