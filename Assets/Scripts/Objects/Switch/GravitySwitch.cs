using UnityEngine;
using System.Collections;

public class GravitySwitch : Switch
{   
	void LateUpdate()
    {
        if (_isTriggered)
        {
            if (Registry.inputHandler.UseButton)
            {
				Use();
            }
        }
    }

	public override void Use ()
	{
		Minibot minibotScript = _triggeredCollider.gameObject.GetComponent<Minibot>();
		minibotScript.InvertVerticalOrientation();
		
		foreach( LevelObject levelObject in _linkedObjects ) {
			Debug.Log ("GOING THROUGH LINKEDOBJECTS");
			if ( levelObject == null )
				continue;

			GravityHandler gravityHandler = levelObject.GetComponent<GravityHandler>();
			if ( gravityHandler == null )
				continue;

			gravityHandler.InvertGravity();
		}
	}
}
