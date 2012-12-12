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
            else if (Input.GetKeyDown(KeyCode.C))
            {
                PlayReplay();
            }
        }
    }

    void LevelCompleted()
    {
        Show();
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

    private void PlayReplay()
    {
        Registry.main.PlayReplay();
        Registry.replayManager.StartReplay();
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
