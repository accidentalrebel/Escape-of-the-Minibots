using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

    public ItemObject objectToActivate;
    private bool isTriggered = false;

	// Use this for initialization
	void Start () {
        if (objectToActivate == null)
            Debug.LogError("objectToActivate is not specified");
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
        isTriggered = true;
    }

    void OnTriggerExit()
    {
        isTriggered = false;
    }
}
