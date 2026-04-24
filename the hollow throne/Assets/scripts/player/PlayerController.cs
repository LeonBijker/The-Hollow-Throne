using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Movement2D movement2D => GetComponent<Movement2D>();
    keyboardinput keyboardinput => GetComponent<keyboardinput>();
    IJumping jumping => GetComponent<IJumping>();
    dash2D dash2D => GetComponent<dash2D>();

    private void Update()
    {
        if (!dash2D.IsDashing())
        {
            movement2D.Move(keyboardinput.getinput());
        }
        
        jumping.Jump(keyboardinput.getJumpInput());
        
        if (keyboardinput.getDashInput())
        {
            dash2D.Dash(keyboardinput.getLastFacingDirection());
        }
    }
}
