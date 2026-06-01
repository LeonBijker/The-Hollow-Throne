using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float stopDistance = 0.5f;
    [Tooltip("Multiplier increase per unit distance beyond stopDistance")]
    [SerializeField] private float accelFactor = 0.5f;
    [SerializeField] private float maxMultiplier = 2f;

    private IEnemyMovement movement;
    private Vector3 initialScale;
    private Vector2 lastFacingDirection = Vector2.right;

    private void Awake()
    {
        movement = GetComponent<IEnemyMovement>();
        initialScale = transform.localScale;

        if (player == null)
        {
            var found = GameObject.FindGameObjectWithTag("Player");
            if (found != null)
                player = found.transform;
        }
    }

    private void FixedUpdate()
    {
        if (player == null || movement == null) return;

        Vector2 current = transform.position;
        Vector2 target = player.position;
        Vector2 toTarget = target - current;
        float distance = toTarget.magnitude;

        if (distance > stopDistance)
        {
            Vector2 dir = toTarget.normalized;

            float multiplier = 1f + (distance - stopDistance) * accelFactor;
            multiplier = Mathf.Clamp(multiplier, 1f, maxMultiplier);

            movement.Move(dir * multiplier);

            if (Mathf.Abs(dir.x) > 0.01f)
                lastFacingDirection = dir;
        }
        else
        {
            movement.Move(Vector2.zero);
        }

        // flip sprite horizontally based on last facing direction
        if (lastFacingDirection.x > 0.01f)
            transform.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
        else if (lastFacingDirection.x < -0.01f)
            transform.localScale = new Vector3(-Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
    }

    private void OnDrawGizmosSelected()
    {
        if (player == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
