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
            Application.LoadLevel(0);
        } 
    }

    void RestartGame()
    {
        Application.LoadLevel(0);
    }
}
