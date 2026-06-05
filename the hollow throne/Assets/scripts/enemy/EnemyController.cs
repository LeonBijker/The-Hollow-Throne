using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private IMovementInput input;
    private IMovement movement;
    private IJumping jumping;

    private void Awake()
    {
        input = GetComponent<IMovementInput>();
        movement = GetComponent<IMovement>();
        jumping = GetComponent<IJumping>();
        Debug.Log($"EnemyController Awake - IMovementInput: {input != null}, IMovement: {movement != null}, IJumping: {jumping != null}");
    }

    private void Update()
    {
        if (input == null || movement == null) return;

        Vector2 direction = input.GetDirection();
        movement.Move(direction);
        Debug.Log($"EnemyController Update - direction={direction}");

        if (jumping != null)
        {
            // use vertical component of movement input as jump intent
            bool wantsToJump = direction.y > 0.1f;
            bool canJump = jumping.IsGrounded() && wantsToJump;
            Debug.Log($"EnemyController Jump check - wantsToJump={wantsToJump}, isGrounded={jumping.IsGrounded()}, willJump={canJump}");
            jumping.Jump(canJump);
        }
    }
}
