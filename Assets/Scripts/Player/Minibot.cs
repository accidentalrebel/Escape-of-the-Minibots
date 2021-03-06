using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GravityHandler))]
public class Minibot : LevelObject {

	const float MINIBOT_TRANSFORM_SCALE_Y = 1f;

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
	public bool InitVerticalOrientation {
		get {
			return _initVerticalOrientation;
		}
	}
	
	[SerializeField]
	private bool _initHorizontalOrientation;
	public bool InitHorizontalOrientation {
		get {
			return _initHorizontalOrientation;
		}
	}
	   
    private GameObject 			_objectBeingCarried;
    private Rigidbody 			_theRigidBody;    
    private MinibotController 	_controller;
	private GravityHandler 		_gravityHandler;

	private bool _hasExited;
	private bool _isDead;
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

	protected override void Awake ()
	{
		base.Awake ();

		_gravityHandler = gameObject.GetComponent<GravityHandler>();
		_gravityHandler.OnGravityChanged += OnGravitySwitched;

		_controller = gameObject.GetComponentInChildren<MinibotController>();
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
		HandleCarriedObject();
    }
	
    void LateUpdate()
    {
        if ( Registry.inputHandler.pickupButton
            && _objectBeingCarried != null
		    && !_isJumping)
        {
            PutDownCarriedObject();
        }

        // Handles the picking up of objects
        if (Registry.inputHandler.pickupButton
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
		       
		_gravityHandler.IsInverted = isInvertedGrav;
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
			if (theDoor.IsOpen)
			{
				theDoor.CloseDoor();
				ExitLevel();
			}
		}
	}

	void OnGravitySwitched()
	{
		Vector3 currentScale = gameObject.transform.localScale;
		float scaleYToUse = MINIBOT_TRANSFORM_SCALE_Y;
		if ( _gravityHandler.IsInverted )
			scaleYToUse = -MINIBOT_TRANSFORM_SCALE_Y;

		gameObject.transform.localScale = new Vector3(currentScale.x, scaleYToUse, currentScale.z);
	}

	// ************************************************************************************
	// ORIENTATION AND STATUS
	// ************************************************************************************

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
		if ( _isJumping )
			return;

        _spriteManager.Play("jumping");
        _isJumping = true;

		Registry.sfxManager.PlaySFX(Registry.sfxManager.SFXJump);
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
		if ( !_gravityHandler.IsInverted )
			feetOffset = Vector3.up;
		else
			feetOffset = Vector3.down;

		Vector3 feetRaycastPosition = _spriteManager.gameObject.transform.position - feetOffset / 2.5f;
		GameObject collidedGameObject = GetCollisionFromPosition(feetRaycastPosition, checkDirection, rayLength);
		if ( collidedGameObject != null )
			return collidedGameObject;

		Vector3 headOffset;
		if ( !_gravityHandler.IsInverted )
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
	// OBJECT CARRYING
	// ************************************************************************************
	private void HandleCarriedObject ()	{
		if (_objectBeingCarried != null) {
			if ( _gravityHandler.IsInverted )
				_objectBeingCarried.transform.position = transform.position + Vector3.down * 1.25f;
			else
				_objectBeingCarried.transform.position = transform.position + Vector3.up ;
		}
	}
	
	public void PickUpObject(GameObject objectAtSide) {
		_objectBeingCarried = objectAtSide;
	}
	
	public void PutDownCarriedObject()
	{     
		if ( _objectBeingCarried == null )
			return;
		
		if ( GetObjectAtSide(_isFacing, dropRayLength) == null )
		{
			Vector3 putDownPosition;
			if (_isFacing == Direction.Left)
				putDownPosition = transform.position + Vector3.left;
			else		
				putDownPosition = transform.position + Vector3.right;
			
			_objectBeingCarried.transform.position = putDownPosition;
			_objectBeingCarried = null;

			Registry.sfxManager.PlaySFX(Registry.sfxManager.SFXBoxSlam);
		}
	}

    // ************************************************************************************
    // SPAWNING
    // ************************************************************************************

    public void Die()
    {
		if ( _isDead )
			return;

		_isDead = true;	
		Registry.sfxManager.PlaySFX(Registry.sfxManager.SFXHazardShock);

		PutDownCarriedObject();
		Registry.main.ResetLevel();
    }

	public void ExitLevel()
    {
        _hasExited = true;
		PutDownCarriedObject();
        DisableMinibot();

        Registry.main.OnMinibotExit();
		Registry.sfxManager.PlaySFX(Registry.sfxManager.SFXDoorExit);
    }

    override public void ResetObject()
    {       
		if (_objectBeingCarried != null)
			_objectBeingCarried = null;

		CancelOutAllAppliedForces();

        base.ResetObject();

		_controller.Reset(_initHorizontalOrientation, _initVerticalOrientation);
		_gravityHandler.Reset(_initVerticalOrientation);

		EnableMinibot();    
		_hasExited = false;
		_isDead = false;
    }

	void CancelOutAllAppliedForces ()
	{
		if ( _theRigidBody == null )
			return;

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

	public void SetFacingValueWithXinput (float xInput)
	{
		if (xInput > 0) {
			IsFacing = Minibot.Direction.Right;
		}
		else if (xInput < 0) {
			IsFacing = Minibot.Direction.Left;
		}
	}
	
	public void SetPlayerAnimationsWithXInput (float xInput)
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
		Rect guiRect = new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 110, 110, 20);
		_initVerticalOrientation = _gravityHandler.IsInverted = GUI.Toggle(guiRect, _gravityHandler.IsInverted, "Invert Gravity");

		guiRect = new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 90, 150, 20);
		_initHorizontalOrientation = _controller.IsInvertedHorizontally = GUI.Toggle(guiRect, _controller.IsInvertedHorizontally, "Invert Horizontal");		 
	}
}
