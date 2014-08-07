using UnityEngine;
using System.Collections;

public class ScreenSideCover : MonoBehaviour {

	public enum SideLocation 
	{
		LEFT,
		TOP,
		RIGHT,
		BOTTOM
	};
	
	public SideLocation sideLocation = SideLocation.LEFT;

	void Start () {
		repositionSideCover();
	}
	
	void repositionSideCover() 
	{
		float screenHeight = 4 * Camera.main.orthographicSize;
		float screenWidth = screenHeight * Camera.main.aspect;

		Vector3 coverSize = renderer.bounds.size;
		Vector3 newPosition;

		if ( sideLocation == SideLocation.TOP ) {
			newPosition = new Vector3(0, screenHeight / 2 - coverSize.x / 2, -1);
			gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, -90);
		}
		else if ( sideLocation == SideLocation.RIGHT ) {
			newPosition = new Vector3(screenWidth / 2 - coverSize.x / 2, 0, -1);
			gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, 0);
		}
		else if ( sideLocation == SideLocation.BOTTOM ) {
			newPosition = new Vector3(0, -screenHeight / 2 + coverSize.x / 2, -1);
			gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, 90);
		}
		else
			newPosition = new Vector3(-screenWidth / 2 + coverSize.x / 2, 0, -1);
					
		gameObject.transform.localPosition = newPosition;
	}
}
