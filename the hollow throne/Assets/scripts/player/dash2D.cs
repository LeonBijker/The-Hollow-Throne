using UnityEngine;

public class dash2D : MonoBehaviour
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    [SerializeField] float dashForce = 20f;
    [SerializeField] float dashDuration = 0.3f;
    private bool isDashing = false;
    private float dashTime;

    public bool IsDashing() => isDashing;



    public void Dash(Vector2 direction)
    {
        if (!isDashing)
        {
            isDashing = true;
            dashTime = Time.time + dashDuration;
            rb.linearVelocity = new  Vector2(direction.x * dashForce, rb.linearVelocity.y);
            Debug.Log("Dashing in direction: " + direction);
        }
    }

    private void Update()
    {
        if (isDashing && Time.time >= dashTime)
        {
            isDashing = false;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocity.y); // Stop horizontal dash, keep vertical speed
        }
    }
}
