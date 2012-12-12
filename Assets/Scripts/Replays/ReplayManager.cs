using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReplayManager : MonoBehaviour {

    Main main;
    List<ReplayEvent> replayList = new List<ReplayEvent>();

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
        StartCoroutine("RecordEvents");
    }

    void LLevelCompleted()
    {
        Debug.LogWarning("level was completed");
        StopCoroutine("RecordEvents");
    }

    IEnumerator RecordEvents()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.D)
                || Input.GetKeyDown(KeyCode.RightArrow))
            {
                ReplayEvent newEvent = new ReplayEvent();
                newEvent.Initialize(Time.time, ReplayEvent.EventType.PressedRight);
                replayList.Add(newEvent);
            }
            else if (Input.GetKeyUp(KeyCode.D)
                || Input.GetKeyUp(KeyCode.RightArrow))
            {
                ReplayEvent newEvent = new ReplayEvent();
                newEvent.Initialize(Time.time, ReplayEvent.EventType.ReleasedRight);
                replayList.Add(newEvent);
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
