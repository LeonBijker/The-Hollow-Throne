using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    [SerializeField] private Vector2 groundCheckOffset = new Vector2(0f, -0.5f);
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer = ~0;

    public bool IsGrounded()
    {
        Vector2 checkPos = (Vector2)transform.position + groundCheckOffset;
        Collider2D col = Physics2D.OverlapCircle(checkPos, groundCheckRadius, groundLayer);
        return col != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector2)transform.position + groundCheckOffset, groundCheckRadius);
    }
}
