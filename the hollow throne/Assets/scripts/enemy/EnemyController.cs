using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private IMovementInput input;
    private IMovement movement;

    private void Awake()
    {
        input = GetComponent<IMovementInput>();
        movement = GetComponent<IMovement>();
    }

    private void Update()
    {
        if (input == null || movement == null) return;

        Vector2 direction = input.GetDirection();
        movement.Move(direction);
    }
}
