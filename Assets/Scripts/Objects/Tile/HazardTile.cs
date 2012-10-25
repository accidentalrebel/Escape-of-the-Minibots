using UnityEngine;
using System.Collections;

public class HazardTile : Tile {

    DynamicSizeObject dynamicSizeComponent;

    void Start()
    {
        dynamicSizeComponent = gameObject.GetComponent<DynamicSizeObject>();
        if (dynamicSizeComponent == null)
        {
            Debug.LogError("dynamicSizeComponent not specified");
            return;
        }

        dynamicSizeComponent.Initialize(Registry.prefabHandler.pfHazard);
    }

    void OnTriggerEnter(Collider other) 
    {        
        if (other.tag == "Player")
        {
            other.GetComponent<Minibot>().Die();            
        }
    }
}
