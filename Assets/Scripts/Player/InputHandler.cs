using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    public float axisSensitivity = 0.1f;
	public bool isJoystickEnabled;

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

	// ************************************************************************************
	// MAIN
	// ************************************************************************************
	void Start () {
        Registry.inputHandler = this;
        Registry.main.ELevelCompleted += ResetInput;
        Registry.main.ELevelStarted += ResetInput;
	}

	void Update () {

        if (!Registry.main.isReplayMode && !Registry.replayViewer.enabled)
        {
			HandleHorizontalInput();
			HandleJumpInput();
			HandleUseInput();
			HandlePickUpInput();
			HandleRestartInput();
			HandleMuteInput();
        }

        HandleAxis();
	}

    void LateUpdate()
    {
        _useButton = false;
        _pickupButton = false;
        _resetButton = false;
    }
        
	// ************************************************************************************
	// INPUTS
	// ************************************************************************************
    private void HandleAxis()
    {
        if ( _hasPressedRight)
            _xAxis += axisSensitivity;
        else if (_hasPressedLeft)
            _xAxis -= axisSensitivity;    
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

	public void ResetInput()
	{
		_hasPressedLeft = false;
		_hasPressedRight = false;
		_useButton = false;
		_pickupButton = false;
		_xAxis = 0;
		_jumpButton = false;
	}

	// ************************************************************************************
	// HELPERS
	// ************************************************************************************
	void HandleHorizontalInput()
	{
		if ( isJoystickEnabled && IsJoystickConnected() ) {
			if ( Input.GetAxis("Horizontal") < 0 ) {
				PressedLeft();
				ReleasedRight();
			}
			else if ( Input.GetAxis("Horizontal") > 0 ) {
				PressedRight();
				ReleasedLeft();	
			}
			else {
				ReleasedRight();
				ReleasedLeft();			
			}
		}
		else {
			if (Input.GetButtonDown("Right")) {
				Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedRight);
				PressedRight();
			}
			else if (Input.GetButtonUp("Right")) {
				Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.ReleasedRight);
				ReleasedRight();
			}
			else if (Input.GetButtonDown("Left")) {
				Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedLeft);
				PressedLeft();
			}
			else if (Input.GetButtonUp("Left"))	{
				Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.ReleasedLeft);
				ReleasedLeft();
			}
		}
	}

	void HandleJumpInput ()
	{
		if (Input.GetButtonDown("Jump")) {
			Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedJump);
			PressedJump();
		}
		else if (Input.GetButtonUp("Jump")) {
			Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.ReleasedJump);
			ReleasedJump();
		}
	}

	void HandleUseInput()
	{
		if (Input.GetButtonDown("Use"))	{
			Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedUse);
			PressedUse();
		}
	}

	void HandlePickUpInput ()
	{
		if (Input.GetButtonDown("PickUp")) {
			Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedPickUp);
			PressedPickUp();
		}
	}

	void HandleRestartInput ()
	{
		if (Input.GetButtonDown("Restart"))	{
			Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedReset);
			PressedReset();
		}
	}

	void HandleMuteInput ()
	{
		if (Input.GetButtonDown("Mute")) {
			Registry.bgmManager.toggleStatus();
		}
	}

	bool IsJoystickConnected() 
	{
		return Input.GetJoystickNames().Length > 0;
	}
}