using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[ExecuteInEditMode]
public class TriggerableBlocks : LevelObject {

    internal DynamicSizeObject dynamicSizeComponent;

    public bool isHidden = false;
    private bool startingIsHidden = false;
    bool IsHidden
    {
        get { return isHidden; }
        set { isHidden = value; UpdateChildTiles(); }
    }

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
	
	internal void Initialize(Vector3 theStartingPos, bool theIsHidden, Vector2 theBlockSize)
	{
		base.Initialize(theStartingPos);

        isHidden = theIsHidden;
        startingIsHidden = isHidden;
        dynamicSizeComponent.Initialize(Registry.prefabHandler.pfTriggerableTile, theBlockSize);
        
        UpdateChildTiles();
	}

    override internal void Use()
    {
        if (isHidden)
            isHidden = false;
        else
            isHidden = true;

        UpdateChildTiles();  
    }

    /// <summary>
    /// This sets the status of the child tiles if it is hidden or not
    /// And how the graphic handler should handle it
    /// </summary>
    private void UpdateChildTiles()
    {
        foreach (GameObject child in dynamicSizeComponent.childTiles)
        {
            if (isHidden)
            {
                child.collider.enabled = false;
                child.GetComponent<GraphicHandler>().theRenderer.enabled = false;
            }
            else
            {
                child.collider.enabled = true;
                child.GetComponent<GraphicHandler>().theRenderer.enabled = true;
            }
        }      
    }

    override internal void ResetObject()
    {
        isHidden = startingIsHidden;
        UpdateChildTiles();  
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

        IsHidden = GUI.Toggle(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 50, 150, 20), IsHidden, "Is hidden?");
    }
}
