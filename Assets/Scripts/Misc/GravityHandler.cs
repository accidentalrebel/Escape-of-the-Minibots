using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LevelObject))]
public class GravityHandler : MonoBehaviour
{   
	public delegate void EventHandler();
	public event EventHandler OnGravityChanged;

	[SerializeField]
	private float _gravity = 10.0f;
	public float Gravity {
		get {
			return _gravity;
		}
	}

	[SerializeField]
	private bool _isInverted = false;
	public bool IsInverted {
		get {
			return _isInverted;
		}
		set {
			if ( value != _isInverted ) {
				_isInverted = value;
				UpdateGravityValue();
			}
			else
				_isInverted = value;
		}
	}

	void Awake() {
	
	}

	void FixedUpdate() 
	{
		ApplyGravity();
	}

	void ApplyGravity ()
	{
		rigidbody.AddForce(new Vector3(0, -_gravity * rigidbody.mass, 0));
	}

	public void InvertGravity()
	{
		if ( IsInverted )
			IsInverted = false;
		else
			IsInverted = true;
	}

	void UpdateGravityValue ()
	{
		_gravity = Mathf.Abs(_gravity);
		if ( IsInverted == true )
			_gravity = -_gravity;

		if ( OnGravityChanged != null )
			OnGravityChanged();
	}

	public void Reset (bool verticalOrientation)
	{
		IsInverted = verticalOrientation;
		UpdateGravityValue();
	}
}