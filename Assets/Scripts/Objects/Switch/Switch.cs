using UnityEngine;
using System.Collections;

public class Switch : LevelObject {

    public Vector3 posOfObjectToActivate1 = Vector3.zero;
    public Vector3 posOfObjectToActivate2 = Vector3.zero;
    public Vector3 posOfObjectToActivate3 = Vector3.zero;

	public Texture triggeredTexture;
	public Texture untriggeredTexture;

    int objectNumToLinkTo = 0;
    protected bool isTriggered = false;
    protected Collider triggeredCollider;
	protected Map map;

	// Use this for initialization
	override protected void Awake () {
		base.Awake();
		
		map = Registry.map;

		if ( triggeredTexture == null )
			Debug.LogWarning("triggeredTexture not initialized!");
		if ( untriggeredTexture == null )
			Debug.LogWarning("untriggeredTexture not initialized!");
	}

	public void Initialize(Vector3 theStartingPos, Vector2 thePosOfObjectToActivate1)
    {
        base.Initialize(theStartingPos);
        posOfObjectToActivate1 =
            new Vector3(thePosOfObjectToActivate1.x
                , thePosOfObjectToActivate1.y, 0);       
    }

    public void Initialize(Vector3 theStartingPos, Vector2 thePosOfObjectToActivate1
        , Vector2 thePosOfObjectToActivate2)
    {
        base.Initialize(theStartingPos);
        posOfObjectToActivate1 = new Vector3(thePosOfObjectToActivate1.x, thePosOfObjectToActivate1.y, 0);
        posOfObjectToActivate2 = new Vector3(thePosOfObjectToActivate2.x, thePosOfObjectToActivate2.y, 0);
    }
	
	public void Initialize(Vector3 theStartingPos, Vector2 thePosOfObjectToActivate1
        , Vector2 thePosOfObjectToActivate2, Vector2 thePosOfObjectToActivate3 )
	{
		base.Initialize(theStartingPos);
        posOfObjectToActivate1 = new Vector3(thePosOfObjectToActivate1.x, thePosOfObjectToActivate1.y, 0);
        posOfObjectToActivate2 = new Vector3(thePosOfObjectToActivate2.x, thePosOfObjectToActivate2.y, 0);
        posOfObjectToActivate3 = new Vector3(thePosOfObjectToActivate3.x, thePosOfObjectToActivate3.y, 0);
	}

    public void SetObjectToActivate(LevelObject theObject)
    {
        Vector3 thePosition = new Vector3(theObject.gameObject.transform.position.x
                    , theObject.gameObject.transform.position.y, 0);;

        if (objectNumToLinkTo == 1)
            posOfObjectToActivate1 = thePosition;                
        else if (objectNumToLinkTo == 2)
            posOfObjectToActivate2 = thePosition;
        else if (objectNumToLinkTo == 3)
            posOfObjectToActivate3 = thePosition;

        Debug.LogWarning(posOfObjectToActivate1 + ", " + posOfObjectToActivate2 + ", " + posOfObjectToActivate3);

        objectNumToLinkTo = 0;
    }

	void LateUpdate () 
    {
        if (isTriggered)
        {
            if (Registry.inputHandler.UseButton)
            {
				Use();
            }
        }
	}

	public override void Use ()
	{
		UpdateSwitchGraphic();
		
		LevelObject objectToUse;
		if (posOfObjectToActivate1 != Vector3.zero)
		{
			objectToUse = map.GetLevelObjectAtPosition(posOfObjectToActivate1);
			objectToUse.Use();
		}
		if (posOfObjectToActivate2 != Vector3.zero)
		{
			objectToUse = map.GetLevelObjectAtPosition(posOfObjectToActivate2);
			objectToUse.Use();
		}
		if (posOfObjectToActivate3 != Vector3.zero)
		{
			objectToUse = map.GetLevelObjectAtPosition(posOfObjectToActivate3);
			objectToUse.Use();
		}
	}

    void OnTriggerEnter(Collider col)
    {
		if (col.tag == "Player") {
	        triggeredCollider = col;
	        isTriggered = true;
		}
    }

    void OnTriggerExit()
    {
        triggeredCollider = null;
        isTriggered = false;

		UpdateSwitchGraphic();
    }

    override public void ResetObject()
    {
        base.ResetObject();
    }

	virtual protected void UpdateSwitchGraphic ()
	{
		if ( isTriggered )
			graphicHandler.theRenderer.material.SetTexture("_MainTex", triggeredTexture);
		else
			graphicHandler.theRenderer.material.SetTexture("_MainTex", untriggeredTexture);
	}

    // ************************************************************************************
    // OBJECT EDITING
    // ************************************************************************************
    override public void GetEditableAttributes(LevelEditor levelEditor)
    {       
        HandleLinkGUI(posOfObjectToActivate1, 1, levelEditor);
        HandleLinkGUI(posOfObjectToActivate2, 2, levelEditor);
        HandleLinkGUI(posOfObjectToActivate3, 3, levelEditor);        
    }

    private void HandleLinkGUI(Vector3 posOfObjectToActivate, int objectLinkNumber, LevelEditor levelEditor)
    {
        string toDisplay;

        if (posOfObjectToActivate != Vector3.zero)
            toDisplay = "Linked";
        else
            toDisplay = "No Link";       

        GUI.Label(new Rect((Screen.width / 2) - 140, (Screen.height / 2) - 110 + (30 * (objectLinkNumber - 1)), 100, 20), toDisplay);

        if (GUI.Button(new Rect((Screen.width / 2) - 30, (Screen.height / 2) - 110 + (30 * (objectLinkNumber - 1)), 100, 20), "Link to Object"))
        {
            Debug.Log("Linking to object");
            objectNumToLinkTo = objectLinkNumber;
            levelEditor.CurrentMode = LevelEditor.LevelEditorMode.PickToLinkMode;
        }
    }
}
