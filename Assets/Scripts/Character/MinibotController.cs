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

    internal bool InvertGravity
    {
        set { 
            invertGravity = value; 
            UpdateGravity(); 
        }
        get { return invertGravity; }
    }

    private void UpdateGravity()
    {
        if (invertGravity == true && gravity > 0)
            gravity = -gravity;
        else if (invertGravity == false && gravity < 0)
            gravity = -gravity;
    }

    private bool grounded = false;
    private CapsuleCollider capsule;
    private Minibot player;

    void Awake()
    {
        rigidbody.freezeRotation = true;
        rigidbody.useGravity = false;
        capsule = GetComponent<CapsuleCollider>();
        if (capsule == null)
            Debug.LogError("capsule was not found!");
        player = GetComponent<Minibot>();
        if (player == null)
            Debug.LogError("player not found!");

        if (invertGravity)
            UpdateGravity();
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

        UpdateGravity();
    }

    void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        if (invertGravity)
            xInput = -xInput;
        if (invertHorizontal)        
            xInput = -xInput;

        // We then handle whether we play the standing or walking animation
        if (xInput == 0)
            player.Standing();
        else
            player.Walking();

        // We then handle the facing
        if (xInput > 0)   // If a negative number
            player.IsFacing = Minibot.Direction.Right;
        else if ( xInput < 0)
            player.IsFacing = Minibot.Direction.Left;

        // Calculate how fast we should be moving
        Vector3 targetVelocity = new Vector3(xInput, 0, yInput);
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = rigidbody.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        if (CheckIfCanMove(velocityChange))
        {
            rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        if (grounded)
        {
            // Jump
            if (canJump && Input.GetButton("Jump"))
            {
                if ( invertGravity )
                    rigidbody.velocity = new Vector3(velocity.x, -CalculateJumpVerticalSpeed(), velocity.z);
                else
                    rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);

                player.Jumped();                
            }
        }

        // We apply gravity manually for more tuning control
        rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));

        grounded = false;
    }

    private bool CheckIfCanMove(Vector3 velocityChange)
    {
        if ( velocityChange.x < 0 )
        {
            if (player.GetObjectAtSide(Minibot.Direction.Left) != null)
                return false;
        }
        else if (velocityChange.x > 0)
        {
            if (player.GetObjectAtSide(Minibot.Direction.Right) != null)
                return false;
        }

        return true;
    }

    void OnCollisionStay(Collision col)
    {
        CheckIfGrounded();

        // The following checks if the player has just landed from a jump
        if (player.isJumping && grounded)
            player.Grounded();  // If so, tell the player that he is now grounded
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
                grounded = true;
            }
        }
    }
}