using UnityEngine;
using System.Collections;

public class MenuLevelRecap : MonoBehaviour {

    public bool isVisible = false;
    public float menuWidth = 400;
    public float menuHeight = 400;
    public float buttonWidth = 150;
    public float buttonHeight = 50;

	// Use this for initialization
	void Start () {
        Registry.main.ELevelCompleted += LevelCompleted;
	}

    void LevelCompleted()
    {
        isVisible = true;
    }

    void Update()
    {
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

    void OnGUI()
    {
        if (isVisible)
        {
            float centerPosition = (Screen.width / 2);
            float topPosition = (Screen.height / 2) - (menuHeight / 2);
            GUI.Box(new Rect
                ( centerPosition - (menuWidth / 2)
                , topPosition
                , menuWidth, menuHeight), "Test");
            
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

    private void RestartLevel()
    {
        Registry.main.RestartLevel();
        isVisible = false;
    }

    private void GoToNextLevel()
    {
        Registry.main.GoToNextLevel();
        isVisible = false;
    }
}
