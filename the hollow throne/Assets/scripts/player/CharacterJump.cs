using UnityEngine;

public class CharacterJump : MonoBehaviour, IJumping
{
    private Rigidbody2D rb;

    [SerializeField] float jumpForce = 5f;

    private bool isGrounded;
    private GroundSensor groundSensor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundSensor = GetComponentInChildren<GroundSensor>();
    }

    public void Jump(bool Pcanjump)
    {
        // refresh grounded state from sensor
        if (groundSensor != null)
            isGrounded = groundSensor.IsGrounded();

        if (Pcanjump && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (groundSensor != null)
            isGrounded = groundSensor.IsGrounded();
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        bool grounded = isGrounded;
        if (groundSensor != null)
            grounded = groundSensor.IsGrounded();

        Gizmos.color = grounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }
}