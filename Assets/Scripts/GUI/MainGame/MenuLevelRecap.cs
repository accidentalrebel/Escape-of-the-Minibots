using UnityEngine;
using System.Collections;

public class MenuLevelRecap : Menu {

	// Use this for initialization
	protected override void Start () {
        base.Start();
        
        Registry.main.ELevelCompleted += LevelCompleted;
	}

    protected override void Update()
    {
        base.Update();

        if (isVisible)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {                
                GoToNextLevel();
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
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
        Registry.main.RestartLevel();
        Hide();
    }

    private void GoToNextLevel()
    {        
        Registry.main.GoToNextLevel();        
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
