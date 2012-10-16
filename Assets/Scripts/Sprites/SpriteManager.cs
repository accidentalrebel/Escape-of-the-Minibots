using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class SpriteManager : MonoBehaviour {

    Renderer theRenderer;
    public float animationSpeed = 1;
    public bool enableAnimation = true;

	// Use this for initialization
	void Start () {
        theRenderer = gameObject.GetComponent<Renderer>().renderer;
        if (theRenderer == null)
            Debug.LogError("theRenderer is not found!");

        StartCoroutine("Animate");
	}

    IEnumerator Animate()
    {
        Vector2 newOffset = new Vector2();
        while (enableAnimation)
        {
            newOffset = theRenderer.material.GetTextureOffset("_MainTex");
            newOffset.x += 0.25f;

            // If we exceed the offset
            if ( newOffset.x >= 1 )
                newOffset.x = 0;    // Go back to the first frame

            theRenderer.material.SetTextureOffset
                ("_MainTex", newOffset);

            yield return new WaitForSeconds(animationSpeed);            
        }
    }
}
