using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LevelObject))]
public class GravityHandler : MonoBehaviour
{   
	[SerializeField]
	private float _gravity = 10.0f;

	[SerializeField]
	private bool _isInverted = false;

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
			_isInverted = false;
	}
}