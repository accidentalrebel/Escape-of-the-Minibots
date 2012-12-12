using UnityEngine;
using System.Collections;

public class ReplayEvent : ScriptableObject {

    float timeTriggered;

    internal void Initialize(float theTimeTriggered)
    {
        timeTriggered = theTimeTriggered;
    }
}
