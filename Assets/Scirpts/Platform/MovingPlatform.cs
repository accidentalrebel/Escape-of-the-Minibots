using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public enum Direction { Left, Right };

    public Vector2 leftmostPosition;
    public Vector2 rightmostPosition;
    public float speed = 1;
    public Direction startingDirection = Direction.Right;
    private Direction direction;


    void Start()
    {
        if (leftmostPosition == Vector2.zero)
            leftmostPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        if (rightmostPosition == null)
            Debug.LogError("EndingPosition not specified!");

        gameObject.transform.position = new Vector3(leftmostPosition.x, leftmostPosition.y, 0);
        direction = startingDirection;
    }

	// Update is called once per frame
	void Update () {

        // Have we reached the ending position?
        if ((direction == Direction.Right && transform.position.x >= rightmostPosition.x)
            || ( direction == Direction.Left && transform.position.x <= leftmostPosition.x))
        {
            ChangeDirection();
        }

        if (direction == Direction.Right)
        {
            gameObject.transform.Translate(Vector3.right * Time.deltaTime * speed);
            //gameObject.rigidbody.AddForce(Vector3.right);
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
