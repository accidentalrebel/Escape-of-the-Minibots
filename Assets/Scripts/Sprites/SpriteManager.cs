using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class SpriteManager : MonoBehaviour {

    Renderer theRenderer;
    public float animationSpeed = 1;
    public bool enableAnimation = true;
    int totalNumberOfFrames;
    Dictionary<int, Vector2> animationFrames = new Dictionary<int, Vector2>();

	// Use this for initialization
	void Start () {
        theRenderer = gameObject.GetComponent<Renderer>().renderer;
        if (theRenderer == null)
            Debug.LogError("theRenderer is not found!");
                
        Initialize(4, 4);
        StartCoroutine("Animate");
	}

    void Initialize(int numOfHorizontalFrames, int numOfVerticalFrames)
    {
        Vector2 offsetDifference = new Vector2();
        offsetDifference.x = (float)(1f / numOfHorizontalFrames);
        offsetDifference.y = (float)(1f / numOfVerticalFrames);
        totalNumberOfFrames = numOfVerticalFrames * numOfVerticalFrames;

        int frameIndex = 1;
        Vector2 currentOffset = new Vector2(0, 1f-offsetDifference.y);
        
        while ( frameIndex <= totalNumberOfFrames )
        {           
            animationFrames.Add(frameIndex, currentOffset);
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

    void SetAnimation(string animationName, int[] frameSet)
    {

    }

    IEnumerator Animate()
    {
        int currentFrame = 1;

        while (enableAnimation)
        {
            theRenderer.material.SetTextureOffset("_MainTex", animationFrames[currentFrame]);

            yield return new WaitForSeconds(animationSpeed);
            currentFrame++;

            // If we reached the end, go to the first frame
            if (currentFrame > totalNumberOfFrames)
            {
                currentFrame = 1;
            }
        }
    }
}
