using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

    FrontMenu frontMenu;
    LevelSelectionMenu levelSelectionMenu;
    Settings settings;

	// Use this for initialization
	void Start () {
        frontMenu = GetComponentInChildren<FrontMenu>();
        if (frontMenu == null)
            Debug.LogError("Front menu not found!");

        levelSelectionMenu = GetComponentInChildren<LevelSelectionMenu>();
        if (levelSelectionMenu == null)
            Debug.LogError("LevelSelectionMenu not found!");

        settings = GameObject.Find("Settings").GetComponent<Settings>();
        if (settings == null)
            Debug.LogError("Settings not found!");

        frontMenu.Show();
        frontMenu.EGoToLevelSelection += LGoToLevelSelection;
        levelSelectionMenu.ELoadLevel += LLoadLevel;
	}

    private void LGoToLevelSelection()
    {
        frontMenu.Hide();
        levelSelectionMenu.Show();
        Debug.Log("Going to level selection screen");
    }

    private void LLoadLevel(int i)
    {
        Debug.Log("Loading level " + i.ToString());
        settings.InitialLevelToLoad = i;
        Application.LoadLevel("MainGame");
    }
}
