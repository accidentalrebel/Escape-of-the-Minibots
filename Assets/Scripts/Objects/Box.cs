using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GravityHandler))]
[RequireComponent(typeof(Rigidbody))]
public class Box : LevelObject {

    private Rigidbody _rigidBody;
	private bool _isInvertedVertically = false;
	private GravityHandler _gravityHandler;

	private bool _initVerticalOrientation = false;
	public bool InitVerticalOrientation {
		get {
			return _initVerticalOrientation;
		}
	}

	protected override void Awake ()
	{
		base.Awake ();

		_gravityHandler = gameObject.GetComponent<GravityHandler>();
		_rigidBody = GetComponent<Rigidbody>();
	}
	
	public void Initialize(Vector3 theStartingPos, bool initVerticalOrientation)
	{
		base.Initialize(theStartingPos);

		_initVerticalOrientation = initVerticalOrientation;
		_gravityHandler.IsInverted = _initVerticalOrientation;
	}

    override public void ResetObject()
    {
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;

		_gravityHandler.Reset(_initVerticalOrientation);

        base.ResetObject();
    }

	public override void GetEditableAttributes (LevelEditor levelEditor)
	{
		Rect guiRect = new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 110, 110, 20);
		_gravityHandler.IsInverted = GUI.Toggle(guiRect, _gravityHandler.IsInverted, "Invert Gravity");
		_initVerticalOrientation =  _gravityHandler.IsInverted;
	}
}
