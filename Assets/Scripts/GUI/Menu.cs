using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    protected bool isVisible = false;
    public float menuWidth = 400;
    public float menuHeight = 400;
    public float buttonWidth = 150;
    public float buttonHeight = 50;


	// Use this for initialization
	virtual protected void Start () {
	
	}
	
	// Update is called once per frame
	virtual protected void Update () {
	
	}

    internal void Show()
    {
        isVisible = true;
    }

    internal void Hide()
    {
        isVisible = false;
    }
}
