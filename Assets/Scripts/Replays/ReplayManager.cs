using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReplayManager : MonoBehaviour {

    Main _main;
	List<ReplayEvent> _eventList = new List<ReplayEvent>();

    float _startTime;
    bool _isPlayingReplay = false;
    bool _canRecord = true;

    void Awake()
    {
        Registry.replayManager = this;
    }

    public void StartRecording()
    {
        if (_isPlayingReplay || Registry.replayViewer.enabled)
            return;

        _canRecord = true;
        _startTime = Time.time;
		ClearReplayData();
    }

	public void StopRecording()
    {
        _canRecord = false;
		ClearReplayData();
    }

	public void AddEvent(float eventTime, ReplayEvent.EventType eventType)
    {
        if (_canRecord || Registry.replayViewer.enabled)
        {
            ReplayEvent newEvent = (ReplayEvent) ScriptableObject.CreateInstance("ReplayEvent");
            newEvent.Initialize(eventTime - _startTime, eventType);
            _eventList.Add(newEvent);
        }
    }

	public void StartReplay()
    {
        StartCoroutine("Replay");
    }

    public void StopReplay()
    {
		if ( !_isPlayingReplay )
			return;

        StopCoroutine("Replay");
        _isPlayingReplay = false;
    }    

    IEnumerator Replay()
    {
        _isPlayingReplay = true;
        int index = 0;
        float replayStartTime = Time.time;
        while ( index < _eventList.Count )
        {
            ReplayEvent currentEvent = _eventList[index];
            yield return new WaitForSeconds(replayStartTime + currentEvent.timeTriggered - Time.time);

            if (currentEvent.eventType == ReplayEvent.EventType.PressedRight)
            {
                Registry.inputHandler.OnPressedRight();
            }
            else if (currentEvent.eventType == ReplayEvent.EventType.ReleasedRight)
            {
                Registry.inputHandler.OnReleasedRight();
            }
            else if (currentEvent.eventType == ReplayEvent.EventType.PressedLeft)
            {
                Registry.inputHandler.OnPressedLeft();
            }
            else if (currentEvent.eventType == ReplayEvent.EventType.ReleasedLeft)
            {
                Registry.inputHandler.OnReleasedLeft();
            }
            else if (currentEvent.eventType == ReplayEvent.EventType.PressedJump)
            {
                Registry.inputHandler.OnPressedJump();
            }
            else if (currentEvent.eventType == ReplayEvent.EventType.ReleasedJump)
            {
                Registry.inputHandler.OnReleasedJump();
            }
            else if (currentEvent.eventType == ReplayEvent.EventType.PressedUse)
            {
                Registry.inputHandler.OnPressedUse();
            }
            else if (currentEvent.eventType == ReplayEvent.EventType.PressedPickUp)
            {
                Registry.inputHandler.OnPressedPickUp();
            }
            else if (currentEvent.eventType == ReplayEvent.EventType.PressedReset)
            {
                Registry.inputHandler.OnPressedReset();
            }

            index++;
        }

        _isPlayingReplay = false;
        Debug.Log("Replay has ended");
    }

	public void ClearReplayData()
	{
		_eventList.Clear();
	}
    
    public string GetReplayDataString()
    {
        string replayData = "";
        foreach ( ReplayEvent replayEvent in _eventList )
        {
            replayData += (replayEvent.timeTriggered.ToString("f4") + "%" + ((int)replayEvent.eventType).ToString()) + "#";
        }

        return replayData;
    }
}
