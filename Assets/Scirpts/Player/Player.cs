using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    internal void Die()
    {
        Debug.LogWarning("I DIED");
        Registry.main.RestartGame();
    }
}
