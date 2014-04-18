using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    public float axisSensitivity = 0.1f;

    float xAxis = 0;
    public float XAxis
    { get { return xAxis; } }

    float yAxis = 0;
	public float YAxis
    { get { return yAxis; } }

    bool jumpButton = false;    
	public  bool JumpButton
    { get { return jumpButton; } }

    bool useButton = false ;
	public bool UseButton
    { get { return useButton; } }

    bool pickupButton = false;
	public bool PickupButton
    { get { return pickupButton; } }

    bool resetButton = false;
	public bool ResetButton
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

        if (!Registry.main.isReplayMode && !Registry.replayManager.replayViewer.isEnabled)
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
            
			if (Input.GetKeyDown(KeyCode.A)
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
            
			if (Input.GetButtonDown("Jump"))
            {
                Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedJump);
                PressedJump();
            }
            else if (Input.GetButtonUp("Jump"))
            {
                Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.ReleasedJump);
                ReleasedJump();
            }
            
			if (Input.GetKeyDown(KeyCode.X))
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

    void PressedRight()
    {
        hasPressedRight = true;
    }

    void ReleasedRight()
    {
        hasPressedRight = false;
    }

    void PressedLeft()
    {
        hasPressedLeft = true;
    }

    void ReleasedLeft()
    {
        hasPressedLeft = false;
    }

    void PressedJump()
    {
        jumpButton = true;
    }

    void ReleasedJump()
    {
        jumpButton = false;
    }

    void PressedUse()
    {        
        useButton = true;
    }

    void PressedPickUp()
    {
        pickupButton = true;
    }

    void PressedReset()
    {
        resetButton = true;
    }
}