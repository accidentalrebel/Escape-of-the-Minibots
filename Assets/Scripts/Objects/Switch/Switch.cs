using UnityEngine;
using System.Collections;

public class Switch : LevelObject {

    public Vector3 posOfObjectToActivate1 = Vector3.zero;
    public Vector3 posOfObjectToActivate2 = Vector3.zero;
    public Vector3 posOfObjectToActivate3 = Vector3.zero;

	[SerializeField]
	private Texture _triggeredTexture;

	[SerializeField]
	private Texture _untriggeredTexture;

    private int _objectNumToLinkTo = 0;
	protected Map _map;
    protected bool _isTriggered = false;
    protected Collider _triggeredCollider;

	override protected void Awake () {
		base.Awake();
		
		_map = Registry.map;

		if ( _triggeredTexture == null )
			Debug.LogWarning("triggeredTexture not initialized!");
		if ( _untriggeredTexture == null )
			Debug.LogWarning("untriggeredTexture not initialized!");
	}

	public void Initialize(Vector3 theStartingPos, Vector2 thePosOfObjectToActivate1)
    {
        base.Initialize(theStartingPos);
        posOfObjectToActivate1 = new Vector3(thePosOfObjectToActivate1.x, thePosOfObjectToActivate1.y, 0);       
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
        Vector3 thePosition = new Vector3(theObject.gameObject.transform.position.x, theObject.gameObject.transform.position.y, 0);;

        if (_objectNumToLinkTo == 1)
            posOfObjectToActivate1 = thePosition;                
        else if (_objectNumToLinkTo == 2)
            posOfObjectToActivate2 = thePosition;
        else if (_objectNumToLinkTo == 3)
            posOfObjectToActivate3 = thePosition;

        _objectNumToLinkTo = 0;
    }

	void LateUpdate () 
    {
        if (_isTriggered)
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
			objectToUse = _map.GetLevelObjectAtPosition(posOfObjectToActivate1);
			objectToUse.Use();
		}
		if (posOfObjectToActivate2 != Vector3.zero)
		{
			objectToUse = _map.GetLevelObjectAtPosition(posOfObjectToActivate2);
			objectToUse.Use();
		}
		if (posOfObjectToActivate3 != Vector3.zero)
		{
			objectToUse = _map.GetLevelObjectAtPosition(posOfObjectToActivate3);
			objectToUse.Use();
		}
	}

    void OnTriggerEnter(Collider col)
    {
		if (col.tag == "Player") {
	        _triggeredCollider = col;
	        _isTriggered = true;
		}
    }

    void OnTriggerExit()
    {
        _triggeredCollider = null;
        _isTriggered = false;

		UpdateSwitchGraphic();
    }

	virtual protected void UpdateSwitchGraphic ()
	{
		if ( _isTriggered )
			_graphicHandler.theRenderer.material.SetTexture("_MainTex", _triggeredTexture);
		else
			_graphicHandler.theRenderer.material.SetTexture("_MainTex", _untriggeredTexture);
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

		float left = (Screen.width / 2) - 140;
		float top = (Screen.height / 2) - 110 + (30 * (objectLinkNumber - 1));
		float width = 100;
		float height = 20;
        GUI.Label(new Rect(left, top, width, height), toDisplay);

		left = (Screen.width / 2) - 30;
        if (GUI.Button(new Rect(left, top, width, height), "Link to Object"))
        {
            Debug.Log("Linking to object");
            _objectNumToLinkTo = objectLinkNumber;
            levelEditor.CurrentMode = LevelEditor.LevelEditorMode.PickToLinkMode;
        }
    }

	public override void ResetObject ()
	{
		base.ResetObject ();		
		_isTriggered = false;
	}
}
