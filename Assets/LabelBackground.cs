using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMesh))]
public class LabelBackground : MonoBehaviour {

	public Object pfLabelBackground;

	void Start () {
		GameObject spawnedObject = (GameObject)Instantiate(pfLabelBackground);
		spawnedObject.transform.parent = transform;
		spawnedObject.transform.localPosition = new Vector3(0, 0, -0.5f);
		spawnedObject.name = "labelBackground";
	}
}
