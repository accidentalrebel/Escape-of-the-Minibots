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

	public void OnPressedRight()
    {
		Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedRight);
        _hasPressedRight = true;
    }

	public void OnReleasedRight()
    {
		Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.ReleasedRight);
        _hasPressedRight = false;
    }

	public void OnPressedLeft()
    {
		Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedLeft);
        _hasPressedLeft = true;
    }

	public void OnReleasedLeft()
    {
		Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.ReleasedLeft);
        _hasPressedLeft = false;
    }

	public void OnPressedJump()
    {
		Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedJump);
        _jumpButton = true;
    }

	public void OnReleasedJump()
    {
		Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.ReleasedJump);
        _jumpButton = false;
    }

	public void OnPressedUse()
    {        
		Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedUse);
        _useButton = true;
    }

	public void OnPressedPickUp()
    {
		Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedPickUp);
        _pickupButton = true;
    }

	public void OnPressedReset()
    {
		Registry.replayManager.AddEvent(Time.time, ReplayEvent.EventType.PressedReset);
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
				OnPressedLeft();
				OnReleasedRight();
			}
			else if ( Input.GetAxis("Horizontal") > 0 ) {
				OnPressedRight();
				OnReleasedLeft();	
			}
			else {
				OnReleasedRight();
				OnReleasedLeft();			
			}
		}
		else {
			if (Input.GetButtonDown("Right"))
				OnPressedRight();
			else if (Input.GetButtonUp("Right"))
				OnReleasedRight();
			else if (Input.GetButtonDown("Left"))
				OnPressedLeft();
			else if (Input.GetButtonUp("Left"))
				OnReleasedLeft();
		}
	}

	void HandleJumpInput ()
	{
		if (Input.GetButtonDown("Jump"))
			OnPressedJump();
		else if (Input.GetButtonUp("Jump"))
			OnReleasedJump();
	}

	void HandleUseInput()
	{
		if (Input.GetButtonDown("Use"))
			OnPressedUse();
	}

	void HandlePickUpInput ()
	{
		if (Input.GetButtonDown("PickUp"))
			OnPressedPickUp();
	}

	void HandleRestartInput ()
	{
		if (Input.GetButtonDown("Restart"))
			OnPressedReset();
	}

	void HandleMuteInput ()
	{
		if (Input.GetButtonDown("Mute"))
			Registry.bgmManager.toggleStatus();
	}

	bool IsJoystickConnected() 
	{
		return Input.GetJoystickNames().Length > 0;
	}
}