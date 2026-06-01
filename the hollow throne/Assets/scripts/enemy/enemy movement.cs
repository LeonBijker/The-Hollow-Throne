using UnityEngine;

public class enemymovement : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;

    private Rigidbody2D rb;
    private Transform targetPoint;
    private Vector3 initialScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialScale = transform.localScale;

        if (pointA != null && pointB != null)
            targetPoint = pointB;
    }

    private void FixedUpdate()
    {
        if (pointA == null || pointB == null)
        {
            // fallback: simple constant horizontal movement to the right
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            return;
        }

        Vector2 currentPos = rb.position;
        Vector2 targetPos = targetPoint.position;
        Vector2 direction = (targetPos - currentPos).normalized;

        rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);

        // switch target when close
        if (Vector2.Distance(currentPos, targetPos) < 0.1f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;
        }

        // flip sprite based on movement direction
        if (rb.linearVelocity.x > 0.01f)
            transform.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
        else if (rb.linearVelocity.x < -0.01f)
            transform.localScale = new Vector3(-Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
    }

    private void OnDrawGizmosSelected()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(pointA.position, 0.1f);
            Gizmos.DrawSphere(pointB.position, 0.1f);
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}
