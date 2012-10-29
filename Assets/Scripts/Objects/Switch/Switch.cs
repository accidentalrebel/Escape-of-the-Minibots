using UnityEngine;
using System.Collections;

public class Switch : LevelObject {

    public Vector3 posOfObjectToActivate1 = Vector3.zero;
    public Vector3 posOfObjectToActivate2 = Vector3.zero;
    public Vector3 posOfObjectToActivate3 = Vector3.zero;
    protected bool isTriggered = false;
    protected Collider triggeredCollider;
	protected Map map;

	// Use this for initialization
	override protected void Awake () {
		base.Awake();
		
		map = Registry.map;
	}

    internal void Initialize(Vector3 theStartingPos, Vector2 thePosOfObjectToActivate1)
    {
        base.Initialize(theStartingPos);
        posOfObjectToActivate1 =
            new Vector3(thePosOfObjectToActivate1.x
                , thePosOfObjectToActivate1.y, 0);       
    }

    internal void Initialize(Vector3 theStartingPos, Vector2 thePosOfObjectToActivate1
        , Vector2 thePosOfObjectToActivate2)
    {
        base.Initialize(theStartingPos);
        posOfObjectToActivate1 =
            new Vector3(thePosOfObjectToActivate1.x
                , thePosOfObjectToActivate1.y, 0);
        posOfObjectToActivate2 =
            new Vector3(thePosOfObjectToActivate2.x
                , thePosOfObjectToActivate1.y, 0);       
    }
	
	internal void Initialize(Vector3 theStartingPos, Vector2 thePosOfObjectToActivate1
        , Vector2 thePosOfObjectToActivate2, Vector2 thePosOfObjectToActivate3 )
	{
		base.Initialize(theStartingPos);
        posOfObjectToActivate1 = 
            new Vector3(thePosOfObjectToActivate1.x
                , thePosOfObjectToActivate1.y, 0);
        posOfObjectToActivate2 =
            new Vector3(thePosOfObjectToActivate2.x
                , thePosOfObjectToActivate1.y, 0);
        posOfObjectToActivate3 =
            new Vector3(thePosOfObjectToActivate3.x
                , thePosOfObjectToActivate1.y, 0);
	}

    internal void SetObjectToActivate(LevelObject theObject)
    {
        posOfObjectToActivate1 = 
            new Vector3(theObject.gameObject.transform.position.x
                , theObject.gameObject.transform.position.y, 0);
    }

    //internal void SetObjectToActivate(Vector2 posOfObjectToActivate)
    //{
    //    objectToActivate = map.GetLevelObjectAtPosition
    //        (new Vector3(theObject.gameObject.transform.position.x
    //            , theObject.gameObject.transform.position.y, 0));
    //}
	
	// Update is called once per frame
	void Update () {
        if (isTriggered)
        {
            if (Input.GetKeyUp(KeyCode.X))
            {
                LevelObject objectToUse = map.GetLevelObjectAtPosition(posOfObjectToActivate1);
                objectToUse.Use();
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

    internal override void ResetObject()
    {
        base.ResetObject();
    }

    // ************************************************************************************
    // OBJECT EDITING
    // ************************************************************************************
    internal override void GetEditableAttributes(LevelEditor levelEditor)
    {
        string toDisplay;
        if (posOfObjectToActivate1 != Vector3.zero)
            toDisplay = "Linked";
        else
            toDisplay = "No Link";
        GUI.Label(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 110, 100, 20), toDisplay);

        if (GUI.Button(new Rect((Screen.width / 2) - 30, (Screen.height / 2) - 110, 100, 20), "Link to Object"))
        {
            Debug.Log("Linking to object");
            levelEditor.CurrentMode = LevelEditor.LevelEditorMode.PickToLinkMode;
        }        
    }
}
