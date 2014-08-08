using UnityEngine;
using System.Collections;

public class CornerCover : MonoBehaviour {

	public enum CornerLocation 
	{
		TOP_LEFT,
		TOP_RIGHT,
		BOTTOM_RIGHT,
		BOTTOM_LEFT
	};

	public CornerLocation cornerLocation = CornerLocation.TOP_LEFT;

	void Start () 
	{
		repositionCornerCover();
	}

	void repositionCornerCover() 
	{
		float screenHeight = 4 * Camera.main.orthographicSize;
		float screenWidth = screenHeight * Camera.main.aspect;
		Vector3 coverSize = renderer.bounds.size;
		Vector3 newPosition;

		if ( cornerLocation == CornerLocation.TOP_LEFT )
			newPosition = new Vector3(-screenWidth / 2 + coverSize.x / 2, screenHeight / 2 - coverSize.y / 2, -1);
		else if ( cornerLocation == CornerLocation.TOP_RIGHT )
			newPosition = new Vector3(screenWidth / 2 - coverSize.x / 2, screenHeight / 2 - coverSize.y / 2, -1);
		else if ( cornerLocation == CornerLocation.BOTTOM_RIGHT )
			newPosition = new Vector3(screenWidth / 2 - coverSize.x / 2, -screenHeight / 2 + coverSize.y / 2, -1);
		else
			newPosition = new Vector3(-screenWidth / 2 + coverSize.x / 2, -screenHeight / 2 + coverSize.y / 2, -1);

		gameObject.transform.localPosition = newPosition;
	}
}
