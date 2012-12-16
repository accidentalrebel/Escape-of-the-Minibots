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
    private float simulatedXAxis = 0;
    private bool hasPressedLeft;

	// Use this for initialization
	void Start () {
        Registry.inputHandler = this;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            if ( !Registry.replayManager.isReplayMode )
                Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedRight);
            
            PressedRight();
            
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            if (!Registry.replayManager.isReplayMode)
                Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.ReleasedRight);
            
            ReleasedRight();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if ( !Registry.replayManager.isReplayMode )
                Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedLeft);
            
            PressedLeft();            
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            if (!Registry.replayManager.isReplayMode)
                Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.ReleasedLeft);
            
            ReleasedLeft();
        }

        HandleSimulatedAxis();

        //yAxis = Input.GetAxis("Vertical");
        jumpButton = Input.GetButton("Jump");
        useButton = Input.GetKeyDown(KeyCode.X);
        pickupButton = Input.GetKeyDown(KeyCode.C);
	}

    private void HandleSimulatedAxis()
    {
        if ( hasPressedRight)
            simulatedXAxis += 0.1f;
        else if (hasPressedLeft)
            simulatedXAxis -= 0.1f;        
        // If no keys are pressed
        else
        {
            if (simulatedXAxis > 0.1f)
                simulatedXAxis -= 0.1f;
            else if (simulatedXAxis < -0.1f)
                simulatedXAxis += 0.1f;
            else
                simulatedXAxis = 0;
        }

        // We cap the values to 1 and -1
        if (simulatedXAxis > 1)
            simulatedXAxis = 1;
        else if (simulatedXAxis < -1)
            simulatedXAxis = -1;

        xAxis = simulatedXAxis;
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
}