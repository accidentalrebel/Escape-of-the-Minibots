using UnityEngine;
using System.Collections;

public class Box : LevelObject {

    private Rigidbody theRigidBody;

    override protected void Start()
    {
        theRigidBody = GetComponent<Rigidbody>();
        if (theRigidBody == null)
            Debug.LogError("theRigidBody is not found!");

        base.Start();
    }
	
	override public void Initialize(Vector3 theStartingPos)
	{
		base.Initialize(theStartingPos);
	}

    void PickUp()
    {
        theRigidBody.useGravity = false;
    }

    void PutDown()
    {
        theRigidBody.useGravity = true;
    }

    override public void ResetObject()
    {
        theRigidBody.velocity = Vector3.zero;
        theRigidBody.angularVelocity = Vector3.zero;
        theRigidBody.useGravity = true;

        base.ResetObject();
    }
}
