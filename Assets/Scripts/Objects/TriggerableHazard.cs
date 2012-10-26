using UnityEngine;
using System.Collections;

public class TriggerableHazard : LevelObject {

    DynamicSizeObject dynamicSizeComponent;

    // ************************************************************************************
    // MAIN
    // ************************************************************************************
    void Start()
    {
        dynamicSizeComponent = gameObject.GetComponent<DynamicSizeObject>();
        if (dynamicSizeComponent == null)
        {
            Debug.LogError("dynamicSizeComponent not specified");
            return;
        }

        dynamicSizeComponent.Initialize(Registry.prefabHandler.pfHazard);
    }

    // ************************************************************************************
    // OBJECT EDITING
    // ************************************************************************************
    internal override void GetEditableAttributes(LevelEditor levelEditor)
    {
        GUI.Label(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 110, 50, 20), "Width");
        dynamicSizeComponent.BlockWidth = GUI.TextField(new Rect((Screen.width / 2) - 90, (Screen.height / 2) - 110, 100, 20), dynamicSizeComponent.BlockWidth);

        GUI.Label(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 80, 50, 20), "Height");
        dynamicSizeComponent.BlockHeight = GUI.TextField(new Rect((Screen.width / 2) - 90, (Screen.height / 2) - 80, 100, 20), dynamicSizeComponent.BlockHeight);
    }
}
