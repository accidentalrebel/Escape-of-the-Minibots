using UnityEngine;
using System.Collections;

public class StepSwitch : Switch {

    void OnTriggerEnter(Collider col)
    {
        objectToActivate.Use();
    }

    void OnTriggerExit()
    {
        objectToActivate.Use();
    }
}
