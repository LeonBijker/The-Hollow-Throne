using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private IMovementInput input;
    private IMovement movement;
    private IJumping jumping;
    private bool enabledForGameplay = false;
    private Rigidbody2D rb;

    private void Awake()
    {
        input = GetComponent<IMovementInput>();
        movement = GetComponent<IMovement>();
        jumping = GetComponent<IJumping>();
        rb = GetComponent<Rigidbody2D>();

        if (GameManager.Instance != null)
            enabledForGameplay = GameManager.Instance.State == GameState.Playing;
    }

    private void Update()
    {
        if (!enabledForGameplay) return;
        if (input == null || movement == null) return;

        Vector2 direction = input.GetDirection();
        movement.Move(direction);

        if (jumping != null)
        {
            bool wantsToJump = direction.y > 0.1f;
            bool canJump = jumping.IsGrounded() && wantsToJump;
            jumping.Jump(canJump);
        }
    }

    private void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        enabledForGameplay = state == GameState.Playing;

        if (!enabledForGameplay && rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
