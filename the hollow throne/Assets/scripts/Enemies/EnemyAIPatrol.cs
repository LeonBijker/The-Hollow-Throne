using UnityEngine;

public class EnemyAIPatrol : MonoBehaviour
{

    [Header("Edge Detection")]
    [SerializeField] private float frontOffset = 0.6f;
    [SerializeField] private float edgeCheckDistance = 0.8f;
    [SerializeField] private LayerMask groundLayer = 1 << 6;

    private IJumping jumping;
    private Rigidbody2D rb;

    private float nextRandomTime;
    private float randomTimer;

    private void Awake()
    {
        jumping = GetComponent<IJumping>();
        rb = GetComponent<Rigidbody2D>();


    }


    private void Update()
    {
        if (jumping == null) return;

        if (GameManager.Instance != null &&
            GameManager.Instance.State != GameState.Playing)
        {
            return;
        }

        randomTimer += Time.deltaTime;

        if (randomTimer >= nextRandomTime)
        {
            if (jumping.IsGrounded())
            {
                jumping.Jump(true);
            }


        }
    }

    private void FixedUpdate()
    {
        if (jumping == null) return;

        if (GameManager.Instance != null &&
            GameManager.Instance.State != GameState.Playing)
        {
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.angularVelocity = 0f;
            }

            return;
        }

        if (!jumping.IsGrounded()) return;

        float facing = 1f;

        if (rb != null)
        {
            if (Mathf.Abs(rb.linearVelocity.x) > 0.01f)
                facing = Mathf.Sign(rb.linearVelocity.x);
            else
                facing = Mathf.Sign(transform.localScale.x);
        }
        else
        {
            facing = Mathf.Sign(transform.localScale.x);
        }

        Vector2 origin = (Vector2)transform.position +
                         new Vector2(frontOffset * facing, 0.2f);

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            Vector2.down,
            edgeCheckDistance,
            groundLayer
        );

        if (hit.collider == null)
        {
            // No ground ahead, so jump instead of walking off the edge.
            jumping.Jump(true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        float facing = Mathf.Sign(transform.localScale.x);

        Vector2 origin = (Vector2)transform.position +
                         new Vector2(frontOffset * facing, 0f);

        Gizmos.DrawLine(
            origin,
            origin + Vector2.down * edgeCheckDistance
        );

        Gizmos.DrawSphere(origin, 0.05f);
    }
}
