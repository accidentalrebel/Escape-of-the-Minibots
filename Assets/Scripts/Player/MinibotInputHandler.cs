using UnityEngine;
using System.Collections;

public class MinibotInputHandler : MonoBehaviour {

    float xAxis = 0;
    internal float XAxis
    { get { return xAxis; } }

    float yAxis = 0;
    internal float YAxis
    { get { return yAxis; } }

    bool jumpButton = false;    
    internal bool JumpButton
    { get { return jumpButton; } }

    bool useButton = false ;
    internal bool UseButton
    { get { return useButton; } }

    bool pickupButton = false;
    internal bool PickupButton
    { get { return pickupButton; } }

    private bool hasPressedRight = false;

	// Use this for initialization
	void Start () {
        Registry.inputHandler = this;
	}
	
	// Update is called once per frame
	void Update () {
        //if (hasPressedRight == false)
        //    xAxis = Input.GetAxis("Horizontal");
        //else
        //    xAxis = 1;
        if (Registry.replayManager.isReplayMode)
        {
            if (hasPressedRight)
            {
                xAxis = 1;
            }
            else
            {
                xAxis = 0;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedRight);
                xAxis = 1;
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.ReleasedRight);
                xAxis = 0;
            }
        }

        yAxis = Input.GetAxis("Vertical");
        jumpButton = Input.GetButton("Jump");
        useButton = Input.GetKeyDown(KeyCode.X);
        pickupButton = Input.GetKeyDown(KeyCode.C);
	}

    internal void PressedRight()
    {
        hasPressedRight = true;
    }

    internal void ReleasedRight()
    {
        hasPressedRight = false;
    }
}