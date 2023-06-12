using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    None, Left, Right, Up, Down
}

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance;

    static Direction currentDirection = Direction.None;

    bool isSlide;
    [SerializeField] Vector2 startPosition, diffPosition;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSlide)
        {
            isSlide = true;
            startPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && isSlide)
        {
            isSlide = false;
            startPosition = Vector2.zero;
            diffPosition = Vector2.zero;
        }

        currentDirection = CheckDirection();
    }

    private Direction CheckDirection()
    {
        if (!isSlide)
        {
            return Direction.None;
        }

        diffPosition = (Vector2)Input.mousePosition - startPosition;

        if (diffPosition.magnitude < 0.1f)
        {
            return Direction.None;
        }

        if (Mathf.Abs(diffPosition.x) > Mathf.Abs(diffPosition.y))
        {
            if (diffPosition.x > 0)
            {
                return Direction.Right;
            }
            else
            {
                return Direction.Left;
            }
        }
        else
        {
            if (diffPosition.y > 0)
            {
                return Direction.Up;
            }
            else
            {
                return Direction.Down;
            }
        }
    }

    public Direction GetDirection()
    {
        return currentDirection;
    }
}
