using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
    public delegate void EventHandler();
    public event EventHandler ELevelCompleted;
    public event EventHandler ELevelStarted;
    public event EventHandler EUpdateMinibotCount;

    public string engineVersion = "Alpha 5.0";
    public string mapPackVersion = "Alpha 3.0";

    public string mapToLoad = "1";
    
    internal Timer timer;
    internal LevelEditor levelEditor;

    internal bool isReplayMode = false;
    int minibotCountAtStart;

    Map map;
    Settings settings;
    List<Minibot> minibotList = new List<Minibot>();

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

        GameObject settingsGO = GameObject.Find("Settings");
        if (settingsGO != null)
        {
            settings = settingsGO.GetComponent<Settings>();
            if ( settings == null)
                Debug.LogWarning("Settings not found!");
        }

        // Level editor is optional
        levelEditor = gameObject.GetComponent<LevelEditor>();
        if (levelEditor == null)
            Debug.LogWarning("Could not find level editor.");
    }

    void Start()
    {
        // If we have a settings file ( If we booted the game from the Main Menu Scene )
        if ( settings != null )
            map.levelReader.LoadLevel(settings.InitialLevelToLoad.ToString());

        // IF we don't ( If we booted the game from the Main Game Scene )
        else
            map.levelReader.LoadLevel(mapToLoad);
    }

    void LateUpdate()
    {      
        // The following are debug keys
        // If we press the R key, the level restarts
        if ( Registry.inputHandler.ResetButton )
        {
            RestartLevel(); 
        }
        // Automatically finish the level
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!isReplayMode)
                LevelCompleted();   
        }
        // Show/Hide the level editor
        else if (Input.GetKeyDown(KeyCode.L))
        {
            if (!levelEditor.enabled)
            {
                levelEditor.enabled = true;
            }
            else
            {
                levelEditor.enabled = false;
            }
        }
    }

    // ************************************************************************************
    // LEVEL CHANGING
    // ************************************************************************************
    /// <summary>
    /// Gets the name of the level that would go next after this level
    /// </summary>
    internal void GoToNextLevel()
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

    /// <summary>
    /// Starts a new level
    /// </summary>
    internal void StartLevel()
    {
        Registry.playtestManager.SendPlaytestData(Registry.main.timer.CurrentTime);    
        Registry.replayManager.StopReplay();
        Registry.replayManager.StartRecording();

        isReplayMode = false;

        EUpdateMinibotCount();                          // We update the Minibot Count
        ELevelStarted();
    }

    /// <summary>
    /// Restarts the game
    /// </summary>
    internal void RestartLevel()
    {
        map.RestartLevel();
        StartLevel();
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

        if (minibotsLeft <= 0)
            LevelCompleted();
    }

    /// <summary>
    /// This method handles what would happen when the level is over
    /// </summary>
    private void LevelCompleted()
    {
        // Just restart the game automatically if in mapEditMode
        if (levelEditor != null
            && levelEditor.MapEditMode == true)
            RestartLevel();
        else
            ELevelCompleted();
    }

    // ************************************************************************************
    // REPLAY
    // ************************************************************************************
    internal void StartReplay()
    {
        isReplayMode = true;
        Registry.replayManager.StartReplay();
        map.RestartLevel();
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
}
