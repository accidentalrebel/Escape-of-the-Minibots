using UnityEngine;
using System.Collections;

[RequireComponent(typeof(FileReader))]
public class LevelLoader : MonoBehaviour {

    public enum TileType { None = '0', Wall = '1' };

    private char[,] mapArray;

    private Object pfTile;

	// Use this for initialization
	void Start () {   
        pfTile = Resources.Load(@"Prefabs/pfTile");
        if (pfTile == null)
            Debug.LogError("Can not find pfTile prefab!");

        BuildLevel(GetLevelData("1"));
	}

    private string GetLevelData(string levelLabel)
    {
        return FileReader.GetData(Application.dataPath + @"\Levels\" + levelLabel + ".txt");
    }

    private void BuildLevel(string levelData)
    {
        Vector2 levelDimension = GetLevelDimensions(levelData);
        mapArray = GetMapArray(levelData, levelDimension);

        for (int yCoord = 0; yCoord < (int)levelDimension.y; yCoord++)
        {
            for (int xCoord = 0; xCoord < (int)levelDimension.x; xCoord++)
            {
                if ( mapArray[xCoord, yCoord] == (char)TileType.Wall)
                {
                    GameObject newTile = (GameObject)Instantiate(pfTile);
                    newTile.transform.position
                        = new Vector3(xCoord, levelDimension.y-yCoord, 0);
                }
            }
        }
    }

    private char[,] GetMapArray(string levelData, Vector2 levelDimension)
    {
        char[,] mapArray = new char[(int)levelDimension.x, (int)levelDimension.y];

        int i = 0;
        for (int yCoord = 0; yCoord < (int)levelDimension.y; yCoord++)
        {
            for (int xCoord = 0; xCoord < (int)levelDimension.x; xCoord++)
            {                
                mapArray[xCoord, yCoord] = levelData[i];
                i++;
            }

            i += 2;
        }

        return mapArray;
    }

    private Vector2 GetLevelDimensions(string levelData)
    {
        float width = 0, height = 1;
        bool hasGottenWidth = false;

        for ( int i = 0 ; i < levelData.Length ; i++ )
        {
            if (levelData[i] == '\r')
            {
                height++;
                i += 2;
                hasGottenWidth = true;
            }
            else
            {
                if (!hasGottenWidth)
                    width++;
            }
        }        
        
        return new Vector2(width, height);
    }	
}
