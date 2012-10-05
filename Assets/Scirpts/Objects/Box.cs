using UnityEngine;
using System.Collections;

public class Box : ItemObject {

    private Rigidbody theRigidBody;

    void Start()
    {
        theRigidBody = GetComponent<Rigidbody>();
        if (theRigidBody == null)
            Debug.LogError("theRigidBody is not found!");
    }

    internal void PickUp()
    {
        theRigidBody.useGravity = false;
    }

    internal void PutDown()
    {
        theRigidBody.useGravity = true;
    }
}
