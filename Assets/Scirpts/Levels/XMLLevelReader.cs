using UnityEngine;
using System.Collections;
using System.Xml;

public class XMLLevelReader : XMLAccessor {
    
    public string levelToLoad = "1";    

	// Use this for initialization
	void Start () {
		pfTile = Resources.Load(@"Prefabs/pfTile");
        if (pfTile == null)
            Debug.LogError("Can not find pfTile prefab!");
        pfMinibot = Resources.Load(@"Prefabs/pfMinibot");
        if (pfMinibot == null)
            Debug.LogError("Can not find pfMinibot prefab!");
		pfBox = Resources.Load (@"Prefabs/pfBox");
		if (pfBox == null)
			Debug.LogError("Can not find pfBox prefab!");
		pfDoor = Resources.Load (@"Prefabs/pfDoor");
		if (pfDoor == null)
			Debug.LogError("Can not find pfDoor prefab!");
		pfGravityInverter = Resources.Load(@"Prefabs/pfGravityInverter");
		if (pfGravityInverter == null )
			Debug.LogError("Can not find pfGravityInverter prefab!");
		pfHazard = Resources.Load (@"Prefabs/pfHazard");
		if ( pfHazard == null )
			Debug.LogError("Can not find pfHazard prefab!");
		pfHorizontalInverter = Resources.Load (@"Prefabs/pfHorizontalInverter");
		if ( pfHorizontalInverter == null )
			Debug.LogError("Can not find pfHorizontalInverter prefab!");
		pfMovingPlatform = Resources.Load ( @"Prefabs/pfMovingPlatform");
		if ( pfMovingPlatform == null )
			Debug.LogError("Can not find pfMovingPlatform prefab!");
		pfStepSwitch = Resources.Load( @"Prefabs/pfStepSwitch");
		if (pfStepSwitch == null )
			Debug.LogError("Can not find pfStepSwitch prefab!");
		pfSwitch = Resources.Load( @"Prefabs/pfSwitch");
		if (pfSwitch == null )
			Debug.LogError("Can not find pfSwitch prefab!");
		pfTriggerableBlock = Resources.Load (@"Prefabs/pfTriggerableBlock");
		if ( pfTriggerableBlock == null )
			Debug.LogError("Can not find pfTriggerableBlock prefab!");	
		
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
                newObject.transform.position
                    = new Vector3(float.Parse(reader.GetAttribute("x"))
                        , Mathf.Ceil(float.Parse(reader.GetAttribute("y"))), 0);
                
                RigidBodyFPSController controller = newObject.GetComponentInChildren<RigidBodyFPSController>();
                controller.InvertGravity = StringToBool(reader.GetAttribute("invertGravity"));
                controller.invertHorizontal = StringToBool(reader.GetAttribute("invertHorizontal"));
				
				newObject.transform.parent = minibotsContainer.transform;
            }
            else if (reader.IsStartElement("tile"))
            {
                newObject = (GameObject)Instantiate(pfTile);
                newObject.transform.position
                    = new Vector3(float.Parse(reader.GetAttribute("x"))
                        , float.Parse(reader.GetAttribute("y"), 0));
				
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
                newObject.transform.position
                    = new Vector3(float.Parse(reader.GetAttribute("x"))
                        , float.Parse(reader.GetAttribute("y"), 0));
				
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
					, float.Parse(reader.GetAttribute("y")), 0));
				
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
