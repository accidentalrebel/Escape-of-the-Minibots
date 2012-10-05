using UnityEngine;
using System.Collections;

public class HorizontalSwitch : Switch {

    //void OnTriggerEnter(Collider col)
    //{
    //    Debug.Log("Gravity switch triggered ");
    //    if (col.gameObject.tag == "Player")
    //    {
    //        col.GetComponent<RigidBodyFPSController>().InvertHorizontal();
    //    }
    //}
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
