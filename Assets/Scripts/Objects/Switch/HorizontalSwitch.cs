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
        if (isTriggered)
        {
            if (Registry.inputHandler.UseButton)
            { 
                triggeredCollider.gameObject.GetComponent<MinibotController>().InvertHorizontally();
            }
        }
    }
}
