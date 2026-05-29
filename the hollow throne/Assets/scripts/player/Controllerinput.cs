using UnityEngine;

public class ControllerInput : MonoBehaviour, Iinput
{
    [SerializeField] string horizontalAxis = "Horizontal";
    [SerializeField] string jumpButton = "Jump";
    [SerializeField] string dashButton = "Fire3";

    private Vector2 direction;
    private Vector2 lastFacingDirection = Vector2.right;

    public Vector2 GetMovementInput()
    {
        Vector2 currentinput = Vector2.zero;

        currentinput.x = Input.GetAxis("Horizontal");
        currentinput.y = Input.GetAxis("Vertical");

        return currentinput == Vector2.zero ? currentinput : currentinput.normalized;
    }

    public bool GetJumpInput()
    {
        return Input.GetButtonDown(jumpButton);
    }

    public bool GetDashInput()
    {
        return Input.GetButtonDown(dashButton);
    }

    public Vector2 GetLastFacingDirection()
    {
        return lastFacingDirection;
    }
}