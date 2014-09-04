using UnityEngine;
using System.Collections;

public class GravitySwitch : Switch
{   
	protected override void Start ()
	{
		base.Start ();

		HandleSpriteFlipping();
	}

	void LateUpdate()
    {
        if (_isTriggered)
        {
            if (Registry.inputHandler.useButton)
            {
				Use();
            }
        }
    }

	public override void Use ()
	{	
		Registry.sfxManager.PlaySFX(Registry.sfxManager.SFXGravitySwitch);

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
