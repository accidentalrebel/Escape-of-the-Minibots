using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

    public GameObject objectToActivate;
    private bool isTriggered = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isTriggered)
        {
            if (Input.GetKeyUp(KeyCode.X))
            {
                Debug.Log("Used switch!");
            }
        }
	}

    void OnTriggerEnter()
    {
        isTriggered = true;
    }

    void OnTriggerExit()
    {
        isTriggered = false;
    }
}
