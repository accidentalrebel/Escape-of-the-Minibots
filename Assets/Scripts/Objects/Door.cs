using UnityEngine;
using System.Collections;

public class Door : LevelObject {

    public bool isOpen = false;
    public bool IsOpen
    {
        set { isOpen = value; UpdateDoorGraphic(); }
        get { return isOpen; }
    }

    private bool startingIsOpen = true;

    override protected void Start()
    {
        base.Start();
        UpdateDoorGraphic();
    }
	
	internal void Initialize(Vector3 theStartingPos, bool theIsOpen)
	{
		base.Initialize(theStartingPos);		
		isOpen = theIsOpen;
        startingIsOpen = isOpen;
	}

    private void UpdateDoorGraphic()
    {
        if( isOpen)
            graphicHandler.theRenderer.material.color = Color.cyan;
        else            
            graphicHandler.theRenderer.material.color = Color.green;
    }

    override internal void Use()
    {
        if (isOpen)
            isOpen = false;
        else
            isOpen = true;

        UpdateDoorGraphic();
    }

    internal override void ResetObject()
    {
        isOpen = startingIsOpen;
        UpdateDoorGraphic();
    }

    // ************************************************************************************
    // OBJECT EDITING
    // ************************************************************************************
    internal override void GetEditableAttributes()
    {
        IsOpen = GUI.Toggle(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 110, 110, 20), IsOpen, "Is Open?");
    }
}
