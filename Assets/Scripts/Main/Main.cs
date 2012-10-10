using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
    void Awake()
    {
        Registry.main = this;
    }

    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        } 
    }

    internal void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
