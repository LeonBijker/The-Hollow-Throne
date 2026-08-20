using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    private Health health;

    private void OnEnable()
    {
        health.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        health.OnDeath -= HandleDeath;
    }
    private void HandleDeath()
    {
        Destroy(gameObject);
    }
}
