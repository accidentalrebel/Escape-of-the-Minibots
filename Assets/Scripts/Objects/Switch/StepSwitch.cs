using UnityEngine;
using System.Collections;

public class StepSwitch : Switch {
	
	override internal void Initialize(Vector3 theStartingPos)
	{
		base.Initialize(theStartingPos);		
	}

    void Trigger()
    {
        LevelObject objectToUse;
        if (posOfObjectToActivate1 != Vector3.zero)
        {
            objectToUse = map.GetLevelObjectAtPosition(posOfObjectToActivate1);
            objectToUse.Use();
        }
        if (posOfObjectToActivate2 != Vector3.zero)
        {
            objectToUse = map.GetLevelObjectAtPosition(posOfObjectToActivate2);
            objectToUse.Use();
        }
        if (posOfObjectToActivate3 != Vector3.zero)
        {
            objectToUse = map.GetLevelObjectAtPosition(posOfObjectToActivate3);
            objectToUse.Use();
        }
    }
	
    void OnTriggerEnter(Collider col)
    {
        Trigger();
    }

    void OnTriggerExit()
    {
        Trigger();
    }
}
