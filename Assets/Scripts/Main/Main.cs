using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
    Map map;

    void Awake()
    {
        Registry.main = this;
        map = Registry.map;
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
        //Application.LoadLevel(Application.loadedLevel);
        map.RestartLevel();
    }
}
