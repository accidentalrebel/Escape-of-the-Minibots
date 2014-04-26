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

	[SerializeField]
	private bool _isInverted = false;
	public bool IsInverted {
		get {
			return _isInverted;
		}
		set {
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
		float gravityToUse = Mathf.Abs(_gravity);
		if ( _isInverted == true )
			gravityToUse = -gravityToUse;

		rigidbody.AddForce(new Vector3(0, -gravityToUse * rigidbody.mass, 0));
	}

	public void InvertGravity()
	{
		if ( _isInverted )
			_isInverted = false;
		else
			_isInverted = true;

		if ( OnGravityChanged != null )
			OnGravityChanged();
	}
}