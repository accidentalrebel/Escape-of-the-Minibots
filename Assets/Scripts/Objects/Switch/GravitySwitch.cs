using UnityEngine;
using System.Collections;

public class GravitySwitch : Switch
{
	override internal void Initialize(Vector3 theStartingPos)
	{
		base.Initialize(theStartingPos);
	}

    void Update()
    {
        if (isTriggered)
        {
            if (Registry.inputHandler.UseButton)
            {
                triggeredCollider.gameObject.GetComponent<MinibotController>().InvertTheGravity();
            }
        }
    }
}
