using UnityEngine;
using System.Collections;

public class EventDispatcher : MonoBehaviour {

    public delegate void EventHandler();
    public delegate void EventHandlerString(string s);   
    public event EventHandlerString UpdateTimer;
    public event EventHandler FinishedLevelLoading;
    public event EventHandlerString UpdateMinibotCount;

    void Awake()
    {
        Registry.eventDispatcher = this;        
    }

    internal void OnUpdateMinibotCount(string s)
    {
        UpdateMinibotCount(s);
    }

    internal void OnFinishLevelLoading()
    {
        FinishedLevelLoading();        
    }

    internal void OnUpdateTimer(string s)
    {
        UpdateTimer(s);
    }
}
