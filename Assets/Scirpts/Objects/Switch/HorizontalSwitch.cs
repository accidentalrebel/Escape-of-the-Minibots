using UnityEngine;
using System.Collections;

public class HorizontalSwitch : Switch {

    void Start()
    {

    }
	
	internal void Initialize(Vector3 theStartingPos)
	{
		base.Initialize(theStartingPos);
	}

    void Update()
    {
        if (isTriggered)
        {
            if (Input.GetKeyUp(KeyCode.X))
            { 
                triggeredCollider.gameObject.GetComponent<RigidBodyFPSController>().InvertHorizontal();
            }
        }
    }
}
