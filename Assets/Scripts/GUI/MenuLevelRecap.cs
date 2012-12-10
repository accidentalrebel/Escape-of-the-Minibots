using UnityEngine;
using System.Collections;

public class MenuLevelRecap : MonoBehaviour {

    bool isVisible = false;
    public int menuWidth = 400;
    public int menuHeight = 400;

	// Use this for initialization
	void Start () {
        Registry.eventDispatcher.ELevelCompleted += ShowMenu;
	}

    void ShowMenu()
    {
        isVisible = true;
    }

    void OnGUI()
    {
        if (isVisible)
        {
            GUI.Box(new Rect
                ( (Screen.width / 2) - (menuWidth / 2)
                , (Screen.height / 2) - (menuHeight / 2)
                , menuWidth, menuHeight), "Test");
        }
    }
}
