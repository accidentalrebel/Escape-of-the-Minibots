using UnityEngine;
using System.Collections;

public class StepSwitch : Switch {
    
	override internal void Initialize(Vector3 theStartingPos)
	{
		base.Initialize(theStartingPos);		
	}

    void Trigger(bool status)
    {
        LevelObject objectToUse;
        if (posOfObjectToActivate1 != Vector3.zero)
        {
            objectToUse = map.GetLevelObjectAtPosition(posOfObjectToActivate1);
            objectToUse.Use(status);
        }
        if (posOfObjectToActivate2 != Vector3.zero)
        {
            objectToUse = map.GetLevelObjectAtPosition(posOfObjectToActivate2);
            objectToUse.Use(status);
        }
        if (posOfObjectToActivate3 != Vector3.zero)
        {
            objectToUse = map.GetLevelObjectAtPosition(posOfObjectToActivate3);
            objectToUse.Use(status);
        }
    }
	
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("OnTriggerEnter");
        Trigger(true);        
    }

    void OnTriggerExit(Collider col)
    {        
        Debug.Log("OnTriggerExit");
        Trigger(false);
    }

    internal override void ResetObject()
    {
        Debug.LogWarning("Resetting");
        base.ResetObject();
        isTriggered = false;
    }
}
