using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class SpriteManager : MonoBehaviour {

	private bool enableAnimation = true;   
	private Renderer theRenderer;   
    private int totalNumberOfFrames;
    private bool _isHorizontallyFlipped = false;
    private Vector2 offsetDifference = new Vector2();
    private Dictionary<int, Vector2> animationFrames = new Dictionary<int, Vector2>();
    private Dictionary<string, AnimationProperties> animationSets = new Dictionary<string, AnimationProperties>();

    public struct AnimationProperties
    {
        public int[] frameSet;          // The frameSet that contains the frame numbers that this animation should loop through
        public float animationSpeed;    // The animation speed of this animation

        public AnimationProperties(int[] theFrameSet, float theAnimationSpeed)
        {
            frameSet = theFrameSet;
            animationSpeed = theAnimationSpeed;
        }
    }

	void Awake () {
        theRenderer = gameObject.GetComponent<Renderer>().renderer;
        if (theRenderer == null)
            Debug.LogError("theRenderer is not found!");
                
        Initialize(4, 4);                               // We initialize the sprite sheet        
        Play("default");
	}

	public void Initialize(int numOfHorizontalFrames, int numOfVerticalFrames)
    {
        // We get the offset difference, this is the difference of one from from another
        offsetDifference.x = (float)(1f / numOfHorizontalFrames);
        offsetDifference.y = (float)(1f / numOfVerticalFrames);

        // We then get the total number of frames
        totalNumberOfFrames = numOfVerticalFrames * numOfVerticalFrames;

        int frameIndex = 1;
        int[] defaultFrameSet = new int[totalNumberOfFrames];
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

        CreateAnimation("default", new AnimationProperties(defaultFrameSet, 0.5f));        // We create the default animation for default        
    }

    public void CreateAnimation(string animationName, AnimationProperties animationProperty)
    {
        animationSets.Add(animationName, animationProperty);
    }

    public void Play(string currentAnimation)
    {        
        StopCoroutine("Animate");                       // Stop coroutine if it is currently running

        if ( gameObject.activeSelf != false )
            StartCoroutine("Animate", currentAnimation);    // Start animate coroutine
    }

    public void Stop()
    {
        StopCoroutine("Animate");
    }

   	public void SetFlippedX(bool flipValue)
    {
		Vector3 currentScale = transform.localScale;
		float currentXScale = Mathf.Abs(currentScale.x);

		if ( flipValue && !_isHorizontallyFlipped )
			transform.localScale = new Vector3(-currentXScale, currentScale.y, currentScale.z);
		else
			transform.localScale = new Vector3(currentXScale, currentScale.y, currentScale.z);
    }

	public void SetFlippedY(bool flipValue)
	{
		Vector3 currentScale = transform.localScale;
		float currentYScale = Mathf.Abs(currentScale.y);

		if ( flipValue )
			transform.localScale = new Vector3(currentScale.x, -currentYScale, currentScale.z);
		else
			transform.localScale = new Vector3(currentScale.x, currentYScale, currentScale.z);
	}

    IEnumerator Animate(string currentAnimation)
    {
        int currentFrame = 1;

        // We then get the animationFrameSets. This is the one that we will loop through for this animation. 
        int[] animationFrameSets = animationSets[currentAnimation].frameSet;
        float animationSpeed = animationSets[currentAnimation].animationSpeed;
        int flipValue;

        // We make sure that animation is enabled
        while (enableAnimation)
        {
            // First it gets the currentAnimation frame in this set
            // Then it gets the offsets for that particular frame            
            Vector2 newOffset = animationFrames[animationFrameSets[currentFrame - 1]];

            // If the sprite is flipped
            if (_isHorizontallyFlipped)
            {
                newOffset.x += offsetDifference.x;  // Increase the x offset ( flipping actually moves the xOffset by one frame so we adjust it )
                flipValue = -1;                     // We then set the flipvalue        
            }
            else
                flipValue = 1;

            // We then do the actual flipping
            theRenderer.material.SetTextureScale("_MainTex"
                , new Vector2((flipValue) * offsetDifference.x, offsetDifference.y));

            // We then set the textureOffset according to the new calculated offset
            theRenderer.material.SetTextureOffset("_MainTex", newOffset);

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

	public void Reset ()
	{
		SetFlippedY(false);
		SetFlippedX(false);
	}
}
