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
				Debug.Log ("IS INVERTING");

				Minibot minibotScript = _triggeredCollider.gameObject.GetComponent<Minibot>();
				minibotScript.InvertVerticalOrientation();
            }
        }
    }
}
