using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class SpriteManager : MonoBehaviour {

    Renderer theRenderer;
    public float animationSpeed = 1;
    public bool enableAnimation = true;
    int totalNumberOfFrames;
    string currentAnimation;

    Dictionary<int, Vector2> animationFrames = new Dictionary<int, Vector2>();
    Dictionary<string, int[]> animationSets = new Dictionary<string, int[]>();

	// Use this for initialization
	void Start () {
        currentAnimation = "default";

        theRenderer = gameObject.GetComponent<Renderer>().renderer;
        if (theRenderer == null)
            Debug.LogError("theRenderer is not found!");
                
        Initialize(4, 4);
        CreateAnimation("walking", new int[] {1,3});
        SetCurrentAnimation("walking");
        StartCoroutine("Animate");
	}

    void Initialize(int numOfHorizontalFrames, int numOfVerticalFrames)
    {
        Vector2 offsetDifference = new Vector2();
        offsetDifference.x = (float)(1f / numOfHorizontalFrames);
        offsetDifference.y = (float)(1f / numOfVerticalFrames);
        totalNumberOfFrames = numOfVerticalFrames * numOfVerticalFrames;

        int frameIndex = 1;
        int[] defaultFrameSet = new int[totalNumberOfFrames+1];
        Vector2 currentOffset = new Vector2(0, 1f-offsetDifference.y);
        
        while ( frameIndex <= totalNumberOfFrames )
        {   
            animationFrames.Add(frameIndex, currentOffset);
            defaultFrameSet[frameIndex-1] = frameIndex;
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

        // We create the default animation
        CreateAnimation("default", defaultFrameSet);
        SetCurrentAnimation("default");
    }

    private void SetCurrentAnimation(string nameOfAnimationSet)
    {
        currentAnimation = nameOfAnimationSet;
    }

    void CreateAnimation(string animationName, int[] frameSet)
    {
        animationSets.Add(animationName, frameSet);
    }

    IEnumerator Animate()
    {
        int currentFrame = 1;
        int[] animationFrameSets = animationSets[currentAnimation];        

        while (enableAnimation)
        {
            theRenderer.material.SetTextureOffset("_MainTex", animationFrames[animationFrameSets[currentFrame - 1]]);

            yield return new WaitForSeconds(animationSpeed);
            currentFrame++;

            // If we reached the end, go to the first frame
            if (currentFrame > animationFrameSets.Length)
            {
                currentFrame = 1;
            }
        }
    }
}
