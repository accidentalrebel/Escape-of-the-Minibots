using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GravityHandler))]
public class Box : LevelObject {

    private Rigidbody theRigidBody;
	private bool _isInvertedVertically = false;
	private bool _initVerticalOrientation = false;
	public bool InitVerticalOrientation {
		get {
			return _initVerticalOrientation;
		}
	}

	private GravityHandler _gravityHandler;

	protected override void Awake ()
	{
		base.Awake ();

		_gravityHandler = gameObject.GetComponent<GravityHandler>();
	}

    override protected void Start()
    {
        theRigidBody = GetComponent<Rigidbody>();
        if (theRigidBody == null)
            Debug.LogError("theRigidBody is not found!");

        base.Start();
    }
	
	public void Initialize(Vector3 theStartingPos, bool initVerticalOrientation)
	{
		base.Initialize(theStartingPos);

		_initVerticalOrientation = initVerticalOrientation;
		_gravityHandler.IsInverted = _initVerticalOrientation;
	}

	public void PickUp()
    {
        //theRigidBody.useGravity = false;
    }

	public void PutDown()
    {
        //theRigidBody.useGravity = true;
    }

    override public void ResetObject()
    {
        theRigidBody.velocity = Vector3.zero;
        theRigidBody.angularVelocity = Vector3.zero;
        //theRigidBody.useGravity = true;

        base.ResetObject();
    }

	public override void GetEditableAttributes (LevelEditor levelEditor)
	{
		_gravityHandler.IsInverted = GUI.Toggle(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 110, 110, 20), _gravityHandler.IsInverted, "Invert Gravity");
		_initVerticalOrientation =  _gravityHandler.IsInverted;
	}
}
