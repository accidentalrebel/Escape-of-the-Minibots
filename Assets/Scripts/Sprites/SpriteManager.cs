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

            // If we exceed the xoffset
            if (newOffset.x >= 1)
            {
                newOffset.x = 0;        // Go back to the first frame
                newOffset.y -= 0.25f;   // And then move the y offset

                // We check if we exceed the yOffset
                if (newOffset.y < 0)
                {
                    newOffset.x = 0;
                    newOffset.y = 0.75f;
                }
            }

            // We then apply the new offset
            theRenderer.material.SetTextureOffset
                ("_MainTex", newOffset);

            yield return new WaitForSeconds(animationSpeed);            
        }
    }
}
