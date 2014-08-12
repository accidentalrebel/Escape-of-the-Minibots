using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerTest : MonoBehaviour {

	CharacterController _characterController;

	// Use this for initialization
	void Start () {
		_characterController = (CharacterController)transform.GetComponent("CharacterController");
	}

	Vector3 moveDirection = Vector3.zero;
	float speed = 3.0f;
	float gravity = 20.0f;
	float rotateSpeed = 3.0f;
	float jumpSpeed = 8.0f;

	void Update () {
		//if ( Registry.inputHandler.XAxis != 0 )
		//	_characterController.SimpleMove(new Vector3(Registry.inputHandler.XAxis * 2, 0, 0));

		Vector3 forward= transform.TransformDirection(Vector3.right);
		float curSpeed = speed * Registry.inputHandler.XAxis;

		//transform.Rotate(0,0,Registry.inputHandler.XAxis * rotateSpeed);
		//transform.Rotate(0,0,Input.GetAxis("Unhorizontal") * -rotateSpeed);
		
		_characterController.SimpleMove(forward * curSpeed);
		
		if (Registry.inputHandler.JumpButton && _characterController.isGrounded) 
			moveDirection.y= jumpSpeed;
		
		moveDirection.y -= gravity * Time.deltaTime;
		
		_characterController.Move(moveDirection * Time.deltaTime);
	}
}
