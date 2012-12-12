using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReplayManager : MonoBehaviour {

    Main main;
    List<ReplayEvent> replayList = new List<ReplayEvent>();
    float startTime;

	// Use this for initialization
	void Awake () 
    {
        main = GetComponent<Main>();
        if (main == null)
            Debug.LogError("main is not found!");

        main.ELevelStarted += LLevelStarted;
        main.ELevelCompleted += LLevelCompleted;
	}

    void LLevelStarted()
    {
        Debug.LogWarning("level has started");
        startTime = Time.time;
        StartCoroutine("RecordEvents");
    }

    void LLevelCompleted()
    {
        Debug.LogWarning("level was completed");
        StopCoroutine("RecordEvents");
        startTime = Time.time;
        StartCoroutine("PlayReplay");
    }

    IEnumerator RecordEvents()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.D)
                || Input.GetKeyDown(KeyCode.RightArrow))
            {
                ReplayEvent newEvent = new ReplayEvent();
                newEvent.Initialize(Time.time - startTime, ReplayEvent.EventType.PressedRight);
                replayList.Add(newEvent);
            }
            else if (Input.GetKeyUp(KeyCode.D)
                || Input.GetKeyUp(KeyCode.RightArrow))
            {
                ReplayEvent newEvent = new ReplayEvent();
                newEvent.Initialize(Time.time - startTime, ReplayEvent.EventType.ReleasedRight);
                replayList.Add(newEvent);
            }

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator PlayReplay()
    {
        int index = 0;
        while (index < replayList.Count)
        {
            ReplayEvent currentEvent = replayList[index];
            float timeTrigged = currentEvent.timeTriggered;
            if (Time.time - startTime > timeTrigged)
            {
                if (currentEvent.eventType == ReplayEvent.EventType.PressedRight)
                {
                    Debug.Log("Pressed RIGHT");
                }
                else if (currentEvent.eventType == ReplayEvent.EventType.ReleasedRight)
                {
                    Debug.Log("Released RIGHT");
                }
                index++;
            }

            yield return new WaitForFixedUpdate();            
        }

        Debug.LogWarning("Replay has ended");
    }
}
