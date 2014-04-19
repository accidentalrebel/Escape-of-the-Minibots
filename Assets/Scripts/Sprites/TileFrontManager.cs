using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteManager))]
public class TileFrontManager : MonoBehaviour {

	private enum Direction4 { 
		TOP,
		RIGHT,
		BOTTOM,
		LEFT
	};

	private Direction4[] _noNeighborDirectionList;
	private SpriteManager _spriteManager;
	private Vector2 _currentPosition;

	void Awake () {
		_spriteManager = gameObject.GetComponent<SpriteManager>();

		Tile myTileClass = transform.parent.GetComponent<Tile> ();
		if ( myTileClass == null )
			Debug.LogWarning("Parent of this object should have a Tile class");
		
		myTileClass.tileFrontManager = this;
	}

	public void UpdateNeighbors () {
		Vector3 _currentPosition = transform.parent.position;

		Vector3 topPosition = new Vector3(_currentPosition.x, _currentPosition.y + 1, _currentPosition.z);
		LevelObject levelObjectAtTop = Registry.map.GetLevelObjectAtPosition(topPosition);
		if ( levelObjectAtTop == null || 
		    ( levelObjectAtTop != null && !(levelObjectAtTop is Tile)))
		{
			_spriteManager.SetFrameTo("default", 2);
		}
	}

	void Update () {
	
	}
}
