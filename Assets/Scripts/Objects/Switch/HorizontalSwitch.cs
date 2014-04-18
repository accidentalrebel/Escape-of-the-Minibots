using UnityEngine;
using System.Collections;

public class HorizontalSwitch : Switch {

    override protected void Start()
    {

    }
	
	override public void Initialize(Vector3 theStartingPos)
	{
		base.Initialize(theStartingPos);
	}

    void Update()
    {
        if (_isTriggered)
        {
            if (Registry.inputHandler.UseButton)
            { 
                _triggeredCollider.gameObject.GetComponent<MinibotController>().InvertHorizontally();
            }
        }
    }
}
