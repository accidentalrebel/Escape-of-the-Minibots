using UnityEngine;
using System.Collections;

public class Switch : LevelObject {

    public LevelObject objectToActivate;
    protected bool isTriggered = false;
    protected Collider triggeredCollider;
	private Map map;

	// Use this for initialization
	void Awake () {
		map = Registry.map;
		
        if (objectToActivate == null)
            Debug.LogWarning("objectToActivate is not specified");
	}
	
	internal void Initialize(Vector3 theStartingPos, Vector2 posOfObjectToActivate)
	{
		base.Initialize(theStartingPos);		
		
		Debug.Log ("map is " + map);
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
}
