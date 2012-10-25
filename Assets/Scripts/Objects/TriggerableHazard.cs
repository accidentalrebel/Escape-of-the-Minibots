using UnityEngine;
using System.Collections;

public class TriggerableHazard : MonoBehaviour {

    DynamicSizeObject dynamicSizeComponent;

    void Start()
    {
        dynamicSizeComponent = gameObject.GetComponent<DynamicSizeObject>();
        if (dynamicSizeComponent == null)
        {
            Debug.LogError("dynamicSizeComponent not specified");
            return;
        }

        //dynamicSizeComponent.Initialize(Registry.prefabHandler.pfTriggerableHazard);
    }
}
