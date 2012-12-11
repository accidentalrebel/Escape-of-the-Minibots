using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
    public delegate void EventHandler();
    public event EventHandler ELevelCompleted;
    public event EventHandler EUpdateMinibotCount;

    Map map;
    internal Timer timer;
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
        timer = GetComponent<Timer>();
        if (timer == null)
            Debug.LogError("Timer not found!");

        // Level editor is optional
        levelEditor = gameObject.GetComponent<LevelEditor>();
        if (levelEditor == null)
            Debug.LogWarning("Could not find level editor.");
    }

    void Start()
    {
        
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
            LevelCompleted();
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
    internal void OnMinibotExit()
    {
        EUpdateMinibotCount();   
        int minibotsLeft = CountMinibotsInLevel();

        if ( minibotsLeft <= 0 )
            LevelCompleted();
    }

    /// <summary>
    /// This method handles what would happen when the level is over
    /// </summary>
    private void LevelCompleted()
    {
        Debug.Log("LEVEL IS OVER!");
        ELevelCompleted();

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

    internal void FinishedLevelLoading()
    {
        minibotCountAtStart = CountMinibotsInLevel();
        EUpdateMinibotCount();        
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
