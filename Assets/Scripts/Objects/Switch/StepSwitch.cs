using UnityEngine;
using System.Collections;

public class StepSwitch : Switch {
    
	override protected void Awake ()
	{
		base.Awake ();

		_isTriggered = false;
		UpdateSwitchGraphic();
	}

    void Trigger()
    {
		bool status = _isTriggered;

        LevelObject objectToUse;
        if (posOfObjectToActivate1 != Vector3.zero)
        {
            objectToUse = _map.GetLevelObjectAtPosition(posOfObjectToActivate1);
            objectToUse.Use(status);
        }
        if (posOfObjectToActivate2 != Vector3.zero)
        {
            objectToUse = _map.GetLevelObjectAtPosition(posOfObjectToActivate2);
            objectToUse.Use(status);
        }
        if (posOfObjectToActivate3 != Vector3.zero)
        {
            objectToUse = _map.GetLevelObjectAtPosition(posOfObjectToActivate3);
            objectToUse.Use(status);
        }
    }

	public override void Use ()
	{
		// Intentionally left blank
	}
	
    void OnTriggerEnter(Collider col)
    {
		if (col.tag == "Player" || col.tag == "Box") {
	        Debug.Log("OnTriggerEnter");
			_isTriggered = true;
			Trigger();
			UpdateSwitchGraphic();
		}
    }

    void OnTriggerExit(Collider col)
    {     
		if (col.tag == "Player" || col.tag == "Box" ) {
	        Debug.Log("OnTriggerExit");
			_isTriggered = false;
			Trigger();
			UpdateSwitchGraphic();
		}
    }
}
