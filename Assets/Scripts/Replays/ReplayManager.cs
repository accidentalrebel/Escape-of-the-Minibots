using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReplayManager : MonoBehaviour {

    Main main;
    List<ReplayEvent> replayList = new List<ReplayEvent>();
    float startTime;
    internal bool isReplayMode = false;

	// Use this for initialization
	void Awake () 
    {
        Registry.replayManager = this;

        main = GetComponent<Main>();
        if (main == null)
            Debug.LogError("main is not found!");

        main.ELevelStarted += LLevelStarted;
        main.ELevelCompleted += LLevelCompleted;
	}

    void LLevelStarted()
    {
        Debug.LogWarning("level has started");

        isReplayMode = false;
        StopCoroutine("PlayReplay");
        StartCoroutine("RecordEvents");
    }

    void LLevelCompleted()
    {
        Debug.LogWarning("level was completed");        
    }

    internal void StartReplay()
    {
        StopCoroutine("RecordEvents");        
        StartCoroutine("PlayReplay");
    }

    IEnumerator RecordEvents()
    {
        startTime = Time.time;
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.D)
                || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log("Pressed at " + (Time.time - startTime).ToString());
                ReplayEvent newEvent = new ReplayEvent();
                newEvent.Initialize(Time.time - startTime, ReplayEvent.EventType.PressedRight);
                replayList.Add(newEvent);
            }
            else if (Input.GetKeyUp(KeyCode.D)
                || Input.GetKeyUp(KeyCode.RightArrow))
            {
                Debug.Log("Released at " + (Time.time - startTime).ToString());
                ReplayEvent newEvent = new ReplayEvent();
                newEvent.Initialize(Time.time - startTime, ReplayEvent.EventType.ReleasedRight);
                replayList.Add(newEvent);
            }

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator PlayReplay()
    {
        startTime = Time.time;
        isReplayMode = true;
        int index = 0;
        while (index < replayList.Count)
        {
            ReplayEvent currentEvent = replayList[index];
            float timeTriggered = currentEvent.timeTriggered;

            Debug.Log("Wait for " + (timeTriggered - (Time.time - startTime)).ToString());
            yield return new WaitForSeconds(timeTriggered - (Time.time - startTime)); 

            //if (Time.time - startTime > timeTriggered)            
            {
                if (currentEvent.eventType == ReplayEvent.EventType.PressedRight)
                {
                    Debug.Log("Pressed at " + (Time.time - startTime).ToString());
                    Registry.inputHandler.MoveRight = true;
                }
                else if (currentEvent.eventType == ReplayEvent.EventType.ReleasedRight)
                {
                    Debug.Log("Released at " + (Time.time - startTime).ToString());
                    Registry.inputHandler.MoveRight = false;
                }
                index++;
            }
        }

        isReplayMode = false;
        Debug.LogWarning("Replay has ended");
    }
}
