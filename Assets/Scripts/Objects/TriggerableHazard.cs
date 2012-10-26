using UnityEngine;
using System.Collections;

public class TriggerableHazard : LevelObject {

    internal DynamicSizeObject dynamicSizeComponent;

    // ************************************************************************************
    // MAIN
    // ************************************************************************************
    new void Awake()
    {
        dynamicSizeComponent = gameObject.GetComponent<DynamicSizeObject>();
        if (dynamicSizeComponent == null)
        {
            Debug.LogError("dynamicSizeComponent not specified");
            return;
        }
    }

    new void Start()
    {

    }

    internal void Initialize(Vector3 theStartingPos, Vector2 theBlockSize)
    {
        base.Initialize(theStartingPos);
        dynamicSizeComponent.Initialize(Registry.prefabHandler.pfHazard, theBlockSize);
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
