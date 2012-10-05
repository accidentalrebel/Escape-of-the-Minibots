using UnityEngine;
using System.Collections;

public class GravitySwitch : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Gravity switch triggered ");
        if (col.gameObject.tag == "Player")
        {
            col.GetComponent<RigidBodyFPSController>().InvertGravity();
        }
    }
}
