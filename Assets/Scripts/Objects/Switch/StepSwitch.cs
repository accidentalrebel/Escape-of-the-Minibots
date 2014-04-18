using UnityEngine;
using System.Collections;

public class StepSwitch : Switch {
    
	protected override void Awake ()
	{
		base.Awake ();

		isTriggered = false;
		UpdateSwitchGraphic();
	}

	override internal void Initialize(Vector3 theStartingPos)
	{
		base.Initialize(theStartingPos);
	}

    void Trigger()
    {
		bool status = isTriggered;

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
		if (col.tag == "Player" || col.tag == "Box") {
	        Debug.Log("OnTriggerEnter");
			isTriggered = true;
			Trigger();
			UpdateSwitchGraphic();
		}
    }

    void OnTriggerExit(Collider col)
    {     
		if (col.tag == "Player" || col.tag == "Box" ) {
	        Debug.Log("OnTriggerExit");
			isTriggered = false;
			Trigger();
			UpdateSwitchGraphic();
		}
    }

    internal override void ResetObject()
    {
        Debug.LogWarning("Resetting");
        base.ResetObject();
        isTriggered = false;
    }
}
