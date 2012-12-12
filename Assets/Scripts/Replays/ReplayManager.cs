using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReplayManager : MonoBehaviour {

    Main main;
    List<ReplayEvent> replayList = new List<ReplayEvent>();
    private bool canRecord;

	// Use this for initialization
	void Awake () 
    {
        main = GetComponent<Main>();
        if (main == null)
            Debug.LogError("main is not found!");

        main.ELevelStarted += LLevelStarted;
	}

    void LLevelStarted()
    {
        Debug.LogWarning("level has started");
        canRecord = true;
    }

    void Update()
    {
        if (canRecord)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ReplayEvent newEvent = new ReplayEvent();
                newEvent.Initialize(Time.time);
                replayList.Add(newEvent);
            }
        }
    }
}
