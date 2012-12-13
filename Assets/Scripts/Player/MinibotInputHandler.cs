using UnityEngine;
using System.Collections;

public class MinibotInputHandler : MonoBehaviour {

    float xAxis = 0;
    internal float XAxis
    {
        get { return xAxis; }
    }
    float yAxis = 0;
    internal float YAxis
    {
        get { return yAxis; }
    }

	// Use this for initialization
	void Start () {
        Registry.inputHandler = this;
	}
	
	// Update is called once per frame
	void Update () {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
	}
}