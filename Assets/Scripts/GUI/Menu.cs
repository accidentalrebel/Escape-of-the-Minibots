using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    protected bool isVisible = false;
    public float menuWidth = 400;
    public float menuHeight = 400;
    public float buttonWidth = 150;
    public float buttonHeight = 50;

    protected float centerPosition;
    protected float leftPosition;
    protected float topPosition; 

	// Use this for initialization
	virtual protected void Start () {
	    centerPosition = (Screen.width / 2);
        leftPosition = centerPosition - (menuWidth / 2);
        topPosition = (Screen.height / 2) - (menuHeight / 2);
	}
	
	// Update is called once per frame
	virtual protected void Update () {
	
	}

    public void Show()
    {
        isVisible = true;
    }

    public void Hide()
    {
        isVisible = false;
    }
}
