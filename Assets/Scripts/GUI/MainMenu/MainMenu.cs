using UnityEngine;
using System.Collections;

public class MainMenu : Menu {

	// Use this for initialization
	protected override void Start () 
    {
        base.Start();
	}

    protected override void Update()
    {
        base.Update();
    }

    void OnGUI()
    {
        if (isVisible)
        {
            float centerPosition = (Screen.width / 2);
            float leftPosition = centerPosition - (menuWidth /2 );
            float topPosition = (Screen.height / 2) - (menuHeight / 2);
            GUI.Box(new Rect
                (centerPosition - (menuWidth / 2)
                , topPosition
                , menuWidth, menuHeight), "Main Menu");

            GUI.skin.label.alignment = TextAnchor.UpperCenter;
            GUI.skin.label.fontSize = 52;
            GUI.Label( new Rect
                ( leftPosition
                , topPosition + 50
                , menuWidth, 150), "Escape of the Minibots");
            GUI.skin.label.fontSize = 11;

            if (GUI.Button(new Rect
                ( centerPosition - (buttonWidth / 2)
                , (Screen.height / 2) + (menuHeight / 2) - buttonHeight - 20
                , buttonWidth, buttonHeight), "Start Game"))
            {
                GoToLevelSelection();
            }

            GUI.skin.label.alignment = TextAnchor.UpperLeft;
        }
    }

    private void GoToLevelSelection()
    {
        throw new System.NotImplementedException();
    }
}
