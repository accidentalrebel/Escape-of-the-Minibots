using UnityEngine;
using System.Collections;

public class MenuLevelRecap : Menu {

    string levelComment = "";

	// Use this for initialization
	override protected void Start () {
        base.Start();  

		if ( Registry.replayViewer.enabled == false )
			Registry.main.ELevelCompleted += LevelCompleted;
	}

    override protected void Update()
    {
        base.Update();

        if (isVisible) {
            if (Input.GetKeyDown(KeyCode.Z)
			    || Input.GetKeyDown(KeyCode.Space)) {                
                GoToNextLevel();
            }
            else if (Input.GetKeyDown(KeyCode.X)) {
                RestartLevel();
            }
        }
    }

    void LevelCompleted()
    {
		Show();
		Registry.main.StartReplay();
    }

    private void RestartLevel()
    {
        Registry.playtestManager.SendPlaytestData(Registry.main.currentUser
                , Registry.main.timer.CurrentTime, Registry.main.engineVersion
                , Registry.main.mapPackVersion, levelComment);
        Registry.main.RestartLevel();
        Hide();
    }

    private void GoToNextLevel()
    {        
		if ( Registry.replayViewer.enabled )
			Registry.replayViewer.StartNextReplay();
		else {
	        Registry.playtestManager.SendPlaytestData(Registry.main.currentUser
	                , Registry.main.timer.CurrentTime, Registry.main.engineVersion
	                , Registry.main.mapPackVersion, levelComment);
	        levelComment = "";
	        Registry.main.GoToNextLevel();        
		}

		Hide();
    }

    void OnGUI()
    {
        if (isVisible)
        {
            float centerPosition = (Screen.width / 2);
            float topPosition = (Screen.height / 2) - (menuHeight / 2);
            GUI.Box(new Rect
                ( centerPosition - (menuWidth / 2)
                , topPosition
                , menuWidth, menuHeight), "Level Completed");

            GUI.skin.label.alignment = TextAnchor.UpperCenter;
            GUI.Label(new Rect
                ( leftPosition
                , topPosition + 30
                , menuWidth, 50), "Message to the developer");
            GUI.skin.label.alignment = TextAnchor.UpperLeft;   

            levelComment = GUI.TextArea(new Rect
                ( centerPosition - (menuWidth / 2) + 10
                , topPosition + 50
                , menuWidth - 20, 200), levelComment);
            
            GUI.skin.label.alignment = TextAnchor.UpperCenter;
            if (GUI.Button(new Rect
                ( centerPosition - buttonWidth - 10
                , (Screen.height / 2) + (menuHeight / 2) - buttonHeight - 10
                , buttonWidth, buttonHeight), "Restart"))
            {
                RestartLevel();
            }

            if (GUI.Button(new Rect
                ( centerPosition + 10
                , (Screen.height / 2) + (menuHeight / 2) - buttonHeight - 10
                , buttonWidth, buttonHeight), "Next Level"))
            {
                GoToNextLevel();
            }

            GUI.skin.label.alignment = TextAnchor.UpperLeft;            
        }
    }
}
