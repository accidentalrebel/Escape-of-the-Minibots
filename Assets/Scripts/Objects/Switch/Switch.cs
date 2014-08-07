using UnityEngine;
using System.Collections;

public class Switch : LevelObject {

	const int MAX_NUM_OF_LINKS = 7;

    protected LevelObject[] _linkedObjects;
	public LevelObject[] LinkedObjects {
		get {
			return _linkedObjects;
		}
	}

	[SerializeField]
	private Texture _triggeredTexture;

	[SerializeField]
	private Texture _untriggeredTexture;

    private int _indexOfReadyToBeLinked = 0;
	private int _indexOfLink = 0;

	protected Map _map;
    protected bool _isTriggered = false;
    protected Collider _triggeredCollider;

	override protected void Awake () {
		base.Awake();
		
		_map = Registry.map;
		_linkedObjects = new LevelObject[MAX_NUM_OF_LINKS];

		if ( _triggeredTexture == null )
			Debug.LogWarning("triggeredTexture not initialized!");
		if ( _untriggeredTexture == null )
			Debug.LogWarning("untriggeredTexture not initialized!");
	}

	public void Initialize(Vector3 tStartingPos) {
		base.Initialize(tStartingPos);
	}

	public void PushToLinkedObjectsList(LevelObject tObject) {
		_linkedObjects[_indexOfLink] = tObject;
		_indexOfLink++;
	}

	public void PlaceInLinkedObjectsListAtIndex(LevelObject tObject) {
		_linkedObjects[_indexOfReadyToBeLinked] = tObject;
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
		Registry.sfxManager.PlaySFX(Registry.sfxManager.SFXButtonClick);

		foreach( LevelObject levelObject in _linkedObjects ) {
			if ( levelObject != null )
				levelObject.Use();
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
		int i = 1;
		foreach( LevelObject linkObject in _linkedObjects )
		{
			HandleLinkGUI(linkObject, i, levelEditor);
			i++;
		}  
    }

	private void HandleLinkGUI(LevelObject tObjectToActivate, int objectLinkNumber, LevelEditor levelEditor)
    {
        string toDisplay;

		if (tObjectToActivate != null)
			toDisplay = tObjectToActivate.name;
        else
            toDisplay = "No Link";       

		float left = (Screen.width / 2) - 180;
		float top = (Screen.height / 2) - 110 + (30 * (objectLinkNumber - 1));
		float width = 150;
		float height = 20;
        GUI.Label(new Rect(left, top, width, height), toDisplay);

		left = (Screen.width / 2) - 30;
		width = 100;
		float right = (Screen.width / 2 ) + 80;
        
		if (GUI.Button(new Rect(left, top, width, height), "Link to Object"))
        {
            _indexOfReadyToBeLinked = objectLinkNumber - 1;
            levelEditor.CurrentMode = LevelEditor.LevelEditorMode.PickToLinkMode;
        }

		if (tObjectToActivate != null
		    && GUI.Button(new Rect(right, top, width, height), "Unlink"))
		{
			_indexOfReadyToBeLinked = 0;
			RemoveLinkedObjectAtIndex(objectLinkNumber);
			levelEditor.CurrentMode = LevelEditor.LevelEditorMode.EditingMode;
		}
    }

	private void RemoveLinkedObjectAtIndex(int indexPos)
	{
		_linkedObjects[indexPos-1] = null;
	}

	public override void ResetObject ()
	{
		base.ResetObject ();		
		_isTriggered = false;
	}
}
