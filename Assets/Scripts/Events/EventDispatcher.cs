using UnityEngine;
using System.Collections;

public class EventDispatcher : MonoBehaviour {

    public delegate void EventHandler();
    public delegate void EventHandlerString(string s);   
    public event EventHandler UpdateTimer;
    public event EventHandlerString UpdateMinibotCount;

    void Awake()
    {
        Registry.eventDispatcher = this;        
    }

    internal void OnUpdateMinibotCount(string s)
    {
        UpdateMinibotCount(s);
    }

    internal void OnUpdateTimer()
    {
        UpdateTimer();
    }
}
