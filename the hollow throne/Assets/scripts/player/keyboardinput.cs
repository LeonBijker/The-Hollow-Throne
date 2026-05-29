using UnityEngine;

public class KeyboardInput : MonoBehaviour, Iinput
{
    [SerializeField] KeyCode left = KeyCode.A;
    [SerializeField] KeyCode right = KeyCode.D;
    [SerializeField] KeyCode jump = KeyCode.Space;
    [SerializeField] KeyCode dash = KeyCode.W;

    private Vector2 lastFacingDirection = Vector2.right;

    public Vector2 GetMovementInput()
    {
        Vector2 currentInput = Vector2.zero;

        if (Input.GetKey(left))
        {
            currentInput.x = -1;
            lastFacingDirection = Vector2.left;
        }
        else if (Input.GetKey(right))
        {
            currentInput.x = 1;
            lastFacingDirection = Vector2.right;
        }

        return currentInput == Vector2.zero
            ? currentInput
            : currentInput.normalized;
    }

    public bool GetJumpInput()
    {
        return Input.GetKeyDown(jump);
    }

    public bool GetDashInput()
    {
        return Input.GetKeyDown(dash);
    }

    public Vector2 GetLastFacingDirection()
    {
        return lastFacingDirection;
    }
}