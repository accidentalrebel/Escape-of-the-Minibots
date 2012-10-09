using UnityEngine;
using System.Collections;

public class Door : LevelObject {

    public bool isOpen = false;

    void Start()
    {
        base.Start();

        UpdateDoorGraphic();
    }

    private void UpdateDoorGraphic()
    {
        if( isOpen)
            theTile.theRenderer.material.color = Color.cyan;
        else            
            theTile.theRenderer.material.color = Color.green;
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
