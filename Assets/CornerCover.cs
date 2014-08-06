using UnityEngine;
using System.Collections;

public class CornerCover : MonoBehaviour {

	public enum CornerLocation 
	{
		TopLeft,
		TopRight,
		BottomRight,
		BottomLeft
	};

	public CornerLocation cornerLocation = CornerLocation.TopLeft; 
	public Camera guiCamera;

	void Start () 
	{
		repositionCornerCover();
	}

	void repositionCornerCover() 
	{
		var screenHeight = 4 * Camera.main.orthographicSize;
		var screenWidth = screenHeight * Camera.main.aspect;
		Vector3 coverSize = renderer.bounds.size;
		Vector3 newPosition;

		if ( cornerLocation == CornerLocation.TopLeft )
			newPosition = new Vector3(-screenWidth / 2 + coverSize.x / 2, screenHeight / 2 - coverSize.y / 2, -1);
		else if ( cornerLocation == CornerLocation.TopRight )
			newPosition = new Vector3(screenWidth / 2 - coverSize.x / 2, screenHeight / 2 - coverSize.y / 2, -1);
		else if ( cornerLocation == CornerLocation.BottomRight )
			newPosition = new Vector3(screenWidth / 2 - coverSize.x / 2, -screenHeight / 2 + coverSize.y / 2, -1);
		else
			newPosition = new Vector3(-screenWidth / 2 + coverSize.x / 2, -screenHeight / 2 + coverSize.y / 2, -1);

		gameObject.transform.localPosition = newPosition;
	}
}
