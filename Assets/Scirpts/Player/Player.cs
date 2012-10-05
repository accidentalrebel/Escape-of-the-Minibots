using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    internal void Die()
    {
        Debug.LogWarning("I DIED");
        Registry.main.RestartGame();
    }

    void ExitStage()
    {
        Debug.Log("exiting stage");
        gameObject.SetActiveRecursively(false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Door")
        {
            if ( col.gameObject.GetComponent<Door>().isOpen )
                ExitStage();
        }
    }
}
