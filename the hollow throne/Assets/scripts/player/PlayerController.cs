using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Movement2D movement2D => GetComponent<Movement2D>();
    keyboardinput keyboardinput => GetComponent<keyboardinput>();
    PlayerJump playerJump => GetComponent<PlayerJump>();
    GroundChecker groundChecker => GetComponent<GroundChecker>();

    private void Update()
    {
        movement2D.Move(keyboardinput.getinput());
        playerJump.Jump(keyboardinput.getJumpInput().y > 0 && groundChecker.IsGrounded());
    }
}
