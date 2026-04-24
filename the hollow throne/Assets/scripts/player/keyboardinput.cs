using UnityEngine;

public class keyboardinput : MonoBehaviour
{
  [SerializeField] KeyCode Left = KeyCode.A;
  [SerializeField] KeyCode Right = KeyCode.D;
  [SerializeField] KeyCode Up = KeyCode.Space;
  [SerializeField] KeyCode dash = KeyCode.W;
  
  Vector2 direction;
  Vector2 lastFacingDirection = Vector2.right;
  
  public Vector2 getinput()
  {
    if (Input.GetKey(Left))
    {
      direction.x = -1;
      lastFacingDirection = Vector2.left;
    }
    else if (Input.GetKey(Right))
    {
      direction.x = 1;
      lastFacingDirection = Vector2.right;
    }
    else
    {
      direction.x = 0;
    }
    return direction;
  }

  public bool getJumpInput()
  {
    return Input.GetKeyDown(Up);
  }

  public bool getDashInput()
  {
    return Input.GetKeyDown(dash);
  }

  public Vector2 getLastFacingDirection()
  {
    return lastFacingDirection;
  }
  
}
