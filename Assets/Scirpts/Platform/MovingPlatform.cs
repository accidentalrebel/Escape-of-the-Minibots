using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public enum Direction { Left, Right };

    public Vector2 startingPosition;
    public Vector2 endingPosition;
    public float speed = 1;
    public Direction startingDirection = Direction.Right;
    private Direction direction;


    void Start()
    {
        if (startingPosition == Vector2.zero)
            startingPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        if (endingPosition == null)
            Debug.LogError("EndingPosition not specified!");

        direction = startingDirection;
    }

	// Update is called once per frame
	void Update () {

        // Have we reached the ending position?
        if ((direction == Direction.Right && transform.position.x >= endingPosition.x)
            || ( direction == Direction.Left && transform.position.x <= startingPosition.x))
        {
            ChangeDirection();
        }

        if (direction == Direction.Right)
        {
            gameObject.transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else
        {
            gameObject.transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
	}

    private void ChangeDirection()
    {
        if (direction == Direction.Right)
            direction = Direction.Left;
        else
            direction = Direction.Right;
    }
}
