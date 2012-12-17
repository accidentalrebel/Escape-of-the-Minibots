using UnityEngine;
using System.Collections;

public class ReplayEvent {

    public enum EventType { PressedLeft, ReleasedLeft, PressedRight, ReleasedRight, PressedJump, ReleasedJump, PressedUse, ReleasedUse, PressedPickUp, ReleasedPickUp };

    internal float timeTriggered;
    internal EventType eventType;

    internal void Initialize(float theTimeTriggered, EventType theEventType)
    {
        timeTriggered = theTimeTriggered;
        eventType = theEventType;
    }
}
