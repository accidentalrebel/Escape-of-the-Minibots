using UnityEngine;
using System.Collections;

public class MinibotInputHandler : MonoBehaviour {

    bool moveRight = false;
    internal bool MoveRight
    {
        get { return moveRight; }
    }

	// Use this for initialization
	void Start () {
        Registry.inputHandler = this;
	}
	
	// Update is called once per frame
	void Update () {
        if ( !Registry.replayManager.isReplayMode)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                moveRight = true;
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                moveRight = false;
            }
        }
	}
}