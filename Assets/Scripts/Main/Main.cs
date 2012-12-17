using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
    public delegate void EventHandler();
    public event EventHandler ELevelCompleted;
    public event EventHandler ELevelStarted;
    public event EventHandler EUpdateMinibotCount;

    Map map;
    internal Timer timer;
    internal LevelEditor levelEditor;
    internal bool levelActive = false;
    Settings settings;
    List<Minibot> minibotList = new List<Minibot>();
    public string mapToLoad = "1";

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
        if ( settings != null )
            map.levelReader.LoadLevel(settings.InitialLevelToLoad.ToString());
        else
            map.levelReader.LoadLevel(mapToLoad);
    }

    void Update()
    {        
        // If we press the R key, the level restarts
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            LevelCompleted();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            if ( !levelEditor.enabled )
                levelEditor.enabled = true;
            else
                levelEditor.enabled = false;
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

    internal void StartLevel()
    {
        EUpdateMinibotCount();
        ELevelStarted();

        levelActive = true;
        Registry.replayManager.StartRecording();
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
        levelActive = false;
        
        // Just restart the game automatically if in mapEditMode
        if (levelEditor != null
            && levelEditor.MapEditMode == true)
            RestartLevel();
        else
            ELevelCompleted();
    }

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
    internal void RestartLevel()
    {
        map.RestartLevel();
        StartLevel();
    }
}
