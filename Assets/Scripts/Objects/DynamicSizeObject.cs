using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicSizeObject : MonoBehaviour {

    public Vector2 blockSize = new Vector2(1,1);
    private List<GameObject> childTiles = new List<GameObject>();

    internal void Initialize(Object theObjectToGenerate)
    {
        GenerateTiles(theObjectToGenerate);
    }

    private void GenerateTiles(Object objectToGenerate)
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
                    = (GameObject)Instantiate(objectToGenerate);
                newTile.transform.position
                    = new Vector3(transform.position.x + xCoord
                        , transform.position.y + yCoord, 0);
                newTile.transform.parent = this.transform;
                childTiles.Add(newTile);
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
}
