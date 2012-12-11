using UnityEngine;
using System.Collections;

public class LevelSelectionMenu : Menu {

    public delegate void EventHandler(int i);
    public event EventHandler ELoadLevel;
    public LevelList levelList;

    Vector2 scrollPosition;

    protected override void Start()
    {
        base.Start();

        if (levelList == null)
            Debug.LogError("LevelList not specified!");

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
                , new Rect(0, 0, menuWidth - 40, ((buttonHeight - 10) * levelList.members.Count)));

            float initialYPos = 40;

            int i = 0;
            foreach( string levelName in levelList.members )
            {
                if (GUI.Button(new Rect((menuWidth / 2) - (buttonWidth / 2) - 20, (initialYPos * i) + 10, 200, 30), "Level " + levelName))
                {
                    int levelToLoad;
                    if (int.TryParse(levelName, out levelToLoad))
                    {
                        ELoadLevel(levelToLoad);
                    }
                    else
                    {
                        Debug.LogWarning("Could not load the level");
                    }
                    
                    break;
                }

                i++;
            }

            GUI.EndScrollView();
        }
    }
}
