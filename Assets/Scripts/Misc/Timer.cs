using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    float startTime;
    

	// Use this for initialization
	void Awake () 
    {
        startTime = Time.time;        
	}

    void Update()
    {
        float timeElapsed = Time.time - startTime;
        string minutes = Mathf.Floor(timeElapsed / 60).ToString("00");
        string seconds = (timeElapsed % 60).ToString("00");
        string milliseconds = ((timeElapsed * 100) % 100).ToString("00");

        Registry.eventDispatcher.OnUpdateTimer(minutes + ":" + seconds + ":" + milliseconds);
    }
}
