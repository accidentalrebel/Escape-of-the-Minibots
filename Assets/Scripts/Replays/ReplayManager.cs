using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReplayManager : MonoBehaviour {

    Main main;
    List<ReplayEvent> eventList = new List<ReplayEvent>();
    float startTime;
    bool isPlayingReplay = false;
    internal bool canRecord = true;

    void Awake()
    {
        Registry.replayManager = this;
    }

    internal void StartRecording()
    {
        if (isPlayingReplay)
            return;

        canRecord = true;
        startTime = Time.time;
        eventList.Clear();
    }

    internal void StopRecording()
    {
        canRecord = false;
        eventList.Clear();
    }

    internal void AddEvent(float eventTime, ReplayEvent.EventType eventType)
    {
        if (canRecord)
        {
            ReplayEvent newEvent = new ReplayEvent();
            newEvent.Initialize(eventTime - startTime, eventType);
            eventList.Add(newEvent);
        }
    }

    internal void StartReplay()
    {
        StartCoroutine("Replay");
    }

    internal void StopReplay()
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

    internal string GetReplayDataString()
    {
        string replayData = "";
        foreach ( ReplayEvent replayEvent in eventList )
        {
            replayData += (replayEvent.timeTriggered.ToString("f4") + "%" + ((int)replayEvent.eventType).ToString()) + "#";
        }

        return replayData;
    }
}
