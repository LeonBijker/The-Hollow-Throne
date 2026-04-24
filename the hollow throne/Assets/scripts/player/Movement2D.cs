using UnityEngine;

public class Movement2D : MonoBehaviour
{
   private Rigidbody2D rb => GetComponent<Rigidbody2D>();
   [SerializeField] float movementSpeed = 1f;

   public void Move(Vector2 Pdirection)
   {
      rb.linearVelocity = new Vector2(Pdirection.x * movementSpeed, rb.linearVelocityY);
   }
}
