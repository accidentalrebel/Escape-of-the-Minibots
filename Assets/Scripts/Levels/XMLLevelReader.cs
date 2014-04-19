using UnityEngine;
using System.Collections;
using System.Xml;

public class XMLLevelReader : XMLAccessor {

    public string levelToLoad = "1";
    PrefabHandler prefabHandler;

	override protected void Awake () 
    {        
        prefabHandler = Registry.prefabHandler;
        base.Awake();
	}   

    public bool LoadLevel(string theLevelToLoad)
    {
        string filepath = Application.dataPath + @"/Resources/Levels/" + theLevelToLoad + ".xml";

        // If file exists, continue reading file
        XmlReader reader = XmlReader.Create(filepath);
		
		GameObject newObject;

        while (reader.Read())
        {
            if (reader.IsStartElement("minibot"))
            {
                newObject = (GameObject)Instantiate(prefabHandler.pfMinibot);
                newObject.GetComponent<Minibot>().Initialize(
                    new Vector3(float.Parse(reader.GetAttribute("x"))
                        , Mathf.Ceil(float.Parse(reader.GetAttribute("y"))), 0)
                        , StringToBool(reader.GetAttribute("invertGravity"))
                        , StringToBool(reader.GetAttribute("invertHorizontal")));
				
				newObject.transform.parent = minibotsContainer.transform;
            }
            else if (reader.IsStartElement("tile"))
            {
                newObject = (GameObject)Instantiate(prefabHandler.pfTile);
                newObject.GetComponent<Tile>().Initialize
                    (new Vector3
                        (float.Parse(reader.GetAttribute("x"))
                        , float.Parse(reader.GetAttribute("y")), 0));
				
				newObject.transform.parent = tilesContainer.transform;
            }
			else if (reader.IsStartElement("box"))
			{
                newObject = (GameObject)Instantiate(prefabHandler.pfBox);
				newObject.GetComponent<Box>().Initialize(new Vector3
					( float.Parse(reader.GetAttribute("x"))
					, float.Parse(reader.GetAttribute("y")), 0));
				
				newObject.transform.parent = boxesContainer.transform;
			}
			else if (reader.IsStartElement("door"))
			{
                newObject = (GameObject)Instantiate(prefabHandler.pfDoor);
				newObject.GetComponent<Door>().Initialize(new Vector3
					( float.Parse(reader.GetAttribute("x"))
					, float.Parse(reader.GetAttribute("y")), 0)
					, StringToBool(reader.GetAttribute("isOpen")));
				
				newObject.transform.parent = doorsContainer.transform;
			}
			else if (reader.IsStartElement("gravityInverter"))
			{
                newObject = (GameObject)Instantiate(prefabHandler.pfGravityInverter);
				newObject.GetComponent<GravitySwitch>().Initialize(new Vector3
					( float.Parse(reader.GetAttribute("x"))
					, float.Parse(reader.GetAttribute("y")), 0));
				
				newObject.transform.parent = gravityInvertersContainer.transform;
			}
			else if (reader.IsStartElement("hazard"))
			{
                newObject = (GameObject)Instantiate(prefabHandler.pfHazard);
                newObject.GetComponent<HazardTile>().Initialize
                   (new Vector3
                       (float.Parse(reader.GetAttribute("x"))
                       , float.Parse(reader.GetAttribute("y")), 0));
				
				newObject.transform.parent = hazardsContainer.transform;
			}
			else if (reader.IsStartElement("horizontalInverter"))
			{
                newObject = (GameObject)Instantiate(prefabHandler.pfHorizontalInverter);
				newObject.GetComponent<HorizontalSwitch>().Initialize(new Vector3
					( float.Parse(reader.GetAttribute("x"))
					, float.Parse(reader.GetAttribute("y")), 0));
				
				newObject.transform.parent = horizontalInvertersContainer.transform;
			}			
			else if (reader.IsStartElement("triggerableBlock"))
			{
                newObject = (GameObject)Instantiate(prefabHandler.pfTriggerableBlock);
				newObject.GetComponent<TriggerableBlocks>().Initialize(new Vector3
					( float.Parse(reader.GetAttribute("x"))
					, float.Parse(reader.GetAttribute("y")), 0)
                    , StringToBool(reader.GetAttribute("isHidden"))
                    , new Vector2(
                        float.Parse(reader.GetAttribute("width"))
                        , float.Parse(reader.GetAttribute("height")))
                    );
				
				newObject.transform.parent = triggerableBlocksContainer.transform;
			}
            else if (reader.IsStartElement("triggerableHazard"))
            {
                newObject = (GameObject)Instantiate(prefabHandler.pfTriggerableHazard);
                newObject.GetComponent<TriggerableHazard>().Initialize(new Vector3
                    (float.Parse(reader.GetAttribute("x"))
                    , float.Parse(reader.GetAttribute("y")), 0)
                    , StringToBool(reader.GetAttribute("isHidden"))
                    , new Vector2(
                        float.Parse(reader.GetAttribute("width"))
                        , float.Parse(reader.GetAttribute("height")))
                    );

                newObject.transform.parent = triggerableHazardsContainer.transform;
            }
			else if (reader.IsStartElement("stepSwitch"))
			{
                newObject = (GameObject)Instantiate(prefabHandler.pfStepSwitch);

                string xObjectToActivate1 = reader.GetAttribute("xPosOfObjectToActivate");
                string yObjectToActivate1 = reader.GetAttribute("yPosOfObjectToActivate");                  
                
                string xObjectToActivate2 = reader.GetAttribute("xPosOfObjectToActivate2");
                string yObjectToActivate2 = reader.GetAttribute("yPosOfObjectToActivate2");
                
                string xObjectToActivate3 = reader.GetAttribute("xPosOfObjectToActivate3");
                string yObjectToActivate3 = reader.GetAttribute("yPosOfObjectToActivate3");

                // If we only have one objectToActivate
                if ( xObjectToActivate2 == null && yObjectToActivate2 == null)
                {
                    newObject.GetComponent<StepSwitch>().Initialize(new Vector3
                    (float.Parse(reader.GetAttribute("x"))
                    , float.Parse(reader.GetAttribute("y")), 0)
                    , new Vector2
                    (float.Parse(xObjectToActivate1)
                    , float.Parse(yObjectToActivate1))
                    );
                }
                // If we have two objects to ativate
                else if (xObjectToActivate3 == null && yObjectToActivate3 == null)
                {
                    newObject.GetComponent<StepSwitch>().Initialize(new Vector3
                    (float.Parse(reader.GetAttribute("x"))
                    , float.Parse(reader.GetAttribute("y")), 0)
                    , new Vector2
                    (float.Parse(xObjectToActivate1)
                    , float.Parse(yObjectToActivate1))
                    , new Vector2
                    (float.Parse(xObjectToActivate2)
                    , float.Parse(yObjectToActivate2))
                    );
                }
                // If we have three objects to activate
                else
                {
                    newObject.GetComponent<StepSwitch>().Initialize(new Vector3
                        (float.Parse(reader.GetAttribute("x"))
                        , float.Parse(reader.GetAttribute("y")), 0)
                        , new Vector2
                        (float.Parse(xObjectToActivate1)
                        , float.Parse(yObjectToActivate1))
                        , new Vector2
                        (float.Parse(xObjectToActivate2)
                        , float.Parse(yObjectToActivate2))
                        , new Vector2
                        (float.Parse(xObjectToActivate3)
                        , float.Parse(yObjectToActivate3))
                        );
                }
				
				newObject.transform.parent = stepSwitchesContainer.transform;
			}
			else if (reader.IsStartElement("switch"))
			{
                newObject = (GameObject)Instantiate(prefabHandler.pfSwitch);

                string xObjectToActivate1 = reader.GetAttribute("xPosOfObjectToActivate");
                string yObjectToActivate1 = reader.GetAttribute("yPosOfObjectToActivate");

                string xObjectToActivate2 = reader.GetAttribute("xPosOfObjectToActivate2");
                string yObjectToActivate2 = reader.GetAttribute("yPosOfObjectToActivate2");

                string xObjectToActivate3 = reader.GetAttribute("xPosOfObjectToActivate3");
                string yObjectToActivate3 = reader.GetAttribute("yPosOfObjectToActivate3");

                // If we only have one objectToActivate
                if (xObjectToActivate2 == null && yObjectToActivate2 == null)
                {
                    newObject.GetComponent<Switch>().Initialize(new Vector3
                    ( float.Parse(reader.GetAttribute("x"))
                    , float.Parse(reader.GetAttribute("y")), 0)
                    , new Vector2
                    ( float.Parse(xObjectToActivate1)
                    , float.Parse(yObjectToActivate1))
                    );
                }
                // If we have two objects to ativate
                else if (xObjectToActivate3 == null && yObjectToActivate3 == null)
                {
                    newObject.GetComponent<Switch>().Initialize(new Vector3
                    ( float.Parse(reader.GetAttribute("x"))
                    , float.Parse(reader.GetAttribute("y")), 0)
                    , new Vector2
                    ( float.Parse(xObjectToActivate1)
                    , float.Parse(yObjectToActivate1))
                    , new Vector2
                    ( float.Parse(xObjectToActivate2)
                    , float.Parse(yObjectToActivate2))
                    );
                }
                // If we have three objects to activate
                else
                {
                    newObject.GetComponent<Switch>().Initialize(new Vector3
                        ( float.Parse(reader.GetAttribute("x"))
                        , float.Parse(reader.GetAttribute("y")), 0)
                        , new Vector2
                        ( float.Parse(xObjectToActivate1)
                        , float.Parse(yObjectToActivate1))
                        , new Vector2
                        ( float.Parse(xObjectToActivate2)
                        , float.Parse(yObjectToActivate2))
                        , new Vector2
                        ( float.Parse(xObjectToActivate3)
                        , float.Parse(yObjectToActivate3))
                        );
                }
				
				newObject.transform.parent = switchesContainer.transform;
			}			
        }

		Registry.map.UpdateNeighborsForAllWallTiles();

        // We tell the main that we have finished loading
        HasFinishedLoadingLevel(theLevelToLoad);

        return true;
    }

    private void HasFinishedLoadingLevel(string theCurrentLevel)
    {
        Registry.map.currentLevel = theCurrentLevel;
        Registry.main.levelEditor.LevelFileName = theCurrentLevel;
        Registry.main.GetMinibotsInLevel();
        Registry.main.StartLevel();
    }
}
