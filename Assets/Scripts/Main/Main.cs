using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
    Map map;
    LevelEditor levelEditor;
    List<Minibot> minibotList = new List<Minibot>();

    // ************************************************************************************
    // MAIN
    // ************************************************************************************

    void Awake()
    {
        Registry.main = this;
        map = Registry.map;

        // Level editor is optional
        levelEditor = gameObject.GetComponent<LevelEditor>();
        if (levelEditor == null)
            Debug.LogWarning("Could not find level editor.");
    }

    void Update()
    {        
        // If we press the R key, the level restarts
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            LevelOver();
        } 
    }

    // ************************************************************************************
    // INITIALIZATION
    // ************************************************************************************

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

    // ************************************************************************************
    // END GAME LOGIC
    // ************************************************************************************

    /// <summary>
    /// Checks if all the minibots in the level has succesfully exited the level
    /// </summary>
    internal void CheckIfLevelComplete()
    {
        foreach (Minibot minibot in minibotList)
        {
            if (minibot.hasExited == false)
            {
                return;
            }
        }
       
        LevelOver();
    }

    /// <summary>
    /// This method handles what would happen when the level is over
    /// </summary>
    private void LevelOver()
    {
        Debug.Log("LEVEL IS OVER!");

        // Just restart the game if in mapEditMode
        if ( levelEditor != null 
            && levelEditor.MapEditMode == true)
            RestartGame();
        else // Else go to next level
            GetNextLevel();
    }

    /// <summary>
    /// Gets the name of the level that would go next after this level
    /// </summary>
    private void GetNextLevel()
    {
        string nextLevelName = "";
        int currentLevel;

        int.TryParse(map.currentLevel, out currentLevel);    // We try to parse the loadedLevel name to int
        nextLevelName = (currentLevel+=1).ToString();       // We increment the level number and set it as the next level

        LoadNextLevel(nextLevelName);        
    }

    /// <summary>
    /// Loads the next level
    /// </summary>
    /// <param name="nextLevelName"></param>
    private void LoadNextLevel(string nextLevelName)
    {        
        if (nextLevelName == "")
        {
            Debug.LogWarning("Could not get the next level");
            return;
        }

        map.ClearLevel();
        map.levelReader.LoadLevel(nextLevelName);        
    }

    // ************************************************************************************
    // HELPER FUNCTIONS
    // ************************************************************************************

    /// <summary>
    /// Restarts the game
    /// </summary>
    internal void RestartGame()
    {
        map.RestartLevel();
    }
}
