using UnityEngine;
using System.Collections;

public class FrontMenu : Menu {

    public delegate void EventHandler();
    public event EventHandler EGoToLevelSelection;

    public Settings settings;

    override protected void Start()
    {
        base.Start();

        if (settings == null)
            Debug.Log("Settings not specified!");
    }

    void OnGUI()
    {
        if (isVisible)
        {
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
            GUI.skin.label.fontSize = 12;

            GUI.Label(new Rect
                ( leftPosition
                , (Screen.height / 2) + (menuHeight / 2) - buttonHeight - 100
                , menuWidth, 150), "Enter Username:");

            GUI.skin.label.fontSize = 11;

            settings.currentUser = GUI.TextField(new Rect
                ( centerPosition - (buttonWidth / 2)
                , (Screen.height / 2) + (menuHeight / 2) - buttonHeight - 70
                , buttonWidth, 20), settings.currentUser);

            if (settings.currentUser != "")
            {
                if (GUI.Button(new Rect
                    (centerPosition - (buttonWidth / 2)
                    , (Screen.height / 2) + (menuHeight / 2) - buttonHeight - 20
                    , buttonWidth, buttonHeight), "Start Game"))
                {
                    GoToLevelSelection();
                }
            }

            GUI.skin.label.alignment = TextAnchor.UpperLeft;
        }
    }

    private void GoToLevelSelection()
    {
        EGoToLevelSelection();
    }
}
