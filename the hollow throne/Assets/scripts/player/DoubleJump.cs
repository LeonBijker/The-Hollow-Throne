using UnityEngine;

public class DoubleJump : MonoBehaviour, IJumping
{
    private Rigidbody2D rb;

    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private BoxCollider2D groundcheck;

    private int jumpCount = 0;
    private const int maxJumps = 2;

    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Jump(bool canJump)
    {
        if (canJump && jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isGrounded = false;
        }
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}