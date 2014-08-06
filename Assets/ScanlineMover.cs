using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class ScanlineMover : MonoBehaviour {

	const float Y_OFFSET_STEP = 0.05f;

	Renderer _theRenderer;
	
	void Start () {
		_theRenderer = gameObject.GetComponent<Renderer>().renderer;
	}

	void Update () {
		Vector2 currentOffset = _theRenderer.material.GetTextureOffset("_MainTex");
		float offsetTouse = currentOffset.y + (Y_OFFSET_STEP * Time.deltaTime);

		_theRenderer.material.SetTextureOffset("_MainTex", new Vector2(0, offsetTouse));
	}
}
