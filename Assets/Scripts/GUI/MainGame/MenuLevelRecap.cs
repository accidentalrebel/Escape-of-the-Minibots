using UnityEngine;
using System.Collections;

public class MenuLevelRecap : Menu 
{
	static private float MENU_HEIGHT = 120;
	static private float MENU_WIDTH = 400;

	override protected void Start () 
	{
        base.Start();  

		if ( Registry.replayViewer.enabled == false )
			Registry.main.ELevelCompleted += LevelCompleted;
	}

    override protected void Update()
    {
        base.Update();

        if (isVisible) {
            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Use") || Input.GetButtonDown("PickUp"))         
                GoToNextLevel();
			else if (Input.GetButtonDown("Restart"))
                RestartLevel();
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
            float topPosition = (Screen.height / 2) - (MENU_HEIGHT / 2);
            GUI.Box(new Rect
                ( centerPosition - (MENU_WIDTH / 2)
                , topPosition
			 , MENU_WIDTH, MENU_HEIGHT), "");

			GUIStyle guiStyle = new GUIStyle();
			guiStyle.fontSize = 30;
			guiStyle.normal.textColor = Color.white;
			guiStyle.alignment = TextAnchor.MiddleCenter;

			GUI.skin.label.alignment = TextAnchor.UpperCenter;
            GUI.Label(new Rect( leftPosition, topPosition + 10, MENU_WIDTH, 40), "Level Completed!", guiStyle);
			GUI.skin.label.alignment = TextAnchor.UpperLeft;   
            
            GUI.skin.label.alignment = TextAnchor.UpperCenter;
            if (GUI.Button(new Rect
                ( centerPosition - buttonWidth - 10
                , (Screen.height / 2) + (MENU_HEIGHT / 2) - buttonHeight - 10
                , buttonWidth, buttonHeight), "Restart"))
            {
                RestartLevel();
            }

            if (GUI.Button(new Rect
                ( centerPosition + 10
                , (Screen.height / 2) + (MENU_HEIGHT / 2) - buttonHeight - 10
                , buttonWidth, buttonHeight), "Next Level"))
            {
                GoToNextLevel();
            }

            GUI.skin.label.alignment = TextAnchor.UpperLeft;            
        }
    }
}
