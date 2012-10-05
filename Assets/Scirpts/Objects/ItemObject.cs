using UnityEngine;
using System.Collections;

public class ItemObject : MonoBehaviour {

    protected Tile theTile;

    protected void Start()
    {
        theTile = gameObject.GetComponent<Tile>();
        if (theTile == null)
            Debug.LogError("theTile can not be found!");
    }

    virtual internal void Use()
    {
        Debug.LogWarning("Has not been overriden!");
    }
}
