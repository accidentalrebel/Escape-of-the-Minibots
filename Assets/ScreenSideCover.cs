using UnityEngine;
using System.Collections;

public class ScreenSideCover : MonoBehaviour {

	public enum SideLocation 
	{
		Left,
		Top,
		Right,
		Bottom
	};
	
	public SideLocation sideLocation = SideLocation.Left;

	void Start () {
		repositionSideCover();
	}
	
	void repositionSideCover() 
	{
		var screenHeight = 4 * Camera.main.orthographicSize;
		var screenWidth = screenHeight * Camera.main.aspect;
		Vector3 coverSize = renderer.bounds.size;
		Vector3 newPosition;
		
		if ( sideLocation == SideLocation.Left )
			newPosition = new Vector3(-screenWidth / 2 + coverSize.x / 2, 0, -1);
		else if ( sideLocation == SideLocation.Top )
			newPosition = new Vector3(screenWidth / 2 - coverSize.x / 2, screenHeight / 2 - coverSize.y / 2, -1);
		else if ( sideLocation == SideLocation.Right )
			newPosition = new Vector3(screenWidth / 2 - coverSize.x / 2, -screenHeight / 2 + coverSize.y / 2, -1);
		else
			newPosition = new Vector3(-screenWidth / 2 + coverSize.x / 2, -screenHeight / 2 + coverSize.y / 2, -1);
		
		gameObject.transform.localPosition = newPosition;
	}
}
