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
            if (Input.GetKeyUp(KeyCode.X))
            {
                triggeredCollider.gameObject.GetComponent<MinibotController>().InvertTheGravity();
            }
        }
    }
}
