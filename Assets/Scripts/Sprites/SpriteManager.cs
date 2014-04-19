using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class SpriteManager : MonoBehaviour {
		 
	public int numOfRows = 1;
	public int numOfCols = 1;
	public uint startingFrameIndex = 1;
	public string startingAnimation = "default";
	public bool enableAnimation = true;  

	private Renderer _theRenderer;   
    private int _totalNumberOfFrames;
    private bool _isHorizontallyFlipped = false;
    private Vector2 _offsetDifference = new Vector2();
    private Dictionary<int, Vector2> _animationFrames = new Dictionary<int, Vector2>();
    private Dictionary<string, AnimationProperties> _animationSets = new Dictionary<string, AnimationProperties>();

    public struct AnimationProperties
    {
        public int[] frameSet;          
        public float animationSpeed;    

        public AnimationProperties(int[] theFrameSet, float theAnimationSpeed)
        {
            frameSet = theFrameSet;
            animationSpeed = theAnimationSpeed;
        }
    }

	void Awake () {
        _theRenderer = gameObject.GetComponent<Renderer>().renderer;
        if (_theRenderer == null)
            Debug.LogError("theRenderer is not found!");

		if (startingFrameIndex <= 0 )
			Debug.LogWarning("startingFrameIndex should not be lower than 1!");
                
		Initialize(numOfRows, numOfCols);                               
		Play(startingAnimation);
		SetFrameTo(startingAnimation, startingFrameIndex);
	}

	public void Initialize(int numOfHorizontalFrames, int numOfVerticalFrames)
    {        
        _offsetDifference.x = (float)(1f / numOfHorizontalFrames);
        _offsetDifference.y = (float)(1f / numOfVerticalFrames);
		        
        _totalNumberOfFrames = numOfVerticalFrames * numOfVerticalFrames;

        int frameIndex = 1;
        int[] defaultFrameSet = new int[_totalNumberOfFrames];
        Vector2 currentOffset = new Vector2(0, 1f-_offsetDifference.y);
        
        while ( frameIndex <= _totalNumberOfFrames )
        {   
            _animationFrames.Add(frameIndex, currentOffset);
            defaultFrameSet[frameIndex-1] = frameIndex;
            frameIndex++;
            currentOffset.x += _offsetDifference.x;

            if (currentOffset.x >= 1)
            {   
                currentOffset.x = 0;                        	
                currentOffset.y -= _offsetDifference.y;     

                if (currentOffset.y < 0)
                {
                    currentOffset.x = 0;  
                    currentOffset.y = 1 - _offsetDifference.y; 
                }
            }
        }

		_theRenderer.material.SetTextureScale("_MainTex", new Vector2(1/numOfRows, 1/numOfCols));

		CreateAnimation(startingAnimation, new AnimationProperties(defaultFrameSet, 0.5f));    
    }

    public void CreateAnimation(string animationName, AnimationProperties animationProperty)
    {
        _animationSets.Add(animationName, animationProperty);
    }

    public void Play(string currentAnimation)
    {        
        StopCoroutine("Animate");                       

        if ( gameObject.activeSelf != false )
            StartCoroutine("Animate", currentAnimation);
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
		Vector3 currentPosition = transform.localPosition;
		float currentYScale = Mathf.Abs(currentScale.y);

		if ( flipValue )
		{
			transform.localScale = new Vector3(currentScale.x, -currentYScale, currentScale.z);
			transform.localPosition = new Vector3(currentPosition.x, -0.2f, currentPosition.z);
		}
		else
		{
			transform.localScale = new Vector3(currentScale.x, currentYScale, currentScale.z);
			transform.localPosition = new Vector3(currentPosition.x, 0f, currentPosition.z);
		}
	}

    IEnumerator Animate(string currentAnimation)
    {
        uint currentFrame = 1;
		        
        int[] animationFrameSets = _animationSets[currentAnimation].frameSet;
        float animationSpeed = _animationSets[currentAnimation].animationSpeed;
        		        
        while (enableAnimation)
        {
			SetFrameTo(currentAnimation, currentFrame);

            yield return new WaitForSeconds(animationSpeed);
            currentFrame++;                                     
			            
            if (currentFrame > animationFrameSets.Length)
            {
                currentFrame = 1;                              
            }
        }
    }

	public void SetFrameTo(string currentAnimation, uint currentFrame)
	{
		Debug.Log ("Animation set size is " + _animationSets.Count + " and playing " + currentAnimation + " at " + currentFrame);

		Vector2 newOffset = _animationFrames[_animationSets[currentAnimation].frameSet[currentFrame - 1]];

		int flipValue;
		if (_isHorizontallyFlipped)
		{
			newOffset.x += _offsetDifference.x;  
			flipValue = -1;                     
		}
		else
			flipValue = 1;
		
		_theRenderer.material.SetTextureScale("_MainTex", new Vector2((flipValue) * _offsetDifference.x, _offsetDifference.y));
		_theRenderer.material.SetTextureOffset("_MainTex", newOffset);
	}

	public void Reset ()
	{
		SetFlippedY(false);
		SetFlippedX(false);
	}
}
