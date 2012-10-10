using UnityEngine;
using System.Collections;
using System.Xml;

public class XMLLevelReader : XMLAccessor {

    protected Object pfTile;
    protected Object pfMinibot;
    protected Object pfBox;
    protected Object pfDoor;
    protected Object pfGravityInverter;
    protected Object pfHazard;
    protected Object pfHorizontalInverter;
    protected Object pfMovingPlatform;
    protected Object pfStepSwitch;
    protected Object pfSwitch;
    protected Object pfTriggerableBlock;

    public string levelToLoad = "1";    

	// Use this for initialization
	void Start () {

        PrefabHandler prefabHandler = Registry.prefabHandler;

        pfTile = prefabHandler.pfTile;
        pfMinibot = prefabHandler.pfMinibot;
        pfBox = prefabHandler.pfBox;
        pfDoor = prefabHandler.pfDoor;
        pfGravityInverter = prefabHandler.pfGravityInverter;
        pfHazard = prefabHandler.pfHazard;
        pfHorizontalInverter = prefabHandler.pfHorizontalInverter;
        pfMovingPlatform = prefabHandler.pfMovingPlatform;
        pfStepSwitch = prefabHandler.pfStepSwitch;
        pfSwitch = prefabHandler.pfSwitch;
        pfTriggerableBlock = prefabHandler.pfTriggerableBlock;

        if (levelToLoad != "")
        {
            string filepath = Application.dataPath + @"/Levels/" + levelToLoad + ".xml";
            if (CheckIfFileExists(filepath))
                LoadLevel(filepath);
        }
		else
		{
			Debug.LogWarning("No levelToLoad is specified");	
		}
	}   

    void LoadLevel(string filepath)
    {
        XmlReader reader = XmlReader.Create(filepath);
		
		GameObject newObject;

        while (reader.Read())
        {
            if (reader.IsStartElement("minibot"))
            {
                newObject = (GameObject)Instantiate(pfMinibot);
                newObject.GetComponent<Minibot>().Initialize(
                    new Vector3(float.Parse(reader.GetAttribute("x"))
                        , Mathf.Ceil(float.Parse(reader.GetAttribute("y"))), 0)
                        , StringToBool(reader.GetAttribute("invertGravity"))
                        , StringToBool(reader.GetAttribute("invertHorizontal")));
				
				newObject.transform.parent = minibotsContainer.transform;
            }
            else if (reader.IsStartElement("tile"))
            {
                newObject = (GameObject)Instantiate(pfTile);
                newObject.GetComponent<Tile>().Initialize
                    (new Vector3
                        (float.Parse(reader.GetAttribute("x"))
                        , float.Parse(reader.GetAttribute("y")), 0));
				
				newObject.transform.parent = tilesContainer.transform;
            }
			else if (reader.IsStartElement("box"))
			{
				newObject = (GameObject)Instantiate(pfBox);
				newObject.GetComponent<Box>().Initialize(new Vector3
					( float.Parse(reader.GetAttribute("x"))
					, float.Parse(reader.GetAttribute("y")), 0));
				
				newObject.transform.parent = boxesContainer.transform;
			}
			else if (reader.IsStartElement("door"))
			{
				newObject = (GameObject)Instantiate(pfDoor);
				newObject.GetComponent<Door>().Initialize(new Vector3
					( float.Parse(reader.GetAttribute("x"))
					, float.Parse(reader.GetAttribute("y")), 0)
					, StringToBool(reader.GetAttribute("isOpen")));
				
				newObject.transform.parent = doorsContainer.transform;
			}
			else if (reader.IsStartElement("gravityInverter"))
			{
				newObject = (GameObject)Instantiate(pfGravityInverter);
				newObject.GetComponent<GravitySwitch>().Initialize(new Vector3
					( float.Parse(reader.GetAttribute("x"))
					, float.Parse(reader.GetAttribute("y")), 0));
				
				newObject.transform.parent = gravityInvertersContainer.transform;
			}
			else if (reader.IsStartElement("hazard"))
			{
				newObject = (GameObject)Instantiate(pfHazard);
                newObject.GetComponent<HazardTile>().Initialize
                   (new Vector3
                       (float.Parse(reader.GetAttribute("x"))
                       , float.Parse(reader.GetAttribute("y")), 0));
				
				newObject.transform.parent = hazardsContainer.transform;
			}
			else if (reader.IsStartElement("horizontalInverter"))
			{
				newObject = (GameObject)Instantiate(pfHorizontalInverter);
				newObject.GetComponent<HorizontalSwitch>().Initialize(new Vector3
					( float.Parse(reader.GetAttribute("x"))
					, float.Parse(reader.GetAttribute("y")), 0));
				
				newObject.transform.parent = horizontalInvertersContainer.transform;
			}			
			else if (reader.IsStartElement("triggerableBlock"))
			{
				newObject = (GameObject)Instantiate(pfTriggerableBlock);
				newObject.GetComponent<TriggerableBlocks>().Initialize(new Vector3
					( float.Parse(reader.GetAttribute("x"))
					, float.Parse(reader.GetAttribute("y")), 0)
                    , StringToBool(reader.GetAttribute("isHidden")));
				
				newObject.transform.parent = triggerableBlocksContainer.transform;
			}
			else if (reader.IsStartElement("stepSwitch"))
			{
				newObject = (GameObject)Instantiate(pfStepSwitch);
				newObject.GetComponent<StepSwitch>().Initialize(new Vector3
					( float.Parse(reader.GetAttribute("x"))
					, float.Parse(reader.GetAttribute("y")), 0)					
					, new Vector2
					( float.Parse(reader.GetAttribute("xPosOfObjectToActivate"))
					, float.Parse(reader.GetAttribute("yPosOfObjectToActivate"))
					));
				
				newObject.transform.parent = stepSwitchesContainer.transform;
			}
			else if (reader.IsStartElement("switch"))
			{
				newObject = (GameObject)Instantiate(pfSwitch);
				newObject.GetComponent<Switch>().Initialize(new Vector3
					( float.Parse(reader.GetAttribute("x"))
					, float.Parse(reader.GetAttribute("y")), 0)					
					, new Vector2
					( float.Parse(reader.GetAttribute("xPosOfObjectToActivate"))
					, float.Parse(reader.GetAttribute("yPosOfObjectToActivate"))
					));
				
				newObject.transform.parent = switchesContainer.transform;
			}			
        }
    }
}
