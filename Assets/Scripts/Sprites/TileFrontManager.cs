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

		Tile tileClass = transform.parent.GetComponent<Tile>();
		if ( tileClass == null )
			Debug.LogWarning("Parent of this object should have a Tile class");
		
		tileClass.tileFrontManager = this;
	}

	public void GetNeighbors () {
		Vector3 _currentPosition = transform.parent.position;

		Vector3 topPosition = new Vector3(_currentPosition.x, _currentPosition.y + 1, _currentPosition.z);
		LevelObject levelObjectAtTop = Registry.map.GetLevelObjectAtPosition(topPosition);
		if ( levelObjectAtTop == null || 
		    ( levelObjectAtTop != null && !(levelObjectAtTop is Tile)))
		{
			renderer.material.SetTextureScale("_MainTex", new Vector2(0.1f, 0.1f));
		}
	}

	void Update () {
	
	}
}
