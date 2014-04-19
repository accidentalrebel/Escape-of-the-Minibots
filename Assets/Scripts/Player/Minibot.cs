using UnityEngine;
using System.Collections;

public class Minibot : LevelObject {

	public enum Direction { Left, Right };
	public bool HasExited {
		get {
			return _hasExited;
		}
	}

	public bool IsJumping {
		get {
			return _isJumping;
		}
	}

	[SerializeField]
	private float normalRayLength = 0.5f;

	[SerializeField]
	private float dropRayLength = 1.5f;

	[SerializeField]
	private bool _initVerticalOrientation;
	
	[SerializeField]
	private bool _initHorizontalOrientation;
	   
    private GameObject 			_objectBeingCarried;
    private Rigidbody 			_theRigidBody;    
    private MinibotController 	_controller;
    
	private bool _hasExited;
	private bool _isJumping = false;
    private bool _isStanding = true;
    private bool _isWalking = false;
	private Direction _isFacing = Direction.Right;

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
            && _objectBeingCarried != null
		    && !_isJumping)
        {
            PutDownCarriedObject(_objectBeingCarried);
        }

        // Handles the picking up of objects
        if (Registry.inputHandler.PickupButton
            && _objectBeingCarried ==  null
		    && !_isJumping)
        {
			GameObject objectAtSide = GetObjectAtSide(_isFacing, normalRayLength);

            if (objectAtSide != null
                    && objectAtSide.tag == "Box")
            {
                PickUpObject(objectAtSide);
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
        _spriteManager.CreateAnimation("walking", new SpriteManager.AnimationProperties(new int[] { 2, 1, 3, 1 }, 0.2f));    // We create a new walkign animation        
        _spriteManager.CreateAnimation("jumping", new SpriteManager.AnimationProperties(new int[] { 5, 6 }, 0.1f));    // We create a new walkign animation        
        _spriteManager.CreateAnimation("standing", new SpriteManager.AnimationProperties(new int[] { 1 }, 10f));    // We create a new walkign animation        
        _spriteManager.Play("standing");
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
	// ORIENTATION AND STATUS
	// ************************************************************************************
	public void InvertVerticalOrientation()
	{
		_controller.InvertVertically();
		_spriteManager.SetFlippedY(_controller.IsInvertedVertically);
	}    

	private void DisableMinibot()
	{
		gameObject.SetActive(false);
	}

	private void EnableMinibot()
	{        
		gameObject.SetActive(true);
	}

    // ************************************************************************************
    // ACTIONS
    // ************************************************************************************
    public void Jump()
    {
        _spriteManager.Play("jumping");
        _isJumping = true;
    }

    public void OnReachedGround()
    {
        _spriteManager.Play("walking");
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
    	_spriteManager.Play("standing");
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
		    _spriteManager.Play("walking");
		    _isStanding = false;
		    _isWalking = true;
		}
    }

    public void PickUpObject(GameObject objectAtSide)
    {
        _objectBeingCarried = objectAtSide;
        _objectBeingCarried.GetComponent<Box>().PickUp();
    }

	public void PutDownCarriedObject(GameObject objectToPutDown)
    {     
		if ( GetObjectAtSide(_isFacing, dropRayLength) == null )
		{
			Vector3 putDownPosition;
			if (_isFacing == Direction.Left)
				putDownPosition = transform.position + Vector3.left;
			else		
				putDownPosition = transform.position + Vector3.right;

	        _objectBeingCarried.transform.position = putDownPosition;
	        objectToPutDown.GetComponent<Box>().PutDown();
	        _objectBeingCarried = null;
		}
    }

	public GameObject GetObjectAtSide(Direction direction)
	{
		return GetObjectAtSide(direction, normalRayLength);
	}

    public GameObject GetObjectAtSide(Direction direction, float rayLength)
    {        
        Vector3 checkDirection;
        if (direction == Direction.Left)
            checkDirection = Vector3.left;
        else
            checkDirection = Vector3.right;

		Vector3 feetOffset;
		if ( !_controller.IsInvertedVertically )
			feetOffset = Vector3.up;
		else
			feetOffset = Vector3.down;

		Vector3 feetRaycastPosition = _spriteManager.gameObject.transform.position - feetOffset / 2.5f;
		GameObject collidedGameObject = GetCollisionFromPosition(feetRaycastPosition, checkDirection, rayLength);
		if ( collidedGameObject != null )
			return collidedGameObject;

		Vector3 headOffset;
		if ( !_controller.IsInvertedVertically )
			headOffset = Vector3.down;
		else
			headOffset = Vector3.up;

		Vector3 headRaycastPosition = _spriteManager.gameObject.transform.position - headOffset / 2.5f;
		collidedGameObject = GetCollisionFromPosition(headRaycastPosition, checkDirection, rayLength);
		if ( collidedGameObject != null )
			return collidedGameObject;

        return null;
    }

	GameObject GetCollisionFromPosition (Vector3 startingPosition, Vector3 checkDirection, float rayLength)
	{
		RaycastHit hit;
		if ( Physics.Raycast(startingPosition, checkDirection, out hit, rayLength))
		{
			if (hit.collider.tag == "Steppable"
			    || hit.collider.tag == "Box")
			{
#if UNITY_EDITOR
				Debug.DrawLine(startingPosition, startingPosition + checkDirection, new Color(255, 0, 255));
#endif
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
		DropCarriedObject();
		CancelOutAllAppliedForces();

        base.ResetObject();

		_controller.Reset(_initHorizontalOrientation, _initVerticalOrientation);

		_spriteManager.Reset();     
        EnableMinibot();
       
		_hasExited = false;
    }

	void DropCarriedObject ()
	{
		if (_objectBeingCarried != null)
			_objectBeingCarried = null;
	}

	void CancelOutAllAppliedForces ()
	{
		_theRigidBody.velocity = Vector3.zero;
		_theRigidBody.angularVelocity = Vector3.zero;
	}

    // ************************************************************************************
    // SPRITE RELATED
    // ************************************************************************************
    private void HandleSpriteDirection()
    {
        if (_isFacing == Direction.Left)
            _spriteManager.SetFlippedX(true);
        else
            _spriteManager.SetFlippedX(false);
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
