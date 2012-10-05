using UnityEngine;
using System.Collections;

public class Door : ItemObject {

    override internal void Use()
    {
        Debug.Log("Door has been opened");
    }
}
