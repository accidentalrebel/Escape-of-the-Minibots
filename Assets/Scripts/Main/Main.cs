using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
    Map map;
    internal LevelEditor levelEditor;
    List<Minibot> minibotList = new List<Minibot>();

    int minibotCountAtStart;

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

    void Start()
    {
        minibotCountAtStart = CountMinibotsInLevel();        
        Registry.eventDispatcher.OnUpdateMinibotCount(minibotCountAtStart.ToString());
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
        minibotList.Clear();
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
        if ( CountMinibotsInLevel() <= 0 )
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
        map.GetNextLevel();
        //string nextLevelName = "";
        //int currentLevel;

        //int.TryParse(map.currentLevel, out currentLevel);    // We try to parse the loadedLevel name to int
        //nextLevelName = (currentLevel+=1).ToString();       // We increment the level number and set it as the next level

        //if (map.levelReader.CheckIfFileExists(nextLevelName))
        //{
        //    LoadNextLevel(nextLevelName);
        //}
        //else
        //{
        //    Debug.Log("no more next level");
        //}
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

        Debug.Log("LOADING LEVEL NUMBER " + nextLevelName);

        if (map.levelReader.CheckIfFileExists(nextLevelName))
        {
            map.ClearLevel();
            map.levelReader.LoadLevel(nextLevelName);
        }
    }

    // ************************************************************************************
    // HELPER FUNCTIONS
    // ************************************************************************************
    internal int CountMinibotsInLevel()
    {
        int count = 0;
        foreach (Minibot minibot in minibotList)
        {
            if (minibot != null && minibot.hasExited == false)
            {
                count++;
            }
        }

        return count;
    }

    /// <summary>
    /// Restarts the game
    /// </summary>
    internal void RestartGame()
    {
        map.RestartLevel();
    }
}
