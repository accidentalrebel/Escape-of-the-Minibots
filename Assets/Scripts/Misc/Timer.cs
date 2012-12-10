using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    float startTime;
    

	// Use this for initialization
	void Awake () 
    {
        startTime = Time.time;
        Registry.eventDispatcher.ELevelCompleted += LLevelCompleted;
	}

    void Start()
    {
        StartCoroutine("StartTimer");
    }

    void LLevelCompleted()
    {
        StopCoroutine("StartTimer");
    }

    IEnumerator StartTimer()
    {
        while (true)
        {
            float timeElapsed = Time.time - startTime;
            string minutes = Mathf.Floor(timeElapsed / 60).ToString("00");
            string seconds = (timeElapsed % 60).ToString("00");
            string milliseconds = ((timeElapsed * 100) % 60).ToString("00");

            Registry.eventDispatcher.OnUpdateTimer(minutes + ":" + seconds + ":" + milliseconds);
            yield return new WaitForSeconds(0.01f);
        }       
    }
}
