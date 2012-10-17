using UnityEngine;
using System.Collections;

public class Minibot : LevelObject {

    public enum Direction { Left, Right };
    private GameObject objectBeingCarried;
    private Rigidbody theRigidBody;    

    RigidBodyFPSController controller;
    private bool startingIsInvertedGravity;
    private bool startingIsInvertedHorizontal;
    public bool hasExited;
    private Direction isFacing = Direction.Right;
    public Direction IsFacing
    {
        set {
            // We only set the value if there is achange in direction
            if (isFacing != value)
            {                   
                isFacing = value;
                HandleSpriteDirection();    // If there is, handle the sprite direction
            }
        }
    }

    // ************************************************************************************
    // MAIN
    // ************************************************************************************

	override protected void Start()
	{
        theRigidBody = GetComponent<Rigidbody>();
        if (theRigidBody == null)
            Debug.LogError("theRigidBody is not found!");

        base.Start();

        InitializeSprite();
	}
	
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)
            && objectBeingCarried != null)
        {
            PutDown(objectBeingCarried);
        }

        // Handles the picking up of objects
        if (Input.GetKeyDown(KeyCode.C)
            && objectBeingCarried ==  null )
        {
            GameObject objectAtSide = GetObjectAtSide(Direction.Right);
            if ( objectAtSide != null
                && objectAtSide.tag == "Movable")
            {
                Debug.Log("There's a box");
                PickUp(objectAtSide);
            }
        }

        // Handles the carrying of the object
        if (objectBeingCarried != null)
        {
            objectBeingCarried.transform.position = transform.position + Vector3.up;
        }
    }
    
    internal void Initialize(Vector3 startPos, bool isInvertedGrav, bool isInvertedHor)
    {
        startingPos = startPos;        
        gameObject.transform.position = startingPos;

        controller = gameObject.GetComponentInChildren<RigidBodyFPSController>();
        controller.InvertGravity = isInvertedGrav;
        controller.invertHorizontal = isInvertedHor;
        startingIsInvertedGravity = isInvertedGrav;
        startingIsInvertedHorizontal = isInvertedHor;
    }

    private void InitializeSprite()
    {
        spriteManager.CreateAnimation("walking", new SpriteManager.AnimationProperties(new int[] { 1, 2, 3 }, 0.2f));    // We create a new walkign animation        
        spriteManager.Play("walking");
    }    

    // ************************************************************************************
    // TRIGGERS
    // ************************************************************************************

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Door")
        {
            if ( col.gameObject.GetComponent<Door>().isOpen )
                ExitLevel();
        }
    }

    // ************************************************************************************
    // ACTIONS
    // ************************************************************************************

    private void PickUp(GameObject objectAtSide)
    {
        objectBeingCarried = objectAtSide;
        objectBeingCarried.GetComponent<Box>().PickUp();
    }

    private void PutDown(GameObject objectToPutDown)
    {
        objectBeingCarried.transform.position = transform.position + Vector3.right;
        objectToPutDown.GetComponent<Box>().PutDown();
        objectBeingCarried = null;
    }

    internal GameObject GetObjectAtSide(Direction direction)
    {
        RaycastHit hit;
        Vector3 checkDirection;
        if (direction == Direction.Left)
            checkDirection = Vector3.left;
        else
            checkDirection = Vector3.right;

        if (Physics.Raycast(gameObject.transform.position, checkDirection, out hit, 0.6f))
        {
            if (hit.collider.tag == "Tile"
                || hit.collider.tag == "Movable")
            {
                Debug.DrawLine(gameObject.transform.position, hit.point);
                return hit.collider.gameObject;
            }
        }
        return null;
    }

    // ************************************************************************************
    // SPAWNING
    // ************************************************************************************

    internal void Die()
    {
        Debug.LogWarning("I DIED");
        Registry.main.RestartGame();
    }

    void ExitLevel()
    {
        Debug.Log("exiting stage");
        hasExited = true;
        DisableMinibot();
        Registry.main.CheckIfLevelComplete();
    } 

    override internal void ResetObject()
    {
        // We drop anything that minibot is carrying
        if (objectBeingCarried != null)
            objectBeingCarried = null;

        // We cancel out all applied the forces
        theRigidBody.velocity = Vector3.zero;
        theRigidBody.angularVelocity = Vector3.zero;

        // We reset the objects position to its starting pos
        base.ResetObject();

        // We then reset the controller values to its starting values
        controller.InvertGravity = startingIsInvertedGravity;
        controller.invertHorizontal = startingIsInvertedHorizontal;

        // If object is inactive, activate it        
        EnableMinibot();

        // We make sure that it has not exited
        hasExited = false;
    }

    // Disables this current minibot
    // Hides graphic and then doesn't allow movement.
    private void DisableMinibot()
    {
        gameObject.SetActiveRecursively(false);
    }

    // Disables this current minibsot
    // Shows graphic and then allows movement.
    private void EnableMinibot()
    {        
        gameObject.SetActiveRecursively(true);
    }

    // ************************************************************************************
    // SPRITE RELATED
    // ************************************************************************************
    private void HandleSpriteDirection()
    {
        if (isFacing == Direction.Left)
            spriteManager.HandleSpriteOrientation(true);
        else
            spriteManager.HandleSpriteOrientation(false);
    }
}
