using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class MinibotController : MonoBehaviour
{
	[SerializeField]
    private float _speed = 10.0f;

	[SerializeField]
    private float _gravity = 10.0f;

	[SerializeField]
    private float _maxVelocityChange = 10.0f;  

	[SerializeField]
	private float _jumpHeight = 2.0f;
    
	private CapsuleCollider _capsuleCollider;
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

	private bool _isInvertedVertically = false;    
	public bool IsInvertedVertically
	{
		set { 
			_isInvertedVertically = value; 
			UpdateGravityStatus(); 
		}
		get { return _isInvertedVertically; }
	}


	// ************************************************************************************
	// MAIN
	// ************************************************************************************
    void Awake()
    {
        rigidbody.freezeRotation = true;
        rigidbody.useGravity = false;

        _capsuleCollider = GetComponent<CapsuleCollider>();
        if (_capsuleCollider == null)
            Debug.LogError("capsule collider was not found!");
        
		_playerScript = GetComponent<Minibot>();
        if (_playerScript == null)
            Debug.LogError("player not found!");

        if (_isInvertedVertically)
            UpdateGravityStatus();
    }

    void FixedUpdate()
    {
		float yInput = Registry.inputHandler.YAxis;
		float xInput = AdjustXInput(Registry.inputHandler.XAxis);
		
		_playerScript.setFacingValueWithXinput(xInput);

		Vector3 targetVelocity = CalculateTargetVelocity(xInput, yInput);
        Vector3 currentVelocity = rigidbody.velocity;
		Vector3 velocityChange = getForceThatCanReachTargetVelocity(currentVelocity, targetVelocity);

		if (_playerScript.IsJumping && _isGrounded)
			_playerScript.OnReachedGround();

		_playerScript.setPlayerAnimationsWithXInput(xInput);

        if (CheckIfCanMove(velocityChange)) {
            rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        }
		else
		{
			Vector3 tVelocity = rigidbody.velocity;
			tVelocity.x = 0;
			rigidbody.velocity = tVelocity;
		}

		if (Registry.inputHandler.JumpButton && _isGrounded && _canJump ) {
	        if ( _isInvertedVertically )
	            rigidbody.velocity = new Vector3(currentVelocity.x, -CalculateJumpVerticalSpeed(), currentVelocity.z);
	        else
	            rigidbody.velocity = new Vector3(currentVelocity.x, CalculateJumpVerticalSpeed(), currentVelocity.z);

	        _playerScript.Jump();                       
        }

        rigidbody.AddForce(new Vector3(0, -_gravity * rigidbody.mass, 0));
		_isGrounded = false;
    }

	Vector3 getForceThatCanReachTargetVelocity (Vector3 currentVelocity, Vector3 targetVelocity)
	{
		Vector3 adjustedVelocity = (targetVelocity - currentVelocity);
		adjustedVelocity.x = Mathf.Clamp(adjustedVelocity.x, -_maxVelocityChange, _maxVelocityChange);
		adjustedVelocity.z = Mathf.Clamp(adjustedVelocity.z, -_maxVelocityChange, _maxVelocityChange);
		adjustedVelocity.y = 0;

		return adjustedVelocity;
	}

	// ************************************************************************************
	// VELOCITY
	// ************************************************************************************
	Vector3 CalculateTargetVelocity (float xInput, float yInput)
	{
		Vector3 targetVelocity = new Vector3(xInput, 0, yInput);
		targetVelocity = transform.TransformDirection(targetVelocity);
		targetVelocity *= _speed;

		return targetVelocity;
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
		if (_isInvertedVertically)
			xInput = -xInput;
		if (_isInvertedHorizontally)
			xInput = -xInput;

		return xInput;
	}
	
	private void UpdateGravityStatus()
	{
		if ((_isInvertedVertically == true && _gravity > 0)
		    || (_isInvertedVertically == false && _gravity < 0))
			_gravity = -_gravity;
	}

	public void InvertHorizontally()
	{
		if (_isInvertedHorizontally)
			_isInvertedHorizontally = false;
		else
			_isInvertedHorizontally = true;
	}
	
	public void InvertVertically()
	{
		if (_isInvertedVertically)
			_isInvertedVertically = false;
		else
			_isInvertedVertically = true;

		UpdateGravityStatus();
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
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        if ( _isInvertedVertically )
            return Mathf.Sqrt(2 * _jumpHeight * -_gravity);
        else
            return Mathf.Sqrt(2 * _jumpHeight * _gravity);
    }

    void CheckIfGrounded()
    {
        RaycastHit hit;
        Vector3 checkDirection;
        if ( _isInvertedVertically )
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
		_isInvertedVertically = initVerticalValue;

		UpdateGravityStatus();
	}
}