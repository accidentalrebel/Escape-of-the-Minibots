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
        eventList.Add(newEvent);
    }
}
