using UnityEngine;
using System.Collections;

public class ReplayEvent : ScriptableObject {

    public enum EventType { PressedLeft, ReleasedLeft, PressedRight, ReleasedRight, PressedJump, ReleasedJump, PressedUse, PressedPickUp };

    float timeTriggered;
    EventType eventType;

    internal void Initialize(float theTimeTriggered, EventType theEventType)
    {
        timeTriggered = theTimeTriggered;
        eventType = theEventType;
    }
}
