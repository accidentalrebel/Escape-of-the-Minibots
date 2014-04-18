using UnityEngine;
using System.Collections;

public class Minibot : LevelObject {

    public enum Direction { Left, Right };
    private GameObject objectBeingCarried;
    private Rigidbody theRigidBody;    

    MinibotController controller;
    private bool startingIsInvertedGravity;
    private bool startingIsInvertedHorizontal;
    public bool hasExited;
    public bool isJumping = false;

    private Direction isFacing = Direction.Right;
    private bool isStanding = true;
    private bool isWalking = false;
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
        // Handles the carrying of the object
        if (objectBeingCarried != null)
        {
            objectBeingCarried.transform.position = transform.position + Vector3.up;
        }
    }
	
    void LateUpdate()
    {
        if ( Registry.inputHandler.PickupButton
            && objectBeingCarried != null)
        {
            PutDown(objectBeingCarried);
        }

        // Handles the picking up of objects
        if (Registry.inputHandler.PickupButton
            && objectBeingCarried ==  null )
        {
            GameObject objectAtSide = GetObjectAtSide(isFacing);

            if (objectAtSide != null
                    && objectAtSide.tag == "Box")
            {
                PickUp(objectAtSide);
            }
        }
    }
    
    public void Initialize(Vector3 startPos, bool isInvertedGrav, bool isInvertedHor)
    {
        startingPos = startPos;        
        gameObject.transform.position = startingPos;

        controller = gameObject.GetComponentInChildren<MinibotController>();
        controller.IsInvertedVertically = isInvertedGrav;
        controller.IsInvertedHorizontally = isInvertedHor;
        startingIsInvertedGravity = isInvertedGrav;
        startingIsInvertedHorizontal = isInvertedHor;
    }

    private void InitializeSprite()
    {
        spriteManager.CreateAnimation("walking", new SpriteManager.AnimationProperties(new int[] { 2, 1, 3, 1 }, 0.2f));    // We create a new walkign animation        
        spriteManager.CreateAnimation("jumping", new SpriteManager.AnimationProperties(new int[] { 5, 6 }, 0.1f));    // We create a new walkign animation        
        spriteManager.CreateAnimation("standing", new SpriteManager.AnimationProperties(new int[] { 1 }, 10f));    // We create a new walkign animation        
        spriteManager.Play("standing");
    }  

	public void InvertVerticalOrientation()
	{
		controller.InvertVertically();
		spriteManager.SetFlippedY(true);
	}

    // ************************************************************************************
    // TRIGGERS
    // ************************************************************************************

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Door")
        {
            Door theDoor = col.gameObject.GetComponent<Door>();
            if (theDoor.isOpen)
            {
                theDoor.CloseDoor();
                ExitLevel();
            }
        }
    }

    // ************************************************************************************
    // ACTIONS
    // ************************************************************************************
    public void Jump()
    {
        spriteManager.Play("jumping");
        isJumping = true;
    }

    public void OnReachedGround()
    {
        spriteManager.Play("walking");
        isJumping = false;
    }

	public bool CheckIfCanStand()
	{
		if (isJumping == false && isStanding == false)
			return true;
		return false;
	}

    public void Stand()
    {
    	spriteManager.Play("standing");
    	isStanding = true;
    	isWalking = false;
    }

	public bool CheckIfCanWalk()
	{
		if (isJumping == false && isWalking == false)
			return true;
		return false;
	}

    public void Walk()
    {
		if ( CheckIfCanWalk() )
		{
		    spriteManager.Play("walking");
		    isStanding = false;
		    isWalking = true;
		}
    }

    private void PickUp(GameObject objectAtSide)
    {
        objectBeingCarried = objectAtSide;
        objectBeingCarried.GetComponent<Box>().PickUp();
    }

    private void PutDown(GameObject objectToPutDown)
    {
        Vector3 putDownPosition = Vector3.zero;
        if (isFacing == Direction.Left)
            putDownPosition = transform.position + Vector3.left;
        else if (isFacing == Direction.Right)
            putDownPosition = transform.position + Vector3.right;

        objectBeingCarried.transform.position = putDownPosition;
        objectToPutDown.GetComponent<Box>().PutDown();
        objectBeingCarried = null;
    }

    public GameObject GetObjectAtSide(Direction direction)
    {
        RaycastHit hit;
        Vector3 checkDirection;
        if (direction == Direction.Left)
            checkDirection = Vector3.left;
        else
            checkDirection = Vector3.right;

        if (Physics.Raycast(gameObject.transform.position, checkDirection, out hit, 0.5f))
        {
            if (hit.collider.tag == "Steppable"
                || hit.collider.tag == "Box")
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

    void Die()
    {
        Debug.LogWarning("I DIED");
        Registry.main.ResetLevel();
    }

    void ExitLevel()
    {
        Debug.Log("exiting stage");
        hasExited = true;
        DisableMinibot();
        Registry.main.OnMinibotExit();
    }

    override public void ResetObject()
    {
        Debug.Log("Moved minibot");
        // We drop anything that minibot is carrying
        if (objectBeingCarried != null)
            objectBeingCarried = null;

        // We cancel out all applied the forces
        theRigidBody.velocity = Vector3.zero;
        theRigidBody.angularVelocity = Vector3.zero;

        // We reset the objects position to its starting pos
        base.ResetObject();

        // We then reset the controller values to its starting values
        controller.IsInvertedVertically = startingIsInvertedGravity;
        controller.IsInvertedHorizontally = startingIsInvertedHorizontal;

        // If object is inactive, activate it        
        EnableMinibot();

        // We make sure that it has not exited
        hasExited = false;
    }

    // Disables this current minibot
    // Hides graphic and then doesn't allow movement.
    private void DisableMinibot()
    {
        gameObject.SetActive(false);
    }

    // Disables this current minibsot
    // Shows graphic and then allows movement.
    private void EnableMinibot()
    {        
        gameObject.SetActive(true);
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

    // ************************************************************************************
    // OBJECT EDITING
    // ************************************************************************************
    override public void GetEditableAttributes(LevelEditor levelEditor)
    {
        controller.IsInvertedVertically = GUI.Toggle(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 110, 110, 20), controller.IsInvertedVertically, "Invert Gravity");
        controller.IsInvertedHorizontally = GUI.Toggle(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 90, 150, 20), controller.IsInvertedHorizontally, "Invert Horizontal");
    }
}
