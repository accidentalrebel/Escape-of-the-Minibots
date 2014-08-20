using UnityEngine;
using System.Collections;

public class HazardTile : Tile {

    void OnTriggerEnter(Collider other) 
    {        
        if (other.tag == "Player")
        {
            other.GetComponent<Minibot>().Die();            
        }
    }
}
