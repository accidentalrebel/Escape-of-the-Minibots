using UnityEngine;
using System.Collections;

public class ItemObject : MonoBehaviour {

    virtual internal void Use()
    {
        Debug.LogWarning("Has not been overriden!");
    }
}
