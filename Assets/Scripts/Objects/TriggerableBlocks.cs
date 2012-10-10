using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class TriggerableBlocks : LevelObject {

    public Vector2 blockSize;
    public bool isHidden = false;
    private bool startingIsHidden = false;

	// Use this for initialization
	void Start () {
        GenerateTiles();
        SetStatusOfChildTiles();
	}
	
	internal void Initialize(Vector3 theStartingPos, bool theIsHidden)
	{
		base.Initialize(theStartingPos);
        isHidden = theIsHidden;
        startingIsHidden = isHidden;
	}

    private void GenerateTiles()
    {
        for (int yCoord = 0; yCoord < blockSize.y; yCoord++)
        {
            for (int xCoord = 0; xCoord < blockSize.x; xCoord++)
            {
                // We just skip to the next xCoord as we already have a block present
                if (yCoord == 0 && xCoord == 0)
                    xCoord++;

                GameObject newTile 
                    = (GameObject)Instantiate(Resources.Load(@"Prefabs/pfTriggerableTile"));
                newTile.transform.position
                    = new Vector3(transform.position.x + xCoord
                        , transform.position.y + yCoord, 0);
                newTile.transform.parent = this.transform;
            }
        }
    }

    override internal void Use()
    {
        if (isHidden)
            isHidden = false;
        else
            isHidden = true;

        SetStatusOfChildTiles();  
    }

    private void SetStatusOfChildTiles()
    {
        foreach (Transform child in transform)
        {
            if (isHidden)
            {
                child.gameObject.collider.enabled = false;
                child.gameObject.GetComponent<GraphicHandler>().theRenderer.enabled = false;
            }
            else
            {
                child.gameObject.collider.enabled = true;
                child.gameObject.GetComponent<GraphicHandler>().theRenderer.enabled = true;
            }
        }      
    }

    void OnDrawGizmos()
    {
        for (int yCoord = 0; yCoord < blockSize.y; yCoord++)
        {
            for (int xCoord = 0; xCoord < blockSize.x; xCoord++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(new Vector3(transform.position.x + xCoord
                        , transform.position.y + yCoord, 1), new Vector3(1, 1, 1));
            }
        }
    }

    internal override void ResetObject()
    {
        isHidden = startingIsHidden;
        SetStatusOfChildTiles();  
    }
}
