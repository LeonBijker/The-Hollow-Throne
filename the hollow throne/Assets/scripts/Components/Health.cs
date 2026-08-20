using UnityEngine;
using System;

public class Health : MonoBehaviour ,IDamageable
{
    [SerializeField] int MaxHealth;
    int currentHealth;
    public event Action<int> OnDamaged;
    public event Action OnDeath;

    private void Awake()
    {
        currentHealth = MaxHealth;
    }
    public void TakeDamage(int amount, GameObject scource)
    {
        currentHealth -= amount;
        OnDamaged?.Invoke(amount);

        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}
