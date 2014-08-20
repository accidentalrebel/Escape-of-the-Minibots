using UnityEngine;
using System.Collections;

public class PrefabHandler : MonoBehaviour {

    public Object pfTile;
    public Object pfMinibot;
    public Object pfBox;
    public Object pfDoor;
    public Object pfGravityInverter;
    public Object pfHazard;
    public Object pfTriggerableHazard;
    public Object pfHorizontalInverter;
    public Object pfMovingPlatform;
    public Object pfStepSwitch;
    public Object pfSwitch;
    public Object pfTriggerableBlock;
    public Object pfTriggerableTile;
	public Object pfSurroundingTile;

	// Use this for initialization
	void Awake () {
        Registry.prefabHandler = this;        

        if (pfTile == null)
            Debug.LogError("Can not find pfTile prefab!");
        if (pfMinibot == null)
            Debug.LogError("Can not find pfMinibot prefab!");     
        if (pfBox == null)
			Debug.LogError("Can not find pfBox prefab!");
        if (pfDoor == null)
            Debug.LogError("Can not find pfDoor prefab!");
        if (pfGravityInverter == null)
            Debug.LogError("Can not find pfGravityInverter prefab!");       
        if (pfHazard == null)
            Debug.LogError("Can not find pfHazard prefab!");        
        if (pfTriggerableHazard == null)
            Debug.LogError("Can not find pfTriggerableHazard prefab!");        
        if (pfHorizontalInverter == null)
            Debug.LogError("Can not find pfHorizontalInverter prefab!");        
        if (pfMovingPlatform == null)
            Debug.LogError("Can not find pfMovingPlatform prefab!");        
        if (pfStepSwitch == null)
            Debug.LogError("Can not find pfStepSwitch prefab!");        
        if (pfSwitch == null)
            Debug.LogError("Can not find pfSwitch prefab!");        
        if (pfTriggerableBlock == null)
            Debug.LogError("Can not find pfTriggerableBlock prefab!");        
        if (pfTriggerableTile == null)
            Debug.LogError("Can not find pfTriggerableTile prefab!");	
		if (pfSurroundingTile == null)
			Debug.LogError("Can not find pfSurroundingTile prefab!");	
	}
}
