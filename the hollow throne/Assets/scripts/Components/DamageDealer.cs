using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damagable = collision.GetComponent<IDamageable>();
        if (damagable != null)
        {
            damagable.TakeDamage(damage, gameObject);
        }
    }
}
