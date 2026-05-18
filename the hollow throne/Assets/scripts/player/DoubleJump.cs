using UnityEngine;

public class DoubleJump : MonoBehaviour, IJumping
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    [SerializeField] float jumpForce = 5f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private BoxCollider2D groundcheck;

    [SerializeField] private int jumpCount = 0;
    private const int maxJumps = 2;

    public void Jump(bool canJump)
    {
        if (canJump && jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
            print(jumpCount);
        }
    }

    public bool IsGrounded()
    {
        if (groundcheck == null) return false;
        bool grounded = Physics2D.OverlapBox(groundcheck.bounds.center, groundcheck.bounds.size, 0, groundLayer);
        if (grounded) jumpCount = 0; // Reset jumps when landing
        return grounded;
    }

    private void FixedUpdate()
    {
        // Continuously check ground to reset jump count when landing
        if (IsGrounded() && jumpCount > 0)
        {
            jumpCount = 0;
        }
    }
}
