using UnityEngine;
using System.Collections;

public class StepSwitch : Switch {
	
	override internal void Initialize(Vector3 theStartingPos)
	{
		base.Initialize(theStartingPos);		
	}
	
    void OnTriggerEnter(Collider col)
    {
        LevelObject objectToUse = map.GetLevelObjectAtPosition(posOfObjectToActivate);
        objectToUse.Use();
    }

    void OnTriggerExit()
    {
        LevelObject objectToUse = map.GetLevelObjectAtPosition(posOfObjectToActivate);
        objectToUse.Use();
    }
}
