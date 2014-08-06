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
		float offsetToUse = currentOffset.y + (Random.Range(-0.1f, 0.2f) * Time.deltaTime);
		_theRenderer.material.SetTextureOffset("_MainTex", new Vector2(0, offsetToUse));
	}
}
