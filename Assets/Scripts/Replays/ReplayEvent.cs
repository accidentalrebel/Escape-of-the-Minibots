using UnityEngine;
using System.Collections;

public class ReplayEvent: ScriptableObject {

    public enum EventType { PressedLeft, ReleasedLeft, PressedRight, ReleasedRight, PressedJump, ReleasedJump, PressedUse, ReleasedUse, PressedPickUp, ReleasedPickUp, PressedReset };

    float timeTriggered;
    EventType eventType;

    void Initialize(float theTimeTriggered, EventType theEventType)
    {
        timeTriggered = theTimeTriggered;
        eventType = theEventType;
    }
}
