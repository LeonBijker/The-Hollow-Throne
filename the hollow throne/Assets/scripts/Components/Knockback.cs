using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] float force;
    

    public void ApplyTO(Rigidbody2D rb,  Vector2 direction)
    {
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }
}
