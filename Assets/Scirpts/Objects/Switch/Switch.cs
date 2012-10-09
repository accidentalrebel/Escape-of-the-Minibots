using UnityEngine;
using System.Collections;

public class Switch : LevelObject {

    public LevelObject objectToActivate;
    protected bool isTriggered = false;
    protected Collider triggeredCollider;

	// Use this for initialization
	void Start () {
        if (objectToActivate == null)
            Debug.LogError("objectToActivate is not specified");
	}
	
	internal void Initialize(Vector3 theStartingPos)
	{
		base.Initialize(theStartingPos);
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
