using UnityEngine;
using System.Collections;

public class Switch : LevelObject {

    public LevelObject objectToActivate;
    protected bool isTriggered = false;
    protected Collider triggeredCollider;
	private Map map;

	// Use this for initialization
	override protected void Awake () {
		base.Awake();
		
		map = Registry.map;
		
        if (objectToActivate == null)
            Debug.LogWarning("objectToActivate is not specified");
	}
	
	internal void Initialize(Vector3 theStartingPos, Vector2 posOfObjectToActivate)
	{
		base.Initialize(theStartingPos);		
		
		LevelObject theObject = map.GetLevelObjectAtPosition
			(new Vector3(posOfObjectToActivate.x, posOfObjectToActivate.y, 0));
		if ( theObject != null )
			objectToActivate = theObject;
		else
			Debug.LogError("There was no object at position specified!");
	}
	
	// Update is called once per frame
	void Update () {
        if (isTriggered)
        {
            if (Input.GetKeyUp(KeyCode.X))
            {
                objectToActivate.Use();
            }
        }
	}

    void OnTriggerEnter(Collider col)
    {
        triggeredCollider = col;
        isTriggered = true;
    }

    void OnTriggerExit()
    {
        triggeredCollider = null;
        isTriggered = false;
    }

    // ************************************************************************************
    // OBJECT EDITING
    // ************************************************************************************
    internal override void GetEditableAttributes(LevelEditor levelEditor)
    {
        GUI.Label(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 110, 100, 20), objectToActivate.name);
        if (GUI.Button(new Rect((Screen.width / 2) - 30, (Screen.height / 2) - 110, 100, 20), "Link to Object"))
        {
            Debug.Log("Linking to object");
            levelEditor.CurrentMode = LevelEditor.LevelEditorMode.PickToLinkMode;
        }        
    }
}
