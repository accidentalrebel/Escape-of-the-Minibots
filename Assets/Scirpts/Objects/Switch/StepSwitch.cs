using UnityEngine;
using System.Collections;

public class StepSwitch : Switch {
	
	internal void Initialize(Vector3 theStartingPos)
	{
		base.Initialize(theStartingPos);		
	}
	
    void OnTriggerEnter(Collider col)
    {
        objectToActivate.Use();
    }

    void OnTriggerExit()
    {
        objectToActivate.Use();
    }
}
