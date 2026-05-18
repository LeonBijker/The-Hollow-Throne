using UnityEngine;

public class DoubleJump : MonoBehaviour, IJumping
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    [SerializeField] float jumpForce = 5f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private BoxCollider2D groundcheck;

    [SerializeField] private int jumpCount = 0;
    // keep this at 1 per your request (prevents unintended extra jumps)
    private const int maxJumps = 1;

    private void Start()
    {
        // If no ground layer was set in inspector, default to layer index 3
        if (groundLayer == 0)
        {
            groundLayer = 1 << 3; // layer 3
            Debug.Log("DoubleJump: groundLayer not set - defaulting to layer 3");
        }

        // Try to auto-assign the groundcheck collider if not set
        if (groundcheck == null)
        {
            groundcheck = GetComponent<BoxCollider2D>();
            if (groundcheck == null)
            {
                groundcheck = GetComponentInChildren<BoxCollider2D>();
            }

            if (groundcheck != null)
            {
                Debug.Log("DoubleJump: assigned groundcheck automatically: " + groundcheck.name);
            }
            else
            {
                Debug.LogWarning("DoubleJump: groundcheck BoxCollider2D not assigned and not found on GameObject or children.");
            }
        }
    }

    public void Jump(bool canJump)
    {
        if (canJump && jumpCount < maxJumps)
        {
            // zero vertical velocity before applying jump impulse
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
            Debug.Log("DoubleJump: jumpCount=" + jumpCount);
        }
    }

    public bool IsGrounded()
    {
        if (groundcheck == null) return false;
        bool grounded = Physics2D.OverlapBox(groundcheck.bounds.center, groundcheck.bounds.size, 0f, groundLayer);
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

    private void OnDrawGizmosSelected()
    {
        if (groundcheck == null) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(groundcheck.bounds.center, groundcheck.bounds.size);
    }
}
