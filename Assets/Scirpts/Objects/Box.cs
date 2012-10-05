using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

    private Rigidbody theRigidBody;

    void Start()
    {
        theRigidBody = GetComponent<Rigidbody>();
        if (theRigidBody == null)
            Debug.LogError("theRigidBody is not found!");
    }

    internal void PickedUp()
    {
        theRigidBody.useGravity = false;
    }
}
