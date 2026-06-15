using UnityEngine;

public class EnemyPatrol : MonoBehaviour, IMovementInput
{
    [SerializeField] private float moveDuration = 3f;
    [SerializeField] private float wallCheckDistance = 0.6f;
    [SerializeField] private LayerMask wallLayer;

    private float timer;
    private int direction = -1;

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.State != GameState.Playing)
            return;

        timer += Time.deltaTime;

        if (timer >= moveDuration)
        {
            timer = 0f;
            direction *= -1;
        }

        Vector2 origin = transform.position;
        Vector2 dir = new Vector2(direction, 0);

        if (Physics2D.Raycast(origin, dir, wallCheckDistance, wallLayer))
        {
            direction *= -1;
            timer = 0f;
        }
    }

    public Vector2 GetDirection()
    {
        return new Vector2(direction, 0);
    }
}