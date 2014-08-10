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

	[SerializeField]
    string _mapToLoad = "1";

    public string currentUser = "User";
    
    public Timer timer;
    public LevelEditor levelEditor;

    public bool isReplayMode = false;
	public int minibotCountAtStart;

    Map map;
    public Settings settings;
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
            Debug.LogWarning("Timer not found!");

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
        if (settings != null) {
            _mapToLoad = settings.InitialLevelToLoad.ToString();
            currentUser = settings.currentUser;
        }

        if ( !Registry.replayViewer.enabled )
			map.levelReader.LoadLevel(_mapToLoad);
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
    public void GoToNextLevel()
    {
        map.GetNextLevel();
    }

	public void LoadNextLevel(string nextLevelName)
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

	public void StartLevel()
    {        
		if ( !Registry.replayViewer.enabled ) {
	        Registry.replayManager.StopReplay();
	        Registry.replayManager.StartRecording();
		}

        isReplayMode = false;

		if ( EUpdateMinibotCount != null &&EUpdateMinibotCount.GetInvocationList().Length > 0 )
			EUpdateMinibotCount();                          // We update the Minibot Count
		
		if ( ELevelStarted != null &&ELevelStarted.GetInvocationList().Length > 0 )
			ELevelStarted();
    }

    public void RestartLevel()
    {
        map.RestartLevel();
        StartLevel();
    }

	public void ResetLevel()
    {
        map.RestartLevel();
       
		if ( EUpdateMinibotCount != null &&EUpdateMinibotCount.GetInvocationList().Length > 0 )
			EUpdateMinibotCount();                          // We update the Minibot Count
		
		if ( ELevelStarted != null &&ELevelStarted.GetInvocationList().Length > 0 )
			ELevelStarted();
    }

    // ************************************************************************************
    // END GAME LOGIC
    // ************************************************************************************

	public void OnMinibotExit()
    {
		if ( EUpdateMinibotCount != null && EUpdateMinibotCount.GetInvocationList().Length > 0 )
			EUpdateMinibotCount();
       
		int minibotsLeft = CountMinibotsInLevel();

        if (minibotsLeft <= 0)
            LevelCompleted();
    }

    private void LevelCompleted()
    {
        // Just restart the game automatically if in mapEditMode
        if (levelEditor != null
            && levelEditor.MapEditMode == true)
            RestartLevel();
        else
        {
			if ( ELevelCompleted != null && ELevelCompleted.GetInvocationList().Length > 0 )
           	 	ELevelCompleted();
        }
    }

    // ************************************************************************************
    // REPLAY
    // ************************************************************************************
    public void StartReplay()
    {
        isReplayMode = true;
		map.RestartLevel();
		Registry.replayManager.StartReplay();
    }
    
    // ************************************************************************************
    // HELPER FUNCTIONS
    // ************************************************************************************
    public int CountMinibotsInLevel()
    {
        int count = 0;
        foreach (Minibot minibot in minibotList)
        {
            if (minibot != null && minibot.HasExited == false)
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
    public void GetMinibotsInLevel()
    {
        minibotList.Clear();
        foreach (Transform minibot in map.minibotsContainer.transform)
        {
            minibotList.Add(minibot.gameObject.GetComponent<Minibot>());
        }
    }
}
