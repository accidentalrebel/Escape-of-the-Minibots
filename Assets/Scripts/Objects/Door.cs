using UnityEngine;
using System.Collections;

public class Door : LevelObject {

	[SerializeField]
	private Texture _openTexture;
	[SerializeField]
	private Texture _closedTexture;

    private bool _isOpen = false;
	private bool _startingIsOpen = true;

    public bool IsOpen
    {
        get { return _isOpen; }
    }

	override protected void Awake ()
	{
		base.Awake ();

		if ( _openTexture == null )
			Debug.LogError("openTexture not initialized!");
		if ( _closedTexture == null )
			Debug.LogError("closedTexture not initialized!");
	}

    override protected void Start()
    {
        base.Start();
        UpdateDoorGraphic();
    }
	
	public void Initialize(Vector3 theStartingPos, bool theIsOpen)
	{
		base.Initialize(theStartingPos);		
		_isOpen = theIsOpen;
        _startingIsOpen = _isOpen;
	}

    private void UpdateDoorGraphic()
    {
        if( _isOpen)
			_graphicHandler.theRenderer.material.SetTexture("_MainTex", _openTexture);
        else            
			_graphicHandler.theRenderer.material.SetTexture("_MainTex", _closedTexture);

		HandleSpriteFlipping();
    }

    override public void Use(bool setToValue)
    {
		_isOpen = setToValue;
        UpdateDoorGraphic();
    }

    override public void Use()
    {
        if (_isOpen)
            _isOpen = false;
        else
            _isOpen = true;

        UpdateDoorGraphic();
    }

    public void CloseDoor()
    {
        _isOpen = false; 
        UpdateDoorGraphic();
    }

    override public void ResetObject()
    {
        _isOpen = _startingIsOpen;
        UpdateDoorGraphic();
    }

    // ************************************************************************************
    // OBJECT EDITING
    // ************************************************************************************
    override public void GetEditableAttributes(LevelEditor levelEditor)
    {
        _isOpen = GUI.Toggle(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 110, 110, 20), IsOpen, "Is Open?");
		_startingIsOpen = IsOpen;
		UpdateDoorGraphic();
    }
}
