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

        HandleSimulatedAxis();

        //yAxis = Input.GetAxis("Vertical");
        jumpButton = Input.GetButton("Jump");
        useButton = Input.GetKeyDown(KeyCode.X);
        pickupButton = Input.GetKeyDown(KeyCode.C);
	}

    private void HandleSimulatedAxis()
    {
        if (hasPressedRight)
        {
            simulatedXAxis += 0.1f;

            if (simulatedXAxis > 1)
                simulatedXAxis = 1;
        }
        else
        {
            simulatedXAxis -= 0.1f;
            
            if (simulatedXAxis < 0)
                simulatedXAxis = 0;
        }

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
}