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
		foreach( LevelObject levelObject in _linkedObjects ) {
			if ( levelObject != null )
				levelObject.Use(_isTriggered);
		}
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
