using UnityEngine;
using System.Collections;

public class EventDispatcher : MonoBehaviour {

    public delegate void EventHandler();
    public event EventHandler UpdateMinibotCount;
    public event EventHandler UpdateTimer;

    void Awake()
    {
        Registry.eventDispatcher = this;        
    }

    internal void OnUpdateMinibotCount()
    {
        UpdateMinibotCount();
    }

    internal void OnUpdateTimer()
    {
        UpdateTimer();
    }
}
