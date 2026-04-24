using UnityEngine;

public class PlayerJump : MonoBehaviour
{
  private Rigidbody2D rb => GetComponent<Rigidbody2D>();
  [SerializeField] float jumpForce = 5f;

  public void Jump(bool Pcanjump)
  {
    if (Pcanjump)
    {
      rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
      rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
  }
}
