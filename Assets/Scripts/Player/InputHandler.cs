using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    public float axisSensitivity = 0.1f;

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

    private bool resetButton = false;
    internal bool ResetButton
    { get { return resetButton; } }

    private bool hasPressedRight = false;
    private bool hasPressedLeft = false;

	// Use this for initialization
	void Start () {
        Registry.inputHandler = this;
        Registry.main.ELevelCompleted += ResetInput;
        Registry.main.ELevelStarted += ResetInput;
	}
	
	// Update is called once per frame
	void Update () {

        if (!Registry.main.isReplayMode)
        {
            if (Input.GetKeyDown(KeyCode.D)
                || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedRight);
                PressedRight();
            }
            else if (Input.GetKeyUp(KeyCode.D)
                || Input.GetKeyUp(KeyCode.RightArrow))
            {
                Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.ReleasedRight);
                ReleasedRight();
            }
            else if (Input.GetKeyDown(KeyCode.A)
                || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedLeft);
                PressedLeft();
            }
            else if (Input.GetKeyUp(KeyCode.A)
                || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.ReleasedLeft);
                ReleasedLeft();
            }
            else if (Input.GetButtonDown("Jump"))
            {
                Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedJump);
                PressedJump();
            }
            else if (Input.GetButtonUp("Jump"))
            {
                Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.ReleasedJump);
                ReleasedJump();
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedUse);
                PressedUse();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedPickUp);
                PressedPickUp();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedReset);
                PressedReset();
            }
        }

        HandleAxis();
	}
    
    void ResetInput()
    {
        hasPressedLeft = false;
        hasPressedRight = false;
        useButton = false;
        pickupButton = false;
        xAxis = 0;
        jumpButton = false;
    }

    void LateUpdate()
    {
        useButton = false;
        pickupButton = false;
        resetButton = false;
    }
        
    private void HandleAxis()
    {
        if ( hasPressedRight)
            xAxis += axisSensitivity;
        else if (hasPressedLeft)
            xAxis -= axisSensitivity;        
        // If no keys are pressed
        else
        {
            if (xAxis > axisSensitivity)
                xAxis -= axisSensitivity;
            else if (xAxis < -axisSensitivity)
                xAxis += axisSensitivity;
            else
                xAxis = 0;
        }

        // We cap the values to 1 and -1
        if (xAxis > 1)
            xAxis = 1;
        else if (xAxis < -1)
            xAxis = -1;
    }

    internal void PressedRight()
    {
        hasPressedRight = true;
    }

    internal void ReleasedRight()
    {
        hasPressedRight = false;
    }

    internal void PressedLeft()
    {
        hasPressedLeft = true;
    }

    internal void ReleasedLeft()
    {
        hasPressedLeft = false;
    }

    internal void PressedJump()
    {
        jumpButton = true;
    }

    internal void ReleasedJump()
    {
        jumpButton = false;
    }

    internal void PressedUse()
    {        
        useButton = true;
    }

    internal void PressedPickUp()
    {
        pickupButton = true;
    }

    internal void PressedReset()
    {
        resetButton = true;
    }
}