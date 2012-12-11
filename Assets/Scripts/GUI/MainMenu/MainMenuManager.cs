using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

    FrontMenu frontMenu;

	// Use this for initialization
	void Start () {
        frontMenu = GetComponentInChildren<FrontMenu>();
        if (frontMenu == null)
            Debug.LogError("Front menu not found!");

        frontMenu.Show();
        frontMenu.EGoToLevelSelection += LGoToLevelSelection;
	}

    private void LGoToLevelSelection()
    {
        Debug.Log("Going to level selection screen");
    }
}
