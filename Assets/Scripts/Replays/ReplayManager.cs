using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReplayManager : MonoBehaviour {

    Main main;
    List<ReplayEvent> eventList = new List<ReplayEvent>();

    float startTime;
    bool isPlayingReplay = false;

    bool canRecord = true;
    public ReplayViewer replayViewer;

    void Awake()
    {
        Registry.replayManager = this;
    }

    void Start()
    {
        replayViewer = GetComponent<ReplayViewer>();
        if (replayViewer == null)
            Debug.LogError("replayViewer is not found!");
    }

    public void StartRecording()
    {
        if (isPlayingReplay)
            return;

        canRecord = true;
        startTime = Time.time;
        eventList.Clear();
    }

	public void StopRecording()
    {
        canRecord = false;
        eventList.Clear();
    }

	public void AddEvent(float eventTime, ReplayEvent.EventType eventType)
    {
        if (canRecord)
        {
            ReplayEvent newEvent = (ReplayEvent) ScriptableObject.CreateInstance("ReplayEvent");
            newEvent.Initialize(eventTime - startTime, eventType);
            eventList.Add(newEvent);
        }
    }

	public void StartReplay()
    {
        StartCoroutine("Replay");
    }

    public void StopReplay()
    {
        StopCoroutine("Replay");
        isPlayingReplay = false;
    }    

    IEnumerator Replay()
    {
        isPlayingReplay = true;
        int index = 0;
        float replayStartTime = Time.time;
        while ( index < eventList.Count )
        {
            ReplayEvent currentEvent = eventList[index];
            yield return new WaitForSeconds(replayStartTime + currentEvent.timeTriggered - Time.time);

            if (currentEvent.eventType == ReplayEvent.EventType.PressedRight)
            {
                Registry.inputHandler.PressedRight();
            }
            else if (currentEvent.eventType == ReplayEvent.EventType.ReleasedRight)
            {
                Registry.inputHandler.ReleasedRight();
            }
            else if (currentEvent.eventType == ReplayEvent.EventType.PressedLeft)
            {
                Registry.inputHandler.PressedLeft();
            }
            else if (currentEvent.eventType == ReplayEvent.EventType.ReleasedLeft)
            {
                Registry.inputHandler.ReleasedLeft();
            }
            else if (currentEvent.eventType == ReplayEvent.EventType.PressedJump)
            {
                Registry.inputHandler.PressedJump();
            }
            else if (currentEvent.eventType == ReplayEvent.EventType.ReleasedJump)
            {
                Registry.inputHandler.ReleasedJump();
            }
            else if (currentEvent.eventType == ReplayEvent.EventType.PressedUse)
            {
                Registry.inputHandler.PressedUse();
            }
            else if (currentEvent.eventType == ReplayEvent.EventType.PressedPickUp)
            {
                Registry.inputHandler.PressedPickUp();
            }
            else if (currentEvent.eventType == ReplayEvent.EventType.PressedReset)
            {
                Registry.inputHandler.PressedReset();
            }

            index++;
        }

        isPlayingReplay = false;
        Debug.Log("Replay has ended");
    }
    
    public string GetReplayDataString()
    {
        string replayData = "";
        foreach ( ReplayEvent replayEvent in eventList )
        {
            replayData += (replayEvent.timeTriggered.ToString("f4") + "%" + ((int)replayEvent.eventType).ToString()) + "#";
        }

        return replayData;
    }
}
