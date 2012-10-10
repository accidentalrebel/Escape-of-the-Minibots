using UnityEngine;
using System.Collections;

public class Door : LevelObject {

    public bool isOpen = false;

    void Start()
    {
        base.Start();

        UpdateDoorGraphic();
    }
	
	internal void Initialize(Vector3 theStartingPos, bool theIsOpen)
	{
		base.Initialize(theStartingPos);		
		isOpen = theIsOpen;
	}

    private void UpdateDoorGraphic()
    {
        if( isOpen)
            theGraphicHandler.theRenderer.material.color = Color.cyan;
        else            
            theGraphicHandler.theRenderer.material.color = Color.green;
    }

    override internal void Use()
    {
        if (isOpen)
            isOpen = false;
        else
            isOpen = true;

        UpdateDoorGraphic();
    }
}
