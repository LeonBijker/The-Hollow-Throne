using UnityEngine;

public class GroundChecker : MonoBehaviour
{
  private Collider2D col => GetComponent<Collider2D>();
  [SerializeField] LayerMask groundLayer;
  [SerializeField] float groundCheckDistance = 0.2f;

  public bool IsGrounded()
  {
    return Physics2D.OverlapBox(col.bounds.center + Vector3.down * col.bounds.extents.y,
                                new Vector2(col.bounds.size.x, groundCheckDistance),
                                0f,
                                groundLayer) != null;
  }
}

