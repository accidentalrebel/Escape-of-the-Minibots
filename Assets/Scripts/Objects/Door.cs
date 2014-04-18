using UnityEngine;
using System.Collections;

public class Door : LevelObject {

    public bool isOpen = false;
	public Texture openTexture;
	public Texture closedTexture;

    public bool IsOpen
    {
        set { isOpen = value; UpdateDoorGraphic(); }
        get { return isOpen; }
    }

    private bool startingIsOpen = true;

	override protected void Awake ()
	{
		base.Awake ();

		if ( openTexture == null )
			Debug.LogError("openTexture not initialized!");
		if ( closedTexture == null )
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
		isOpen = theIsOpen;
        startingIsOpen = isOpen;
	}

    private void UpdateDoorGraphic()
    {
        if( isOpen)
			graphicHandler.theRenderer.material.SetTexture("_MainTex", openTexture);
        else            
			graphicHandler.theRenderer.material.SetTexture("_MainTex", closedTexture);
    }

    override public void Use(bool setToValue)
    {
        isOpen = setToValue;
        UpdateDoorGraphic();
    }

    override public void Use()
    {
        if (isOpen)
            isOpen = false;
        else
            isOpen = true;

        UpdateDoorGraphic();
    }

    public void CloseDoor()
    {
        isOpen = false; 
        UpdateDoorGraphic();
    }

    override public void ResetObject()
    {
        isOpen = startingIsOpen;
        UpdateDoorGraphic();
    }

    // ************************************************************************************
    // OBJECT EDITING
    // ************************************************************************************
    override public void GetEditableAttributes(LevelEditor levelEditor)
    {
        IsOpen = GUI.Toggle(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 110, 110, 20), IsOpen, "Is Open?");
    }
}
