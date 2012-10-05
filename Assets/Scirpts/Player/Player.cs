using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public enum Direction { Left, Right };
    private GameObject objectBeingCarried;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)
            && objectBeingCarried != null)
        {
            PutDown(objectBeingCarried);
        }

        // Handles the picking up of objects
        if (Input.GetKeyDown(KeyCode.C)
            && objectBeingCarried ==  null )
        {
            GameObject objectAtSide = GetObjectAtSide(Direction.Right);
            if ( objectAtSide != null
                && objectAtSide.tag == "Movable")
            {
                Debug.Log("There's a box");
                PickUp(objectAtSide);
            }
        }

        // Handles the carrying of the object
        if (objectBeingCarried != null)
        {
            objectBeingCarried.transform.position = transform.position + Vector3.up;
        }
    }

    private void PutDown(GameObject objectToPutDown)
    {
        objectBeingCarried.transform.position = transform.position + Vector3.right;
        objectToPutDown.GetComponent<Box>().PutDown();
        objectBeingCarried = null;
    }

    private void PickUp(GameObject objectAtSide)
    {
        objectBeingCarried = objectAtSide;
        objectBeingCarried.GetComponent<Box>().PickUp();
    }

    internal void Die()
    {
        Debug.LogWarning("I DIED");
        Registry.main.RestartGame();
    }

    void ExitStage()
    {
        Debug.Log("exiting stage");
        gameObject.SetActiveRecursively(false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Door")
        {
            if ( col.gameObject.GetComponent<Door>().isOpen )
                ExitStage();
        }
    }

    internal GameObject GetObjectAtSide(Direction direction)
    {
        RaycastHit hit;
        Vector3 checkDirection;
        if (direction == Direction.Left)
            checkDirection = Vector3.left;
        else
            checkDirection = Vector3.right;

        if (Physics.Raycast(gameObject.transform.position, checkDirection, out hit, 0.6f))
        {
            if (hit.collider.tag == "Tile"
                || hit.collider.tag == "Movable")
            {
                Debug.DrawLine(gameObject.transform.position, hit.point);
                return hit.collider.gameObject;
            }
        }
        return null;
    }
}
