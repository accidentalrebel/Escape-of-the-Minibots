using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class TriggerableBlocks : MonoBehaviour {

    public Vector2 blockSize;

	// Use this for initialization
	void Start () {
        GenerateTiles();
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
}
