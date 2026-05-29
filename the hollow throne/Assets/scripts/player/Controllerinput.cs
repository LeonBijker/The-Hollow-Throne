using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInput : MonoBehaviour, Iinput
{
    private Vector2 lastFacingDirection = Vector2.right;

    [SerializeField] private bool debugInput = true;

    public Vector2 GetMovementInput()
    {
        if (Gamepad.current == null)
            return Vector2.zero;

        Vector2 input = Gamepad.current.leftStick.ReadValue();
        Vector2 dpad = Gamepad.current.dpad.ReadValue();

        Vector2 finalInput = input + dpad;

        if (finalInput.magnitude < 0.1f)
            finalInput = Vector2.zero;

        if (finalInput != Vector2.zero)
            lastFacingDirection = finalInput.normalized;

        return finalInput;
    }

    public bool GetJumpInput()
    {
        if (Gamepad.current == null)
            return false;

        bool pressed = Gamepad.current.buttonSouth.wasPressedThisFrame;

        if (debugInput && pressed)
            Debug.Log("JUMP pressed (A button)");

        return pressed;
    }

    public bool GetDashInput()
    {
        if (Gamepad.current == null)
            return false;

        bool pressed = Gamepad.current.buttonNorth.wasPressedThisFrame;

        if (debugInput && pressed)
            Debug.Log("DASH pressed (Y button)");

        return pressed;
    }

    public Vector2 GetLastFacingDirection()
    {
        return lastFacingDirection;
    }
}