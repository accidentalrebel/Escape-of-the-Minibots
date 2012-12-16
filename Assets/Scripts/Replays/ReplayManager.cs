using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReplayManager : MonoBehaviour {

    Main main;
    List<ReplayEvent> eventList = new List<ReplayEvent>();
    float startTime;
    internal bool isReplayMode = false;

    void Awake()
    {
        Registry.replayManager = this;
    }

    internal void StartRecording()
    {
        startTime = Time.time;
    }

    internal void AddEvent(float eventTime, ReplayEvent.EventType eventType)
    {
        ReplayEvent newEvent = new ReplayEvent();
        newEvent.Initialize(eventTime - startTime, eventType);
        Debug.Log("Logged event at " + (eventTime - startTime).ToString());
        eventList.Add(newEvent);
    }

    internal void StartReplay()
    {
        StartCoroutine("Replay");
    }

    IEnumerator Replay()
    {
        isReplayMode = true;
        int index = 0;
        float replayStartTime = Time.time;
        while ( index < eventList.Count )
        {
            ReplayEvent currentEvent = eventList[index];
            yield return new WaitForSeconds(replayStartTime + currentEvent.timeTriggered - Time.time);

            if (currentEvent.eventType == ReplayEvent.EventType.PressedRight)
            {
                Debug.Log("Moved right at " + (Time.time - replayStartTime).ToString());
                Registry.inputHandler.PressedRight();
            }
            else if (currentEvent.eventType == ReplayEvent.EventType.ReleasedRight)
            {
                Debug.Log("Released right at " + (Time.time - replayStartTime).ToString());
                Registry.inputHandler.ReleasedRight();
            }
            else if (currentEvent.eventType == ReplayEvent.EventType.PressedLeft)
            {
                Debug.Log("Pressed left at " + (Time.time - replayStartTime).ToString());
                Registry.inputHandler.PressedLeft();
            }
            else if (currentEvent.eventType == ReplayEvent.EventType.ReleasedLeft)
            {
                Debug.Log("Released left at " + (Time.time - replayStartTime).ToString());
                Registry.inputHandler.ReleasedLeft();
            }

            index++;
        }

        isReplayMode = false;
        Debug.LogWarning("Replay has ended");
    }
}
