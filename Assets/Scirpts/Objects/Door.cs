using UnityEngine;
using System.Collections;

public class Door : ItemObject {

    bool isOpened = false;

    override internal void Use()
    {
        if (isOpened)
        {
            theTile.theRenderer.material.color = Color.green;
            isOpened = false;
        }
        else
        {
            theTile.theRenderer.material.color = Color.cyan;
            isOpened = true;
        }        
    }
}
