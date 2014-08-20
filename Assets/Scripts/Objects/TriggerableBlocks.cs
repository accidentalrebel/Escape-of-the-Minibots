using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerableBlocks : LevelObject {

    public DynamicSizeObject dynamicSizeComponent;

    private bool _isHidden = false;
    private bool _startingIsHidden = false;
    protected Object _prefabToSpawn;

    public bool IsHidden
    {
        get { return _isHidden; }
        set { _isHidden = value; UpdateChildTiles(); }
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

        _prefabToSpawn = Registry.prefabHandler.pfTriggerableTile;        
    }

    override protected void Start()
    {
        
    }
	
	public void Initialize(Vector3 theStartingPos, bool theIsHidden, Vector2 theBlockSize)
	{
		base.Initialize(theStartingPos);

        _isHidden = theIsHidden;
        _startingIsHidden = _isHidden;
        
        dynamicSizeComponent.Initialize(_prefabToSpawn, theBlockSize);
        
        UpdateChildTiles();
	}

    // ************************************************************************************
    // CHILD TILES
    // ************************************************************************************
    private void UpdateChildTiles()
    {
        foreach (GameObject child in dynamicSizeComponent.childTiles)
        {
            if (_isHidden)
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
        _isHidden = _startingIsHidden;
        UpdateChildTiles();  
    }

    // ************************************************************************************
    // USAGE
    // ************************************************************************************
    override public void Use(bool setToValue)
    {
        _isHidden = setToValue;
        UpdateChildTiles();
    }

    override public void Use()
    {
        if (_isHidden)
            _isHidden = false;
        else
            _isHidden = true;

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
		_startingIsHidden = IsHidden;
    }
}
