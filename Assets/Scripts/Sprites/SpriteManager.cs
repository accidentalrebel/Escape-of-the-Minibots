using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class SpriteManager : MonoBehaviour {

    Renderer theRenderer;
    public bool enableAnimation = true;
    int totalNumberOfFrames;
    string currentAnimation;

    Dictionary<int, Vector2> animationFrames = new Dictionary<int, Vector2>();
    Dictionary<string, AnimationValues> animationSets = new Dictionary<string, AnimationValues>();
        
    private struct AnimationValues
    {
        public int[] frameSet;
        public float animationSpeed;

        public AnimationValues(int[] theFrameSet, float theAnimationSpeed)
        {
            frameSet = theFrameSet;
            animationSpeed = theAnimationSpeed;
        }
    }

	// Use this for initialization
	void Start () {
        theRenderer = gameObject.GetComponent<Renderer>().renderer;
        if (theRenderer == null)
            Debug.LogError("theRenderer is not found!");
                
        Initialize(4, 4);                               // We initialize the sprite sheet
        CreateAnimation("walking", new AnimationValues(new int[] {1,3}, 0.5f));    // We create a new walkign animation
        SetCurrentAnimation("walking");                 // We set the new walking animation as our current animation
        StartCoroutine("PlayAnimation");                // We start the animation
	}

    /// <summary>
    /// This initializes the sprite sheet. 
    /// It determines all the frames in the sheet and assigns a frame offset to each one.
    /// </summary>
    /// <param name="numOfHorizontalFrames">The number of horizontal frames in the spritesheet</param>
    /// <param name="numOfVerticalFrames">The number of vertical frames in the spritesheet</param>
    void Initialize(int numOfHorizontalFrames, int numOfVerticalFrames)
    {
        Vector2 offsetDifference = new Vector2();

        // We get the offset difference, this is the difference of one from from another
        offsetDifference.x = (float)(1f / numOfHorizontalFrames);
        offsetDifference.y = (float)(1f / numOfVerticalFrames);

        // We then get the total number of frames
        totalNumberOfFrames = numOfVerticalFrames * numOfVerticalFrames;

        int frameIndex = 1;
        int[] defaultFrameSet = new int[totalNumberOfFrames+1];
        Vector2 currentOffset = new Vector2(0, 1f-offsetDifference.y);
        
        // The following assigns the offsets to the a frame index through the use of a dictionary
        while ( frameIndex <= totalNumberOfFrames )
        {   
            animationFrames.Add(frameIndex, currentOffset);
            defaultFrameSet[frameIndex-1] = frameIndex;
            frameIndex++;
            currentOffset.x += offsetDifference.x;

            // If we exceed the xoffset, go back to zero x position
            if (currentOffset.x >= 1)
            {   
                currentOffset.x = 0;                        // Go back to the first frame in x
                currentOffset.y -= offsetDifference.y;      // And then move the y offset

                // We check if we exceed the yOffset, if we did, we go back to the very first frame
                if (currentOffset.y < 0)
                {
                    currentOffset.x = 0;                    // Go back to the first frame in x
                    currentOffset.y = 1 - offsetDifference.y;   // Go to the first frame in y
                }
            }
        }

        CreateAnimation("default", new AnimationValues(defaultFrameSet, 0.5f));        // We create the default animation for default
        SetCurrentAnimation("default");                     // We set the default as our default animation
    }

    /// <summary>
    /// This sets the currentAnimation
    /// </summary>
    /// <param name="nameOfAnimationSet">The name of the animationToUse</param>
    private void SetCurrentAnimation(string nameOfAnimationSet)
    {
        currentAnimation = nameOfAnimationSet;
    }

    /// <summary>
    /// This creates a new animation
    /// </summary>
    /// <param name="animationName">The name of the new animation set</param>
    /// <param name="frameSet">the frames to use for this animation (i.e. {1, 2, 4, 5})</param>
    void CreateAnimation(string animationName, AnimationValues animationValue)
    {
        animationSets.Add(animationName, animationValue);
    }

    /// <summary>
    /// Plays the animation
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayAnimation()
    {
        int currentFrame = 1;

        // We then get the animationFrameSets. This is the one that we will loop through for this animation. 
        int[] animationFrameSets = animationSets[currentAnimation].frameSet;
        float animationSpeed = animationSets[currentAnimation].animationSpeed;

        // We make sure that animation is enabled
        while (enableAnimation)
        {
            // This line of code does a couple of things
            // First it gets the currentAnimation frame in this set
            // Then it gets the offsets for that particular frame
            // And then it sets the textureOffset according to the offset received
            theRenderer.material.SetTextureOffset("_MainTex", animationFrames[animationFrameSets[currentFrame - 1]]);
                        
            yield return new WaitForSeconds(animationSpeed);    // Let's wait for a number of seconds
            currentFrame++;                                     // We then increase the currentFrame

            // If we reached the end
            if (currentFrame > animationFrameSets.Length)
            {
                currentFrame = 1;                               // go to the first frame
            }

            // We loop back up again and display the next animation frame
        }
    }
}
