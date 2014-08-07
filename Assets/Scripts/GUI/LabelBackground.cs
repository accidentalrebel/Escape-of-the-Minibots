using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMesh))]
public class LabelBackground : MonoBehaviour {

	public Object pfLabelBackground;

	void Start () {
		TextMesh textMesh = transform.GetComponent("TextMesh") as TextMesh;

		GameObject spawnedObject = createAndAddLabelBackgroundObject();
		adjustScaleAccordingToTextWidth(spawnedObject, textMesh);
	}

	GameObject createAndAddLabelBackgroundObject() 
	{
		GameObject spawnedObject = (GameObject)Instantiate(pfLabelBackground);
		spawnedObject.transform.parent = transform;
		spawnedObject.transform.localPosition = new Vector3(0, -0.5f, -0.5f);
		spawnedObject.name = "labelBackground";

		return spawnedObject;
	}

	void adjustScaleAccordingToTextWidth (GameObject spawnedObject, TextMesh textMesh)
	{
		Vector3 currentScale = spawnedObject.transform.localScale; 
		spawnedObject.transform.localScale = new Vector3(textMesh.text.Length / 2, currentScale.y, currentScale.z);
	}
}
