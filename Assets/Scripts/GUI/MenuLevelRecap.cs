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
            float centerPosition = (Screen.width / 2);
            float topPosition = (Screen.height / 2) - (menuHeight / 2);
            GUI.Box(new Rect
                ( centerPosition - (menuWidth / 2)
                , topPosition
                , menuWidth, menuHeight), "Test");
            
            GUI.skin.label.alignment = TextAnchor.UpperCenter;
            GUI.Button(new Rect
                ( centerPosition - (buttonWidth / 2)
                , topPosition
                , buttonWidth, buttonHeight), "Test");
            GUI.skin.label.alignment = TextAnchor.UpperLeft;
            
        }
    }
}
