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
		foreach( LevelObject levelObject in _linkedObjects ) {
			if ( levelObject == null )
				continue;

			Debug.Log ("GOING THROUGH LINKEDOBJECTS");
			GravityHandler gravityHandler = levelObject.GetComponent<GravityHandler>();
			if ( gravityHandler == null )
				continue;

			Debug.Log ("GOING THROUGH GRAVITYHANDLER");
			gravityHandler.InvertGravity();
		}
	}
}
