using UnityEngine;
using System.Collections;

public class GravitySwitch : Switch
{
	override internal void Initialize(Vector3 theStartingPos)
	{
		base.Initialize(theStartingPos);
	}
    
	void LateUpdate()
    {
        if (isTriggered)
        {
            if (Registry.inputHandler.UseButton)
            {
				Debug.Log ("IS INVERTING");

				Minibot minibotScript = triggeredCollider.gameObject.GetComponent<Minibot>();
				minibotScript.InvertVerticalOrientation();
            }
        }
    }
}
