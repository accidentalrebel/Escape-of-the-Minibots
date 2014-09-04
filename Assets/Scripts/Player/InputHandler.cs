using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    public float axisSensitivity = 0.1f;

    float _xAxis = 0;
    public float xAxis
    { get { return _xAxis; } }

    float _yAxis = 0;
	public float yAxis
    { get { return _yAxis; } }

    bool _jumpButton = false;    
	public  bool jumpButton
    { get { return _jumpButton; } }

    bool _useButton = false ;
	public bool useButton
    { get { return _useButton; } }

    bool _pickupButton = false;
	public bool pickupButton
    { get { return _pickupButton; } }

    bool _resetButton = false;
	public bool resetButton
    { get { return _resetButton; } }

    private bool _hasPressedRight = false;
    private bool _hasPressedLeft = false;

	// Use this for initialization
	void Start () {
        Registry.inputHandler = this;
        Registry.main.ELevelCompleted += ResetInput;
        Registry.main.ELevelStarted += ResetInput;
	}
	
	// Update is called once per frame
	void Update () {

        if (!Registry.main.isReplayMode && !Registry.replayViewer.enabled)
        {
			if ( Input.GetAxis("Horizontal") < 0 ) {
				PressedLeft();
			}
			else if ( Input.GetAxis("Horizontal") > 0 ) {
				PressedRight();
			}
			else
			{
				ReleasedRight();
				ReleasedLeft();
			}

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

			else if (Input.GetKeyDown(KeyCode.M))
			{
				Registry.bgmManager.toggleStatus();
			}
        }

        HandleAxis();
	}
    
	public void ResetInput()
    {
        _hasPressedLeft = false;
        _hasPressedRight = false;
        _useButton = false;
        _pickupButton = false;
        _xAxis = 0;
        _jumpButton = false;
    }

    void LateUpdate()
    {
        _useButton = false;
        _pickupButton = false;
        _resetButton = false;
    }
        
    private void HandleAxis()
    {
        if ( _hasPressedRight)
            _xAxis += axisSensitivity;
        else if (_hasPressedLeft)
            _xAxis -= axisSensitivity;        
        // If no keys are pressed
        else
        {
            if (_xAxis > axisSensitivity)
                _xAxis -= axisSensitivity;
            else if (_xAxis < -axisSensitivity)
                _xAxis += axisSensitivity;
            else
                _xAxis = 0;
        }

        // We cap the values to 1 and -1
        if (_xAxis > 1)
            _xAxis = 1;
        else if (_xAxis < -1)
            _xAxis = -1;
    }

	public void PressedRight()
    {
        _hasPressedRight = true;
    }

	public void ReleasedRight()
    {
        _hasPressedRight = false;
    }

	public void PressedLeft()
    {
        _hasPressedLeft = true;
    }

	public void ReleasedLeft()
    {
        _hasPressedLeft = false;
    }

	public void PressedJump()
    {
        _jumpButton = true;
    }

	public void ReleasedJump()
    {
        _jumpButton = false;
    }

	public void PressedUse()
    {        
        _useButton = true;
    }

	public void PressedPickUp()
    {
        _pickupButton = true;
    }

	public void PressedReset()
    {
        _resetButton = true;
    }
}