using UnityEngine;
using System.Collections;

public class HorizontalSwitch : Switch {

    void Start()
    {

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
