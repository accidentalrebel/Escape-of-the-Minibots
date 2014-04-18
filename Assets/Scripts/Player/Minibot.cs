using UnityEngine;
using System.Collections;

public class Minibot : LevelObject {

    public enum Direction { Left, Right };
    private GameObject 			_objectBeingCarried;
    private Rigidbody 			_theRigidBody;    
    private MinibotController 	_controller;

	[SerializeField]
    private bool _initVerticalOrientation;
    
	[SerializeField]
	private bool _initHorizontalOrientation;
    
	private bool _hasExited;
	public bool HasExited {
		get {
			return _hasExited;
		}
	}

    private bool _isJumping = false;
	public bool IsJumping {
		get {
			return _isJumping;
		}
	}

    private Direction _isFacing = Direction.Right;
    private bool _isStanding = true;
    private bool _isWalking = false;
    public Direction IsFacing
    {
        set {
            // We only set the value if there is achange in direction
            if (_isFacing != value)
            {                   
                _isFacing = value;  
				HandleSpriteDirection();
            }
        }
    }

    // ************************************************************************************
    // MAIN
    // ************************************************************************************
	override protected void Start()
	{
        _theRigidBody = GetComponent<Rigidbody>();
        if (_theRigidBody == null)
            Debug.LogError("theRigidBody is not found!");

        base.Start();

        InitializeSprite();
	}

    void Update()
    {
        // Handles the carrying of the object
        if (_objectBeingCarried != null)
        {
            _objectBeingCarried.transform.position = transform.position + Vector3.up;
        }
    }
	
    void LateUpdate()
    {
        if ( Registry.inputHandler.PickupButton
            && _objectBeingCarried != null)
        {
            PutDown(_objectBeingCarried);
        }

        // Handles the picking up of objects
        if (Registry.inputHandler.PickupButton
            && _objectBeingCarried ==  null )
        {
            GameObject objectAtSide = GetObjectAtSide(_isFacing);

            if (objectAtSide != null
                    && objectAtSide.tag == "Box")
            {
                PickUp(objectAtSide);
            }
        }
    }
    
	// ************************************************************************************
	// INITIALIZATION
	// ************************************************************************************
    public void Initialize(Vector3 startPos, bool isInvertedGrav, bool isInvertedHor)
    {
        startingPos = startPos;        
        gameObject.transform.position = startingPos;

        _controller = gameObject.GetComponentInChildren<MinibotController>();
        _controller.IsInvertedVertically = isInvertedGrav;
        _controller.IsInvertedHorizontally = isInvertedHor;
        _initVerticalOrientation = isInvertedGrav;
        _initHorizontalOrientation = isInvertedHor;
    }

    private void InitializeSprite()
    {
        spriteManager.CreateAnimation("walking", new SpriteManager.AnimationProperties(new int[] { 2, 1, 3, 1 }, 0.2f));    // We create a new walkign animation        
        spriteManager.CreateAnimation("jumping", new SpriteManager.AnimationProperties(new int[] { 5, 6 }, 0.1f));    // We create a new walkign animation        
        spriteManager.CreateAnimation("standing", new SpriteManager.AnimationProperties(new int[] { 1 }, 10f));    // We create a new walkign animation        
        spriteManager.Play("standing");
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
	// ORIENTATION
	// ************************************************************************************
	public void InvertVerticalOrientation()
	{
		_controller.InvertVertically();
		spriteManager.SetFlippedY(_controller.IsInvertedVertically);
	}    

    // ************************************************************************************
    // ACTIONS
    // ************************************************************************************
    public void Jump()
    {
        spriteManager.Play("jumping");
        _isJumping = true;
    }

    public void OnReachedGround()
    {
        spriteManager.Play("walking");
        _isJumping = false;
    }

	public bool CheckIfCanStand()
	{
		if (_isJumping == false && _isStanding == false)
			return true;
		return false;
	}

    public void Stand()
    {
    	spriteManager.Play("standing");
    	_isStanding = true;
    	_isWalking = false;
    }

	public bool CheckIfCanWalk()
	{
		if (_isJumping == false && _isWalking == false)
			return true;
		return false;
	}

    public void Walk()
    {
		if ( CheckIfCanWalk() )
		{
		    spriteManager.Play("walking");
		    _isStanding = false;
		    _isWalking = true;
		}
    }

    public void PickUp(GameObject objectAtSide)
    {
        _objectBeingCarried = objectAtSide;
        _objectBeingCarried.GetComponent<Box>().PickUp();
    }

	public void PutDown(GameObject objectToPutDown)
    {
        Vector3 putDownPosition = Vector3.zero;
        if (_isFacing == Direction.Left)
            putDownPosition = transform.position + Vector3.left;
        else if (_isFacing == Direction.Right)
            putDownPosition = transform.position + Vector3.right;

        _objectBeingCarried.transform.position = putDownPosition;
        objectToPutDown.GetComponent<Box>().PutDown();
        _objectBeingCarried = null;
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

    public void Die()
    {
        Debug.LogWarning("I DIED");
        Registry.main.ResetLevel();
    }

	public void ExitLevel()
    {
        Debug.Log("exiting stage");
        _hasExited = true;
        DisableMinibot();
        Registry.main.OnMinibotExit();
    }

    override public void ResetObject()
    {       
		// We drop anything that minibot is carrying
        if (_objectBeingCarried != null)
            _objectBeingCarried = null;

        // We cancel out all applied the forces
        _theRigidBody.velocity = Vector3.zero;
        _theRigidBody.angularVelocity = Vector3.zero;

        base.ResetObject();
		_controller.Reset(_initHorizontalOrientation, _initVerticalOrientation);
		spriteManager.Reset();
     
        EnableMinibot();
        _hasExited = false;
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
        if (_isFacing == Direction.Left)
            spriteManager.HandleSpriteOrientation(true);
        else
            spriteManager.HandleSpriteOrientation(false);
    }

	public void setFacingValueWithXinput (float xInput)
	{
		if (xInput > 0) {
			IsFacing = Minibot.Direction.Right;
		}
		else if (xInput < 0) {
			IsFacing = Minibot.Direction.Left;
		}
	}
	
	public void setPlayerAnimationsWithXInput (float xInput)
	{
		if ( _isJumping )
			return;
		
		if (xInput != 0 )
			Walk();
		else
			Stand();
	}

    // ************************************************************************************
    // OBJECT EDITING
    // ************************************************************************************
    override public void GetEditableAttributes(LevelEditor levelEditor)
    {
        _controller.IsInvertedVertically = GUI.Toggle(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 110, 110, 20), _controller.IsInvertedVertically, "Invert Gravity");
        _controller.IsInvertedHorizontally = GUI.Toggle(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 90, 150, 20), _controller.IsInvertedHorizontally, "Invert Horizontal");
    }
}
