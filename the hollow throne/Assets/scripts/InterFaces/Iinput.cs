using UnityEngine;

public interface Iinput
{
    Vector2 GetMovementInput();

    bool GetJumpInput();

    bool GetDashInput();

    Vector2 GetLastFacingDirection();

}
