using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class MinibotController : MonoBehaviour
{
    public float speed = 10.0f;
    public float gravity = 10.0f;    
    public float maxVelocityChange = 10.0f;    
    public float jumpHeight = 2.0f;

    public bool canJump = true;
    public bool invertGravity = false;
    public bool invertHorizontal = false;

    private bool isGrounded = false;
    private CapsuleCollider capsuleCollider;
    private Minibot player;

    void Awake()
    {
        rigidbody.freezeRotation = true;
        rigidbody.useGravity = false;

        capsuleCollider = GetComponent<CapsuleCollider>();
        if (capsuleCollider == null)
            Debug.LogError("capsule collider was not found!");
        
		player = GetComponent<Minibot>();
        if (player == null)
            Debug.LogError("player not found!");

        if (invertGravity)
            UpdateGravityStatus();
    }

    void FixedUpdate()
    {
		float yInput = Registry.inputHandler.YAxis;
		float xInput = AdjustXInput(Registry.inputHandler.XAxis);
		
		HandlePlayerFacing(xInput);

		Vector3 targetVelocity = CalculateTargetVelocity(xInput, yInput);

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = rigidbody.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

		if (player.isJumping && isGrounded)
			player.OnReachedGround();

		HandlePlayerSprite(xInput);

        if (CheckIfCanMove(velocityChange)) {
            rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        }
		else
		{
			Vector3 tVelocity = rigidbody.velocity;
			tVelocity.x = 0;
			rigidbody.velocity = tVelocity;
		}

		if (Registry.inputHandler.JumpButton && isGrounded && canJump ) {
	        if ( invertGravity )
	            rigidbody.velocity = new Vector3(velocity.x, -CalculateJumpVerticalSpeed(), velocity.z);
	        else
	            rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);

	        player.Jump();                       
        }

        rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));
		isGrounded = false;
    }

	Vector3 CalculateTargetVelocity (float xInput, float yInput)
	{
		Vector3 targetVelocity = new Vector3(xInput, 0, yInput);
		targetVelocity = transform.TransformDirection(targetVelocity);
		targetVelocity *= speed;

		return targetVelocity;
	}

	void OnCollisionStay(Collision col)
	{
		CheckIfGrounded();
	}

	void OnCollisionExit(Collision col)
	{
		CheckIfGrounded();
	}

	void HandlePlayerFacing (float xInput)
	{
		if (xInput > 0) {
			player.IsFacing = Minibot.Direction.Right;
		}
		else if (xInput < 0) {
			player.IsFacing = Minibot.Direction.Left;
		}
	}

	void HandlePlayerSprite (float xInput)
	{
		if ( player.isJumping )
			return;

		if (xInput != 0 )
			player.Walk();
		else
			player.Stand();
	}

	float AdjustXInput(float xInput)
	{
		if (invertGravity)
			xInput = -xInput;
		if (invertHorizontal)
			xInput = -xInput;

		return xInput;
	}

	internal bool InvertGravity
	{
		set { 
			invertGravity = value; 
			UpdateGravityStatus(); 
		}
		get { return invertGravity; }
	}
	
	private void UpdateGravityStatus()
	{
		if ((invertGravity == true && gravity > 0)
		    || (invertGravity == false && gravity < 0))
			gravity = -gravity;
	}

	internal void InvertHorizontal()
	{
		if (invertHorizontal)
			invertHorizontal = false;
		else
			invertHorizontal = true;
	}
	
	internal void InvertTheGravity()
	{
		if (invertGravity)
			invertGravity = false;
		else
			invertGravity = true;
		
		UpdateGravityStatus();
	}

    private bool CheckIfCanMove(Vector3 velocityChange)
    {
        if (( velocityChange.x < 0
		    && player.GetObjectAtSide(Minibot.Direction.Left) != null )
        	|| 
		    (velocityChange.x > 0
		    && player.GetObjectAtSide(Minibot.Direction.Right) != null))
			return false;

        return true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        if ( invertGravity )
            return Mathf.Sqrt(2 * jumpHeight * -gravity);
        else
            return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    void CheckIfGrounded()
    {
        RaycastHit hit;
        Vector3 checkDirection;
        if ( invertGravity )
            checkDirection = Vector3.up;
        else
            checkDirection = Vector3.down;

        if (Physics.Raycast(gameObject.transform.position, checkDirection , out hit, 0.6f))
        {
            if (hit.collider.tag == "Steppable"
                || hit.collider.tag == "Player"
                || hit.collider.tag == "Movable")
            {
                Debug.DrawLine(gameObject.transform.position, hit.point);
                isGrounded = true;
				return;
            }
        }

		isGrounded = false;
    }
}