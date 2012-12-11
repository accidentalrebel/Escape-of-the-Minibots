using UnityEngine;
using System.Collections;

public class LevelSelectionMenu : Menu {

    Vector2 scrollPosition;

    protected override void Start()
    {
        base.Start();

        scrollPosition = Vector2.zero;
    }

    void OnGUI()
    {
        if (isVisible)
        {
            GUI.Box(new Rect
                (centerPosition - (menuWidth / 2)
                , topPosition
                , menuWidth, menuHeight), "Level Selection");
            
            scrollPosition = GUI.BeginScrollView(new Rect(leftPosition, topPosition + 40, menuWidth, menuHeight - 60)
                , scrollPosition
                , new Rect(0, 0, menuWidth - 40, menuHeight));

            float initialYPos = 40;
            
            for (int i = 0; i < 15; i++)
            {
                GUI.Button(new Rect((menuWidth / 2) - (buttonWidth / 2) - 20, (initialYPos * i) + 10, 200, 30), "Button");
            }

            GUI.EndScrollView();
        }
    }
}
