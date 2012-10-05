using UnityEngine;
using System.Collections;

public class Door : ItemObject {

    internal bool isOpen = false;

    override internal void Use()
    {
        if (isOpen)
        {
            theTile.theRenderer.material.color = Color.green;
            isOpen = false;
        }
        else
        {
            theTile.theRenderer.material.color = Color.cyan;
            isOpen = true;
        }        
    }
}
