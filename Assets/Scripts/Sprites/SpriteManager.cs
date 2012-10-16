using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        Initialize(4, 4);
	}

    void Initialize(int numOfHorizontalFrames, int numOfVerticalFrames)
    {
        Vector2 offsetDifference = new Vector2();
        offsetDifference.x = (float)(1f / numOfHorizontalFrames);
        offsetDifference.y = (float)(1f / numOfVerticalFrames);
        int totalNumberOfFrames = numOfVerticalFrames * numOfVerticalFrames;

        Dictionary<int, Vector2> frames = new Dictionary<int, Vector2>();
        int frameIndex = 1;
        Vector2 currentOffset = new Vector2(0, 1f-offsetDifference.y);
        
        while ( frameIndex <= totalNumberOfFrames )
        {
            frames.Add(frameIndex, currentOffset);
            frameIndex++;
            currentOffset.x += offsetDifference.x;

            // If we exceed the xoffset
            if (currentOffset.x >= 1)
            {   
                currentOffset.x = 0;                        // Go back to the first frame
                currentOffset.y -= offsetDifference.y;     // And then move the y offset

                // We check if we exceed the yOffset
                if (currentOffset.y < 0)
                {   
                    currentOffset.x = 0;
                    currentOffset.y = 1 - offsetDifference.y;
                }
            }
        }
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
