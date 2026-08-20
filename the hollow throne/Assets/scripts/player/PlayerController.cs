using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IMovement movement;
    private Iinput input;
    private IJumping jumping;
    private PlayerDash PlayerDash;

    private void Awake()
    {
        input = GetComponent<Iinput>();
        PlayerDash = GetComponent<PlayerDash>();
        movement = GetComponent<IMovement>();
        jumping = GetComponent<IJumping>();
    }

    private void Update()
    {
        // refresh input component each frame so runtime swaps (keyboard/controller) are picked up
        input = GetComponent<Iinput>();
        if (input == null || movement == null || PlayerDash == null) return;

        if (!PlayerDash.IsDashing())
        {
            movement.Move(input.GetMovementInput());
        }

        jumping?.Jump(input.GetJumpInput());

        if (input.GetDashInput())
        {
            PlayerDash.Dash(input.GetLastFacingDirection());
        }
    }

    // Swap or add a movement component at runtime. T must implement IMovement.
    // Swap or add a jumping component at runtime. T must implement IJumping.
    // This will remove any existing IJumping components that are not of type T.
    public void SwapJumping<T>() where T : Component, IJumping
    {
        // destroy existing IJumping components that are not of the requested type
        MonoBehaviour[] monos = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour m in monos)
        {
            if (m is IJumping && !(m is T))
            {
                Destroy(m);
            }
        }

        T comp = GetComponent<T>();
        if (comp == null)
            comp = gameObject.AddComponent<T>();

        jumping = comp as IJumping;
        Debug.Log($"PlayerController: Swapped jumping to {typeof(T).Name}");
    }
}
