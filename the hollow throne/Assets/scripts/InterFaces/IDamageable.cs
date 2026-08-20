using UnityEngine;

public interface IDamageable 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void TakeDamage(int Amount, GameObject source);
}
