﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteManager))]
public class TileFrontManager : MonoBehaviour {

	public bool enabled = true;

	public void Enable()
	{
		enabled = true;
	}

	public void Disable()
	{
		enabled = false;
	}

	public void UpdateNeighbors () 
	{
		if ( !enabled ) 
			return;

		if ( !CheckForEdgeNeighbors()
		    && !CheckForTripeAndUpInwardNeighbors()
		    && !CheckForDoubleInwardNeighbors()
		    && !CheckForTopRightNeighbor() 
		    && !CheckForBottomRightNeighbor()
		    && !CheckForBottomLeftNeighbor()
		    && !CheckForTopLeftNeighbor() 
		    && !CheckForOppositeNeighbors()
		    && !CheckForNormalNeighbors()) {
			_spriteManager.SetFrameTo("default", (int)TileSide.MIDDLE);
		}
	}

	private enum TileSide {
		MIDDLE 				= 1,
		TOP 				= 2,
		TOP_RIGHT 			= 3,
		RIGHT 				= 4,
		BOTTOM_RIGHT 		= 5,
		BOTTOM 				= 6,
		BOTTOM_LEFT			= 7,
		LEFT 				= 8,
		TOP_LEFT 			= 9,
		IN_TOP_RIGHT 		= 10,
		IN_BOTTOM_RIGHT 	= 11,
		IN_BOTTOM_LEFT 		= 12,
		IN_TOP_LEFT 		= 13,
		RIGHT_LEFT			= 14,
		TOP_BOTTOM			= 15,
		IN_TOP_RIGHT_OUT_BOTTOM_LEFT 	= 16,
		IN_TOP_RIGHT_OUT_LEFT 			= 17,
		IN_TOP_RIGHT_OUT_BOTTOM 		= 18,
		IN_BOTTOM_RIGHT_OUT_TOP_LEFT 	= 19,
		IN_BOTTOM_RIGHT_OUT_TOP			= 20,
		IN_BOTTOM_RIGHT_OUT_LEFT		= 21,
		IN_BOTTOM_LEFT_OUT_TOP_RIGHT	= 22,
		IN_BOTTOM_LEFT_OUT_RIGHT		= 23,
		IN_BOTTOM_LEFT_OUT_TOP			= 24,
		IN_TOP_LEFT_OUT_BOTTOM_RIGHT	= 25,
		IN_TOP_LEFT_OUT_RIGHT			= 26,
		IN_TOP_LEFT_OUT_BOTTOM			= 27,
		EDGE_TOP			= 28,
		EDGE_RIGHT			= 29,
		EDGE_BOTTOM			= 30,
		EDGE_LEFT			= 31,
		SINGLE				= 32,
		DOUBLE_IN_TOP		= 33,
		DOUBLE_IN_RIGHT		= 34,
		DOUBLE_IN_BOTTOM	= 35,
		DOUBLE_IN_LEFT		= 36,
		DOUBLE_IN_TOP_W_BAR		= 37,
		DOUBLE_IN_RIGHT_W_BAR 	= 38,
		DOUBLE_IN_BOTTOM_W_BAR	= 39,
		DOUBLE_IN_LEFT_W_BAR	= 40,
		TRIPLE_IN_TOP_RIGHT		= 41,
		TRIPLE_IN_BOTTOM_RIGHT	= 42,
		TRIPLE_IN_BOTTOM_LEFT	= 43,
		TRIPLE_IN_TOP_LEFT		= 44,
		FOURWAY_IN				= 45
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
		_tileSideDictionary.Add (TileSide.BOTTOM, new Vector2(0, -1));
		_tileSideDictionary.Add (TileSide.BOTTOM_LEFT, new Vector2(-1, -1));
		_tileSideDictionary.Add (TileSide.LEFT, new Vector2(-1, 0));
		_tileSideDictionary.Add (TileSide.TOP_LEFT, new Vector2(-1, 1));
	}

	private bool CheckForTripeAndUpInwardNeighbors ()
	{
		if ( GetTileObjectAtDirection(TileSide.TOP) != null
		    && GetTileObjectAtDirection(TileSide.LEFT) != null
		    && GetTileObjectAtDirection(TileSide.RIGHT) != null
		    && GetTileObjectAtDirection(TileSide.BOTTOM) != null )
		{
			if ( GetTileObjectAtDirection(TileSide.TOP_RIGHT) == null
			    && GetTileObjectAtDirection(TileSide.BOTTOM_RIGHT) == null
			    && GetTileObjectAtDirection(TileSide.BOTTOM_LEFT) == null
			    && GetTileObjectAtDirection(TileSide.TOP_LEFT) == null ) {
				_spriteManager.SetFrameTo("default", (int)TileSide.FOURWAY_IN);
				return true;
			}
			else if ( GetTileObjectAtDirection(TileSide.BOTTOM_LEFT) != null
			    && GetTileObjectAtDirection(TileSide.TOP_RIGHT) == null
			    && GetTileObjectAtDirection(TileSide.BOTTOM_RIGHT) == null		 
			    && GetTileObjectAtDirection(TileSide.TOP_LEFT) == null ) {
				_spriteManager.SetFrameTo("default", (int)TileSide.TRIPLE_IN_TOP_RIGHT);
				return true;
			}
			else if ( GetTileObjectAtDirection(TileSide.TOP_RIGHT) == null
		         && GetTileObjectAtDirection(TileSide.TOP_LEFT) != null
		         && GetTileObjectAtDirection(TileSide.BOTTOM_RIGHT) == null
		         && GetTileObjectAtDirection(TileSide.BOTTOM_LEFT) == null ) {
				_spriteManager.SetFrameTo("default", (int)TileSide.TRIPLE_IN_BOTTOM_RIGHT);
				return true;
			}
			else if ( GetTileObjectAtDirection(TileSide.TOP_RIGHT) != null
		         && GetTileObjectAtDirection(TileSide.BOTTOM_RIGHT) == null
		         && GetTileObjectAtDirection(TileSide.BOTTOM_LEFT) == null
		         && GetTileObjectAtDirection(TileSide.TOP_LEFT) == null ) {
				_spriteManager.SetFrameTo("default", (int)TileSide.TRIPLE_IN_BOTTOM_LEFT);
				return true;
			}
			else if ( GetTileObjectAtDirection(TileSide.BOTTOM_RIGHT) != null
		         && GetTileObjectAtDirection(TileSide.TOP_RIGHT) == null
		         && GetTileObjectAtDirection(TileSide.BOTTOM_LEFT) == null
		         && GetTileObjectAtDirection(TileSide.TOP_LEFT) == null ) {
				_spriteManager.SetFrameTo("default", (int)TileSide.TRIPLE_IN_TOP_LEFT);
				return true;
			}
		}

		return false;
	}

	private bool CheckForDoubleInwardNeighbors ()
	{
		if ( GetTileObjectAtDirection(TileSide.TOP) != null
		         && GetTileObjectAtDirection(TileSide.LEFT) != null
		         && GetTileObjectAtDirection(TileSide.RIGHT) != null
		         && GetTileObjectAtDirection(TileSide.BOTTOM) != null
		         && GetTileObjectAtDirection(TileSide.TOP_RIGHT) == null
		         && GetTileObjectAtDirection(TileSide.TOP_LEFT) == null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.DOUBLE_IN_TOP);
			return true;
		}
		else if ( GetTileObjectAtDirection(TileSide.TOP) != null
			    && GetTileObjectAtDirection(TileSide.LEFT) != null
			    && GetTileObjectAtDirection(TileSide.RIGHT) != null
			    && GetTileObjectAtDirection(TileSide.BOTTOM) == null
			    && GetTileObjectAtDirection(TileSide.TOP_RIGHT) == null
			    && GetTileObjectAtDirection(TileSide.TOP_LEFT) == null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.DOUBLE_IN_TOP_W_BAR);
			return true;
		}
		else if ( GetTileObjectAtDirection(TileSide.TOP) != null
		         && GetTileObjectAtDirection(TileSide.RIGHT) != null
		         && GetTileObjectAtDirection(TileSide.BOTTOM) != null
		         && GetTileObjectAtDirection(TileSide.LEFT) != null
		         && GetTileObjectAtDirection(TileSide.TOP_RIGHT) == null
		         && GetTileObjectAtDirection(TileSide.BOTTOM_RIGHT) == null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.DOUBLE_IN_RIGHT);
			return true;
		}
		else if ( GetTileObjectAtDirection(TileSide.TOP) != null
			    && GetTileObjectAtDirection(TileSide.RIGHT) != null
			    && GetTileObjectAtDirection(TileSide.BOTTOM) != null
			    && GetTileObjectAtDirection(TileSide.LEFT) == null
			    && GetTileObjectAtDirection(TileSide.TOP_RIGHT) == null
			    && GetTileObjectAtDirection(TileSide.BOTTOM_RIGHT) == null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.DOUBLE_IN_RIGHT_W_BAR);
			return true;
		}
		else if ( GetTileObjectAtDirection(TileSide.LEFT) != null
		         && GetTileObjectAtDirection(TileSide.RIGHT) != null
		         && GetTileObjectAtDirection(TileSide.BOTTOM) != null	     
		         && GetTileObjectAtDirection(TileSide.TOP) != null
		         && GetTileObjectAtDirection(TileSide.BOTTOM_RIGHT) == null
		         && GetTileObjectAtDirection(TileSide.BOTTOM_LEFT) == null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.DOUBLE_IN_BOTTOM);
			return true;
		}
		else if ( GetTileObjectAtDirection(TileSide.LEFT) != null
		         && GetTileObjectAtDirection(TileSide.RIGHT) != null
		         && GetTileObjectAtDirection(TileSide.BOTTOM) != null	     
		         && GetTileObjectAtDirection(TileSide.TOP) == null
		         && GetTileObjectAtDirection(TileSide.BOTTOM_RIGHT) == null
		         && GetTileObjectAtDirection(TileSide.BOTTOM_LEFT) == null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.DOUBLE_IN_BOTTOM_W_BAR);
			return true;
		}
		else if ( GetTileObjectAtDirection(TileSide.TOP) != null
		         && GetTileObjectAtDirection(TileSide.LEFT) != null
		         && GetTileObjectAtDirection(TileSide.BOTTOM) != null
		         && GetTileObjectAtDirection(TileSide.RIGHT) != null
		         && GetTileObjectAtDirection(TileSide.BOTTOM_LEFT) == null
		         && GetTileObjectAtDirection(TileSide.TOP_LEFT) == null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.DOUBLE_IN_LEFT);
			return true;
		}
		else if ( GetTileObjectAtDirection(TileSide.TOP) != null
			    && GetTileObjectAtDirection(TileSide.LEFT) != null
			    && GetTileObjectAtDirection(TileSide.BOTTOM) != null
		        && GetTileObjectAtDirection(TileSide.RIGHT) == null
			    && GetTileObjectAtDirection(TileSide.BOTTOM_LEFT) == null
			    && GetTileObjectAtDirection(TileSide.TOP_LEFT) == null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.DOUBLE_IN_LEFT_W_BAR);
			return true;
		}

		return false;
	}

	private bool CheckForEdgeNeighbors ()
	{
		if ( GetTileObjectAtDirection(TileSide.TOP) == null
		    && GetTileObjectAtDirection(TileSide.LEFT) == null
		    && GetTileObjectAtDirection(TileSide.RIGHT) == null
		    && GetTileObjectAtDirection(TileSide.BOTTOM) == null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.SINGLE);
			return true;
		}
		else if ( GetTileObjectAtDirection(TileSide.TOP) == null
		    && GetTileObjectAtDirection(TileSide.LEFT) == null
		    && GetTileObjectAtDirection(TileSide.RIGHT) == null
		    && GetTileObjectAtDirection(TileSide.BOTTOM) != null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.EDGE_TOP);
			return true;
		}
		else if ( GetTileObjectAtDirection(TileSide.RIGHT) == null
		         && GetTileObjectAtDirection(TileSide.TOP) == null
		         && GetTileObjectAtDirection(TileSide.BOTTOM) == null
		         && GetTileObjectAtDirection(TileSide.LEFT) != null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.EDGE_RIGHT);
			return true;
		}
		else if ( GetTileObjectAtDirection(TileSide.BOTTOM) == null
		         && GetTileObjectAtDirection(TileSide.RIGHT) == null
		         && GetTileObjectAtDirection(TileSide.LEFT) == null
		         && GetTileObjectAtDirection(TileSide.TOP) != null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.EDGE_BOTTOM);
			return true;
		}
		else if ( GetTileObjectAtDirection(TileSide.LEFT) == null
		         && GetTileObjectAtDirection(TileSide.BOTTOM) == null
		         && GetTileObjectAtDirection(TileSide.TOP) == null
		         && GetTileObjectAtDirection(TileSide.RIGHT) != null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.EDGE_LEFT);
			return true;
		}
		
		return false;
	}

	private bool CheckForTopRightNeighbor ()
	{
		if ( GetTileObjectAtDirection(TileSide.TOP_RIGHT) == null
		    && GetTileObjectAtDirection(TileSide.TOP) != null 
		    && GetTileObjectAtDirection(TileSide.RIGHT) != null)
		{			
			if ( GetTileObjectAtDirection(TileSide.BOTTOM) == null
			    && GetTileObjectAtDirection(TileSide.LEFT) == null) {
				_spriteManager.SetFrameTo("default", (int)TileSide.IN_TOP_RIGHT_OUT_BOTTOM_LEFT);
				return true;
			}
			else if ( GetTileObjectAtDirection(TileSide.LEFT) == null ) {			
				_spriteManager.SetFrameTo("default", (int)TileSide.IN_TOP_RIGHT_OUT_LEFT );
				return true;
			}
			else if ( GetTileObjectAtDirection(TileSide.BOTTOM) == null ) {			
				_spriteManager.SetFrameTo("default", (int)TileSide.IN_TOP_RIGHT_OUT_BOTTOM );
				return true;
			}
			else {
				_spriteManager.SetFrameTo("default", (int)TileSide.IN_TOP_RIGHT);
				return true;
			}
		}

		if ( GetTileObjectAtDirection(TileSide.TOP) == null	
		    && GetTileObjectAtDirection(TileSide.RIGHT) == null 
		    && GetTileObjectAtDirection(TileSide.BOTTOM_LEFT) != null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.TOP_RIGHT);
			return true;
		}

		return false;
	}

	private bool CheckForBottomRightNeighbor ()
	{
		if ( GetTileObjectAtDirection(TileSide.BOTTOM) != null
			&& GetTileObjectAtDirection(TileSide.RIGHT) != null
			&& GetTileObjectAtDirection(TileSide.BOTTOM_RIGHT) == null ) {

			if ( GetTileObjectAtDirection(TileSide.TOP) == null
			    && GetTileObjectAtDirection(TileSide.LEFT) == null) {
				_spriteManager.SetFrameTo("default", (int)TileSide.IN_BOTTOM_RIGHT_OUT_TOP_LEFT);
				return true;
			}
			else if ( GetTileObjectAtDirection(TileSide.LEFT) == null ) {			
				_spriteManager.SetFrameTo("default", (int)TileSide.IN_BOTTOM_RIGHT_OUT_LEFT );
				return true;
			}
			else if ( GetTileObjectAtDirection(TileSide.TOP) == null ) {			
				_spriteManager.SetFrameTo("default", (int)TileSide.IN_BOTTOM_RIGHT_OUT_TOP );
				return true;
			}
			else {
				_spriteManager.SetFrameTo("default", (int)TileSide.IN_BOTTOM_RIGHT);
				return true;
			}
		}

		if ( GetTileObjectAtDirection(TileSide.BOTTOM) == null
	         && GetTileObjectAtDirection(TileSide.RIGHT) == null 
		    && GetTileObjectAtDirection(TileSide.TOP_LEFT) != null  ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.BOTTOM_RIGHT);
			return true;
		}
			
		return false;
	}

	private bool CheckForBottomLeftNeighbor ()
	{
		if ( GetTileObjectAtDirection(TileSide.BOTTOM) != null
	         && GetTileObjectAtDirection(TileSide.LEFT) != null
	         && GetTileObjectAtDirection(TileSide.BOTTOM_LEFT) == null ) 
		{
			if ( GetTileObjectAtDirection(TileSide.TOP) == null
			    && GetTileObjectAtDirection(TileSide.RIGHT) == null) {
				_spriteManager.SetFrameTo("default", (int)TileSide.IN_BOTTOM_LEFT_OUT_TOP_RIGHT);
				return true;
			}
			else if ( GetTileObjectAtDirection(TileSide.TOP) == null ) {			
				_spriteManager.SetFrameTo("default", (int)TileSide.IN_BOTTOM_LEFT_OUT_TOP );
				return true;
			}
			else if ( GetTileObjectAtDirection(TileSide.RIGHT) == null ) {			
				_spriteManager.SetFrameTo("default", (int)TileSide.IN_BOTTOM_LEFT_OUT_RIGHT );
				return true;
			}
			else {
				_spriteManager.SetFrameTo("default", (int)TileSide.IN_BOTTOM_LEFT);
				return true;
			}
		}


		if ( GetTileObjectAtDirection(TileSide.BOTTOM) == null
			&& GetTileObjectAtDirection(TileSide.LEFT) == null 
		    && GetTileObjectAtDirection(TileSide.TOP_RIGHT) != null  ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.BOTTOM_LEFT);
			return true;
		}
			
		return false;
	}

	private bool CheckForTopLeftNeighbor ()
	{
		if ( GetTileObjectAtDirection(TileSide.TOP) != null
	         && GetTileObjectAtDirection(TileSide.LEFT) != null
	         && GetTileObjectAtDirection(TileSide.TOP_LEFT) == null ) 
		{
			if ( GetTileObjectAtDirection(TileSide.BOTTOM) == null
			    && GetTileObjectAtDirection(TileSide.RIGHT) == null) {
				_spriteManager.SetFrameTo("default", (int)TileSide.IN_TOP_LEFT_OUT_BOTTOM_RIGHT );
				return true;
			}
			else if ( GetTileObjectAtDirection(TileSide.BOTTOM) == null ) {			
				_spriteManager.SetFrameTo("default", (int)TileSide.IN_TOP_LEFT_OUT_BOTTOM );
				return true;
			}
			else if ( GetTileObjectAtDirection(TileSide.RIGHT) == null ) {			
				_spriteManager.SetFrameTo("default", (int)TileSide.IN_TOP_LEFT_OUT_RIGHT );
				return true;
			}
			else {
				_spriteManager.SetFrameTo("default", (int)TileSide.IN_TOP_LEFT);
				return true;
			}
		}

		if ( GetTileObjectAtDirection(TileSide.TOP) == null
	        && GetTileObjectAtDirection(TileSide.LEFT) == null 
	    	&& GetTileObjectAtDirection(TileSide.BOTTOM_RIGHT) != null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.TOP_LEFT);		
			return true;
		}

		return false;
	}



	private bool CheckForOppositeNeighbors ()
	{
		if ( GetTileObjectAtDirection(TileSide.TOP) == null
		         && GetTileObjectAtDirection(TileSide.BOTTOM) == null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.TOP_BOTTOM);
			return true;
		}
		else if ( GetTileObjectAtDirection(TileSide.RIGHT) == null
		         && GetTileObjectAtDirection(TileSide.LEFT) == null )
		{
			_spriteManager.SetFrameTo("default", (int)TileSide.RIGHT_LEFT);
			return true;
		}

		return false;
	}

	bool CheckForNormalNeighbors ()
	{
		if ( GetTileObjectAtDirection(TileSide.TOP) == null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.TOP);
			return true;
		}
		else if ( GetTileObjectAtDirection(TileSide.RIGHT) == null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.RIGHT);
			return true;
		}
		else if ( GetTileObjectAtDirection(TileSide.BOTTOM) == null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.BOTTOM);
			return true;
		}
		else if ( GetTileObjectAtDirection(TileSide.LEFT) == null ) {
			_spriteManager.SetFrameTo("default", (int)TileSide.LEFT);
			return true;
		}

		return false;
	}

	private Tile GetTileObjectAtDirection(TileSide tileSideDirection) {

		Vector2 directionOffset = _tileSideDictionary[tileSideDirection];
		Vector3 _currentPosition = transform.parent.position;
		Vector3 topPosition = new Vector3(_currentPosition.x + directionOffset.x
		                                  , _currentPosition.y + directionOffset.y
		                                  , _currentPosition.z);
		LevelObject levelObject = Registry.map.GetLevelObjectAtPosition(topPosition);
		if ( levelObject is Tile && !(levelObject is HazardTile))
			return (Tile)levelObject;
		else
			return null;
	}
}
