using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[ExecuteInEditMode]
public class TriggerableBlocks : LevelObject {

    public Vector2 blockSize;    
    private bool startingIsHidden = false;
    private List<GameObject> childTiles = new List<GameObject>();

    public bool isHidden = false;
    bool IsHidden
    {
        get { return isHidden; }
        set { isHidden = value; UpdateChildTiles(); }
    }

    string blockWidth = "1";
    string BlockWidth
    {
        get { return blockWidth; }
        set { blockWidth = value; UpdateBlockSizeFromString(blockWidth, blockHeight); }
    }

    string blockHeight = "1";
    string BlockHeight
    {
        get { return blockHeight; }
        set { blockHeight = value; UpdateBlockSizeFromString(blockWidth, blockHeight); }
    }

    private void UpdateBlockSizeFromString(string theBlockWidth, string theBlockHeight)
    {
        
        float floatBlockWidth = 0, floatBlockHeight = 0;
        float.TryParse(theBlockWidth, out floatBlockWidth);
        float.TryParse(theBlockHeight, out floatBlockHeight);

        blockSize = new Vector2(floatBlockWidth, floatBlockHeight);
        UpdateBlockSize(blockSize);
    }    

	// Use this for initialization
	protected new void Start () {
        GenerateTiles();
        UpdateChildTiles();
	}
	
	internal void Initialize(Vector3 theStartingPos, bool theIsHidden, Vector2 theBlockSize)
	{
		base.Initialize(theStartingPos);

        isHidden = theIsHidden;
        startingIsHidden = isHidden;
        UpdateBlockSize(theBlockSize);
        
        UpdateChildTiles();
	}

    private void GenerateTiles()
    {
        DestroyChildTiles();

        childTiles.Clear();
        for (int yCoord = 0; yCoord < blockSize.y; yCoord++)
        {
            for (int xCoord = 0; xCoord < blockSize.x; xCoord++)
            {
                // We just skip to the next xCoord as we already have a block present
                //if (yCoord == 0 && xCoord == 0)
                //    xCoord++;

                GameObject newTile 
                    = (GameObject)Instantiate(Resources.Load(@"Prefabs/pfTriggerableTile"));
                newTile.transform.position
                    = new Vector3(transform.position.x + xCoord
                        , transform.position.y + yCoord, 0);
                newTile.transform.parent = this.transform;
                childTiles.Add(newTile);
            }
        }
    }

    private void UpdateBlockSize(Vector2 newBlockSize)
    {
        blockSize = newBlockSize;

        DestroyChildTiles();
        GenerateTiles();
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
        foreach (GameObject child in childTiles)
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

    /// <summary>
    /// Destroys all child tiles
    /// </summary>
    private void DestroyChildTiles()
    {
        foreach (GameObject child in childTiles)
        {
            GameObject.Destroy(child);
        }
    }

    override internal void ResetObject()
    {
        isHidden = startingIsHidden;
        UpdateChildTiles();  
    }

    //void OnDrawGizmos()
    //{
    //    for (int yCoord = 0; yCoord < blockSize.y; yCoord++)
    //    {
    //        for (int xCoord = 0; xCoord < blockSize.x; xCoord++)
    //        {
    //            Gizmos.color = Color.blue;
    //            Gizmos.DrawWireCube(new Vector3(transform.position.x + xCoord
    //                    , transform.position.y + yCoord, 1), new Vector3(1, 1, 1));
    //        }
    //    }
    //}

    // ************************************************************************************
    // OBJECT EDITING
    // ************************************************************************************
    internal override void GetEditableAttributes(LevelEditor levelEditor)
    {
        GUI.Label(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 110, 50, 20), "Width");
        BlockWidth = GUI.TextField(new Rect((Screen.width / 2) - 90, (Screen.height / 2) - 110, 100, 20), BlockWidth);

        GUI.Label(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 80, 50, 20), "Height");
        BlockHeight = GUI.TextField(new Rect((Screen.width / 2) - 90, (Screen.height / 2) - 80, 100, 20), BlockHeight);

        IsHidden = GUI.Toggle(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 50, 150, 20), IsHidden, "Is hidden?");
    }
}
