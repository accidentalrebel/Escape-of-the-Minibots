using UnityEngine;
using System.Collections;

public class SpriteFlipper : MonoBehaviour {

	bool _isHorizontallyFlipped = false;
	bool _isVerticallyFlipped = false;

	void Start () {

	}

	public void SetFlippedX(bool flipValue) {
		Vector3 currentScale = transform.localScale;
		float currentXScale = Mathf.Abs(currentScale.x);
		
		if ( flipValue && !_isHorizontallyFlipped )
			transform.localScale = new Vector3(-currentXScale, currentScale.y, currentScale.z);
		else
			transform.localScale = new Vector3(currentXScale, currentScale.y, currentScale.z);

		_isHorizontallyFlipped = flipValue;
	}
	
	public void SetFlippedY(bool flipValue)	{
		Vector3 currentScale = transform.localScale;
		Vector3 currentPosition = transform.localPosition;
		float currentYScale = Mathf.Abs(currentScale.y);
		
		if ( flipValue && !_isVerticallyFlipped ) {
			transform.localScale = new Vector3(currentScale.x, -currentYScale, currentScale.z);
		}
		else {
			transform.localScale = new Vector3(currentScale.x, currentYScale, currentScale.z);
		}

		_isVerticallyFlipped = flipValue;
	}
}
