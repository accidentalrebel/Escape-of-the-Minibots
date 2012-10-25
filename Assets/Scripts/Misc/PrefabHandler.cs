using UnityEngine;
using System.Collections;

public class PrefabHandler : MonoBehaviour {

    internal Object pfTile;
    internal Object pfMinibot;
    internal Object pfBox;
    internal Object pfDoor;
    internal Object pfGravityInverter;
    internal Object pfHazard;
    internal Object pfTriggerableHazard;
    internal Object pfHorizontalInverter;
    internal Object pfMovingPlatform;
    internal Object pfStepSwitch;
    internal Object pfSwitch;
    internal Object pfTriggerableBlock;    

	// Use this for initialization
	void Awake () {
        Registry.prefabHandler = this;

        pfTile = Resources.Load(@"Prefabs/pfTile");
        if (pfTile == null)
            Debug.LogError("Can not find pfTile prefab!");
        pfMinibot = Resources.Load(@"Prefabs/pfMinibot");
        if (pfMinibot == null)
            Debug.LogError("Can not find pfMinibot prefab!");
        pfBox = Resources.Load(@"Prefabs/pfBox");
        if (pfBox == null)
            Debug.LogError("Can not find pfBox prefab!");
        pfDoor = Resources.Load(@"Prefabs/pfDoor");
        if (pfDoor == null)
            Debug.LogError("Can not find pfDoor prefab!");
        pfGravityInverter = Resources.Load(@"Prefabs/pfGravityInverter");
        if (pfGravityInverter == null)
            Debug.LogError("Can not find pfGravityInverter prefab!");
        pfHazard = Resources.Load(@"Prefabs/pfHazard");
        if (pfHazard == null)
            Debug.LogError("Can not find pfHazard prefab!");
        pfTriggerableHazard = Resources.Load(@"Prefabs/pfTriggerableHazard");
        if (pfTriggerableHazard == null)
            Debug.LogError("Can not find pfTriggerableHazard prefab!");
        pfHorizontalInverter = Resources.Load(@"Prefabs/pfHorizontalInverter");
        if (pfHorizontalInverter == null)
            Debug.LogError("Can not find pfHorizontalInverter prefab!");
        pfMovingPlatform = Resources.Load(@"Prefabs/pfMovingPlatform");
        if (pfMovingPlatform == null)
            Debug.LogError("Can not find pfMovingPlatform prefab!");
        pfStepSwitch = Resources.Load(@"Prefabs/pfStepSwitch");
        if (pfStepSwitch == null)
            Debug.LogError("Can not find pfStepSwitch prefab!");
        pfSwitch = Resources.Load(@"Prefabs/pfSwitch");
        if (pfSwitch == null)
            Debug.LogError("Can not find pfSwitch prefab!");
        pfTriggerableBlock = Resources.Load(@"Prefabs/pfTriggerableBlock");
        if (pfTriggerableBlock == null)
            Debug.LogError("Can not find pfTriggerableBlock prefab!");	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
