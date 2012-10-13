using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
    Map map;
    List<Minibot> minibotList = new List<Minibot>();

    void Awake()
    {
        Registry.main = this;
        map = Registry.map;
    }

    /// <summary>
    /// Gets all the minibots in the level
    /// And then saves it for future reference
    /// </summary>
    internal void GetMinibotsInLevel()
    {
        foreach (Transform minibot in map.minibotsContainer.transform)
        {           
            minibotList.Add(minibot.gameObject.GetComponent<Minibot>());            
        }
    }

    void Update()
    {        
        // If we press the R key, the level restarts
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        } 
    }

    /// <summary>
    /// Checks if all the minibots in the level has succesfully exited the level
    /// </summary>
    internal void CheckIfLevelComplete()
    {
        foreach (Minibot minibot in minibotList)
        {
            if (minibot.hasExited == false)
            {
                Debug.Log("LEVEL IS NOT OVER YET");
                return;
            }
        }

        Debug.Log("LEVEL IS OVER!");
    }

    /// <summary>
    /// Restarts the game
    /// </summary>
    internal void RestartGame()
    {
        map.RestartLevel();
    }
}
