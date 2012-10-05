using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class RigidBodyFPSController : MonoBehaviour
{
    enum Direction { Left, Right };

    public float speed = 10.0f;
    public float gravity = 10.0f;    
    public float maxVelocityChange = 10.0f;    
    public float jumpHeight = 2.0f;
    public bool canJump = true;
    public bool invertGravity = false;
    public bool invertHorizontal = false;

    private bool grounded = false;
    private CapsuleCollider capsule;

    void Awake()
    {
        rigidbody.freezeRotation = true;
        rigidbody.useGravity = false;
        capsule = GetComponent<CapsuleCollider>();

        if (invertGravity)
            InvertGravity();
    }

    private void InvertHorizontal()
    {
        if (invertHorizontal)
            invertHorizontal = false;
        else
            invertHorizontal = true;
    }

    private void InvertGravity()
    {
        gravity = -gravity;
    }

    void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        if (invertGravity)
            xInput = -xInput;
        if (invertHorizontal)
            xInput = -xInput;

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
                Debug.Log(CalculateJumpVerticalSpeed());
                if ( invertGravity )
                    rigidbody.velocity = new Vector3(velocity.x, -CalculateJumpVerticalSpeed(), velocity.z);
                else
                    rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
            }
        }

        // We apply gravity manually for more tuning control
        rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));

        grounded = false;
    }

    private bool CheckIfCanMove(Vector3 velocityChange)
    {
        Debug.Log("x Velocity change is " + velocityChange.x);
        if ( velocityChange.x < 0 )
        {
            if (CheckIfThereIsWall(Direction.Left))
                return false;
        }
        else if (velocityChange.x > 0)
        {
            if (CheckIfThereIsWall(Direction.Right))
                return false;
        }

        return true;
    }

    void OnCollisionStay(Collision col)
    {
        CheckIfGrounded();
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
            if (hit.collider.tag == "Tile"
                || hit.collider.tag == "Player")
            {
                Debug.DrawLine(gameObject.transform.position, hit.point);
                grounded = true;
            }
        }
    }

    private bool CheckIfThereIsWall(Direction direction)
    {
        RaycastHit hit;
        Vector3 checkDirection;
        if (direction == Direction.Left)
            checkDirection = Vector3.left;
        else
            checkDirection = Vector3.right;

        if (Physics.Raycast(gameObject.transform.position, checkDirection, out hit, 0.6f))
        {
            if (hit.collider.tag == "Tile" )
            {
                Debug.DrawLine(gameObject.transform.position, hit.point);
                return true;
            }
        }

        return false;
    }
}