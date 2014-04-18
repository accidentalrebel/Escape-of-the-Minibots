using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerableBlocks : LevelObject {

    DynamicSizeObject dynamicSizeComponent;

    public bool isHidden = false;
    private bool startingIsHidden = false;
    Object prefabToSpawn;

    bool IsHidden
    {
        get { return isHidden; }
        set { isHidden = value; UpdateChildTiles(); }
    }

    // ************************************************************************************
    // MAIN
    // ************************************************************************************
    override protected void Awake()
    {
        dynamicSizeComponent = gameObject.GetComponent<DynamicSizeObject>();
        if (dynamicSizeComponent == null)
        {
            Debug.LogError("dynamicSizeComponent not specified");
            return;
        }

        prefabToSpawn = Registry.prefabHandler.pfTriggerableTile;        
    }

    override protected void Start()
    {
        
    }
	
	public void Initialize(Vector3 theStartingPos, bool theIsHidden, Vector2 theBlockSize)
	{
		base.Initialize(theStartingPos);

        isHidden = theIsHidden;
        startingIsHidden = isHidden;
        
        dynamicSizeComponent.Initialize(prefabToSpawn, theBlockSize);
        
        UpdateChildTiles();
	}

    // ************************************************************************************
    // CHILD TILES
    // ************************************************************************************
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

    override public void ResetObject()
    {
        isHidden = startingIsHidden;
        UpdateChildTiles();  
    }

    // ************************************************************************************
    // USAGE
    // ************************************************************************************
    override protected void Use(bool setToValue)
    {
        isHidden = setToValue;
        UpdateChildTiles();
    }

    override protected void Use()
    {
        if (isHidden)
            isHidden = false;
        else
            isHidden = true;

        UpdateChildTiles();
    }

    // ************************************************************************************
    // OBJECT EDITING
    // ************************************************************************************
    override public void GetEditableAttributes(LevelEditor levelEditor)
    {
        GUI.Label(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 110, 50, 20), "Width");
        dynamicSizeComponent.BlockWidth = GUI.TextField(new Rect((Screen.width / 2) - 90, (Screen.height / 2) - 110, 100, 20), dynamicSizeComponent.BlockWidth);

        GUI.Label(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 80, 50, 20), "Height");
        dynamicSizeComponent.BlockHeight = GUI.TextField(new Rect((Screen.width / 2) - 90, (Screen.height / 2) - 80, 100, 20), dynamicSizeComponent.BlockHeight);

        IsHidden = GUI.Toggle(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 50, 150, 20), IsHidden, "Is hidden?");
    }
}
