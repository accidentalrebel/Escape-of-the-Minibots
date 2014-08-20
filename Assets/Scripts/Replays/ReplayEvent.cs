using UnityEngine;
using System.Collections;

public class ReplayEvent: ScriptableObject {

    public enum EventType { PressedLeft, ReleasedLeft, PressedRight, ReleasedRight, PressedJump, ReleasedJump, PressedUse, ReleasedUse, PressedPickUp, ReleasedPickUp, PressedReset };

	public float timeTriggered;
	public EventType eventType;

	public void Initialize(float theTimeTriggered, EventType theEventType)
    {
        timeTriggered = theTimeTriggered;
        eventType = theEventType;
    }
}
