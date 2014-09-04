using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(GravityHandler))]
public class MinibotController : MonoBehaviour
{
	[SerializeField]
    private float _speed = 10.0f;

	[SerializeField]
    private float _maxVelocityChange = 10.0f;  

	[SerializeField]
	private float _jumpHeight = 2.0f;
    
	private CapsuleCollider _capsuleCollider;
	private GravityHandler _gravityHandler;
	private Minibot _playerScript;
    
	private bool _canJump = true;
	private bool _isGrounded = false;
	private bool _isInvertedHorizontally = false;

	public bool IsInvertedHorizontally {
		get {
				return _isInvertedHorizontally;
		}
		set {
				_isInvertedHorizontally = value;
		}
	}

	// ************************************************************************************
	// MAIN
	// ************************************************************************************
    void Awake()
    {
        rigidbody.freezeRotation = true;
        rigidbody.useGravity = false;

		_gravityHandler = GetComponent<GravityHandler>();
		if ( _gravityHandler == null )
			Debug.LogError("gravityHandler was not found!");

        _capsuleCollider = GetComponent<CapsuleCollider>();
        if (_capsuleCollider == null)
            Debug.LogError("capsule collider was not found!");
        
		_playerScript = GetComponent<Minibot>();
        if (_playerScript == null)
            Debug.LogError("player not found!");
    }

    void FixedUpdate()
    {
		float yInput = Registry.inputHandler.yAxis;
		float xInput = AdjustXInput(Registry.inputHandler.xAxis);
		
		_playerScript.SetFacingValueWithXinput(xInput);

		Vector3 targetVelocity = CalculateTargetVelocity(xInput, yInput);
        Vector3 currentVelocity = rigidbody.velocity;
		Vector3 velocityChange = GetForceThatCanReachTargetVelocity(currentVelocity, targetVelocity);

		if (_playerScript.IsJumping && _isGrounded)
			_playerScript.OnReachedGround();

		_playerScript.SetPlayerAnimationsWithXInput(xInput);

        if (CheckIfCanMove(velocityChange))
			ApplyHorizontalForce(velocityChange);
		else
			RemoveHorizontalForce();

		if (Registry.inputHandler.jumpButton && _isGrounded && _canJump ) {
			ApplyJumpForces(currentVelocity);  
			_playerScript.Jump(); 
        }

		_isGrounded = false;
    }

	// ************************************************************************************
	// FORCES
	// ************************************************************************************
	Vector3 CalculateTargetVelocity (float xInput, float yInput)
	{
		Vector3 targetVelocity = new Vector3(xInput, 0, yInput);
		targetVelocity = transform.TransformDirection(targetVelocity);
		targetVelocity *= _speed;
		
		return targetVelocity;
	}

	Vector3 GetForceThatCanReachTargetVelocity (Vector3 currentVelocity, Vector3 targetVelocity)
	{
		Vector3 adjustedVelocity = (targetVelocity - currentVelocity);
		adjustedVelocity.x = Mathf.Clamp(adjustedVelocity.x, -_maxVelocityChange, _maxVelocityChange);
		adjustedVelocity.z = Mathf.Clamp(adjustedVelocity.z, -_maxVelocityChange, _maxVelocityChange);
		adjustedVelocity.y = 0;

		return adjustedVelocity;
	}

	void ApplyJumpForces (Vector3 currentVelocity)
	{
		if ( _gravityHandler.IsInverted )
			rigidbody.velocity = new Vector3(currentVelocity.x, -CalculateJumpVerticalSpeed(), currentVelocity.z);
		else
			rigidbody.velocity = new Vector3(currentVelocity.x, CalculateJumpVerticalSpeed(), currentVelocity.z);		 
	}

	void ApplyHorizontalForce (Vector3 velocityChange)
	{
		rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
	}

	void RemoveHorizontalForce ()
	{
		Vector3 tVelocity = rigidbody.velocity;
		tVelocity.x = 0;
		rigidbody.velocity = tVelocity;
	}

	// ************************************************************************************
	// TRIGGERS
	// ************************************************************************************
	void OnCollisionStay(Collision col)
	{
		CheckIfGrounded();
	}

	void OnCollisionExit(Collision col)
	{
		CheckIfGrounded();
	}

	// ************************************************************************************
	// STATUS
	// ************************************************************************************
	float AdjustXInput(float xInput)
	{
		if ( _gravityHandler.IsInverted )
			xInput = -xInput;
		if (_isInvertedHorizontally)
			xInput = -xInput;

		return xInput;
	}

	public void InvertHorizontally()
	{
		if (_isInvertedHorizontally)
			_isInvertedHorizontally = false;
		else
			_isInvertedHorizontally = true;
	}

	// ************************************************************************************
	// HELPER FUNCTIONS
	// ************************************************************************************
    private bool CheckIfCanMove(Vector3 velocityChange)
    {
        if (( velocityChange.x < 0
		    && _playerScript.GetObjectAtSide(Minibot.Direction.Left) != null )
        	|| 
		    (velocityChange.x > 0
		    && _playerScript.GetObjectAtSide(Minibot.Direction.Right) != null))
			return false;

        return true;
    }

    float CalculateJumpVerticalSpeed()
    {
		if ( _gravityHandler.IsInverted )
            return Mathf.Sqrt(2 * _jumpHeight * -_gravityHandler.Gravity);
        else
			return Mathf.Sqrt(2 * _jumpHeight * _gravityHandler.Gravity);
    }

    void CheckIfGrounded()
    {
        RaycastHit hit;
        Vector3 checkDirection;
		if ( _gravityHandler.IsInverted )
            checkDirection = Vector3.up;
        else
            checkDirection = Vector3.down;

		if (Physics.Raycast(gameObject.transform.position, checkDirection , out hit, 0.6f))
        {
            if (hit.collider.tag == "Steppable"
                || hit.collider.tag == "Player"
                || hit.collider.tag == "Box")
            {
                Debug.DrawLine(gameObject.transform.position, hit.point);
                _isGrounded = true;
				return;
            }
        }

		_isGrounded = false;
    }

	// ************************************************************************************
	// RELEASE
	// ************************************************************************************
	public void Reset (bool initHorizontalValue, bool initVerticalValue)
	{
		_isInvertedHorizontally = initHorizontalValue;		
		_gravityHandler.IsInverted = initVerticalValue;

		rigidbody.velocity = Vector3.zero;
		_isGrounded = false;
	}
}