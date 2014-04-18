using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicSizeObject : MonoBehaviour {

    public Vector2 blockSize = new Vector2(1,1);
    Object objectToGenerate;
	public List<GameObject> childTiles = new List<GameObject>();

    string blockWidth = "1";
	public string BlockWidth
    {
        get { return blockWidth; }
        set { blockWidth = value; UpdateBlockSizeFromString(blockWidth, blockHeight); }
    }

    string blockHeight = "1";
	public string BlockHeight
    {
        get { return blockHeight; }
        set { blockHeight = value; UpdateBlockSizeFromString(blockWidth, blockHeight); }
    }

    // ************************************************************************************
    // INITIALIZATION
    // ************************************************************************************
    public void Initialize(Object theObjectToGenerate)
    {
        objectToGenerate = theObjectToGenerate;
        GenerateTiles();
    }

	public void Initialize(Object theObjectToGenerate, Vector2 theBlockSize)
    {
        objectToGenerate = theObjectToGenerate;
        blockSize = theBlockSize;
        GenerateTiles();
    }
    
    private void UpdateBlockSizeFromString(string theBlockWidth, string theBlockHeight)
    {
        float floatBlockWidth = 0, floatBlockHeight = 0;
        float.TryParse(theBlockWidth, out floatBlockWidth);
        float.TryParse(theBlockHeight, out floatBlockHeight);

        // The following makes sure that the value is valid and remains valid
        if (floatBlockWidth <= 0)
        {
            floatBlockWidth = 1;
            blockWidth = "1";
        }
        else if (floatBlockWidth > 20)
        {
            floatBlockWidth = 20;
            blockWidth = "20";
        }

        if (floatBlockHeight <= 0)
        {
            floatBlockHeight = 1;
            blockHeight = "1";
        }
        else if (floatBlockHeight > 20)
        {
            floatBlockHeight = 20;
            blockHeight = "20";
        }

        blockSize = new Vector2(floatBlockWidth, floatBlockHeight);
        UpdateBlockSize(blockSize);
    }

    // ************************************************************************************
    // MEMBER METHODS
    // ************************************************************************************
    private void GenerateTiles()
    {
        DestroyChildTiles();
        childTiles.Clear();

        for (int yCoord = 0; yCoord < blockSize.y; yCoord++)
        {
            for (int xCoord = 0; xCoord < blockSize.x; xCoord++)
            {
                GameObject newTile
                    = (GameObject)Instantiate(objectToGenerate);
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
}
