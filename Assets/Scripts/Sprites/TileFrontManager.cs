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
	}

	public void GetNeighbors () {
		Vector3 _currentPosition = transform.parent.position;

		Debug.Log ("Getting neighbor of tile " + _currentPosition);	
		Vector3 topPosition = new Vector3(_currentPosition.x, _currentPosition.y + 1, _currentPosition.z);
		LevelObject levelObjectAtTop = Registry.map.GetLevelObjectAtPosition(topPosition);
		if ( levelObjectAtTop == null || 
		    ( levelObjectAtTop != null && !(levelObjectAtTop is Tile)))
		{
			Debug.LogWarning ("Tile at " + _currentPosition + " has no top neighbor");
			renderer.material.SetTextureScale("_MainTex", new Vector2(0.1f, 0.1f));
		}
	}

	void Update () {
	
	}
}
