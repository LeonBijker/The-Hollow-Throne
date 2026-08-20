using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        // try to swap the player's jumping implementation when they pick up this power-up
        PlayerController controller = go.GetComponent<PlayerController>();
        if (controller != null)
        {
            Debug.Log("Player collected power-up! Swapping jump to DoubleJump.");
            controller.SwapJumping<DoubleJump>();
            Destroy(gameObject);
            return;
        }

        // fallback: check by name if PlayerController not present
        if (go.name == "Player")
        {
            Debug.Log("Player collected power-up (by name), but PlayerController not found on object.");
            Destroy(gameObject);
        }
    }
}
