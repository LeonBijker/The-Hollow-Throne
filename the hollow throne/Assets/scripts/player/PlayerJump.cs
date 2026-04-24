using UnityEngine;

public class PlayerJump : MonoBehaviour, IJumping
{
  private Rigidbody2D rb => GetComponent<Rigidbody2D>();
  [SerializeField] float jumpForce = 5f;
  [SerializeField] LayerMask groundLayer;
  [SerializeField] private BoxCollider2D groundcheck;

  public void Jump(bool Pcanjump)
  {
    if (Pcanjump && IsGrounded())
    {
      rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
      rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
  }

  public bool IsGrounded()
  {
    if (groundcheck == null) return false;
    return Physics2D.OverlapBox(groundcheck.bounds.center, groundcheck.bounds.size, 0, groundLayer);
  }
  
}
