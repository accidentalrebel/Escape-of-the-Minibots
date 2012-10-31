using UnityEngine;
using System.Collections;

public class EventDispatcher : MonoBehaviour {

    public delegate void EventHandler();
    public event EventHandler UpdateMinibotCount;

    void Awake()
    {
        Registry.eventDispatcher = this;        
    }

    internal void OnUpdateMinibotCount()
    {
        UpdateMinibotCount();
    }
}
