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
		if ( cornerLocation == CornerLocation.TopLeft ) {
			var screenHeight = 4 * Camera.main.orthographicSize;
			var screenWidth = screenHeight * Camera.main.aspect;
			Vector3 coverSize = renderer.bounds.size;

			gameObject.transform.localPosition = new Vector3(-screenWidth / 2 + coverSize.x / 2, screenHeight / 2 - coverSize.y / 2, -1);
		}
	}
}
