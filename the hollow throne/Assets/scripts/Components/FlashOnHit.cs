using System.Collections;
using UnityEngine;

public class FlashOnHit : MonoBehaviour
{
    private Health health;
    private SpriteRenderer sprite;

    private void Awake()
    {
        health = GetComponent<Health>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        health.OnDamaged += HandleDamaged;
    }

    private void OnDisable()
    {
        health.OnDamaged -= HandleDamaged;
    }

    private void HandleDamaged(int amount)
    {
        StartCoroutine(FlashSprite());
    }

    private IEnumerator FlashSprite()
    {
        sprite.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        sprite.color = Color.white;
    }
}