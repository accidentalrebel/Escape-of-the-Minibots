using UnityEngine;
using System.Collections;

public class EventDispatcher : MonoBehaviour {

    public delegate void EventHandler();
    public delegate void EventHandlerString(string s);   
    public event EventHandlerString EUpdateTimer;
    public event EventHandler EFinishedLevelLoading;
    public event EventHandlerString EUpdateMinibotCount;
    public event EventHandler ELevelCompleted;

    void Awake()
    {
        Registry.eventDispatcher = this;        
    }

    internal void OnUpdateMinibotCount(string s)
    {
        EUpdateMinibotCount(s);
    }

    internal void OnFinishLevelLoading()
    {
        EFinishedLevelLoading();        
    }

    internal void OnUpdateTimer(string s)
    {
        EUpdateTimer(s);
    }

    internal void OnLevelComplete()
    {
        ELevelCompleted();
    }
}
