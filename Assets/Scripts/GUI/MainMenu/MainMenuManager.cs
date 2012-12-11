using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

    FrontMenu frontMenu;
    LevelSelectionMenu levelSelectionMenu;

	// Use this for initialization
	void Start () {
        frontMenu = GetComponentInChildren<FrontMenu>();
        if (frontMenu == null)
            Debug.LogError("Front menu not found!");

        levelSelectionMenu = GetComponentInChildren<LevelSelectionMenu>();
        if (levelSelectionMenu == null)
            Debug.LogError("LevelSelectionMenu not found!");

        frontMenu.Show();
        frontMenu.EGoToLevelSelection += LGoToLevelSelection;
	}

    private void LGoToLevelSelection()
    {
        frontMenu.Hide();
        levelSelectionMenu.Show();
        Debug.Log("Going to level selection screen");
    }
}
