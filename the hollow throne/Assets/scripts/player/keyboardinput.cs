using UnityEngine;

public class keyboardinput : MonoBehaviour
{
  [SerializeField] KeyCode Left = KeyCode.A;
  [SerializeField] KeyCode Right = KeyCode.D;
  [SerializeField] KeyCode Up = KeyCode.Space;
  
  Vector2 direction;
  
  public Vector2 getinput()
  {
    if (Input.GetKey(Left))
    {
      direction.x = -1;
    }
    else if (Input.GetKey(Right))
    {
      direction.x = 1;
    }
    else
    {
      direction.x = 0;
    }
    return direction;
  }

  public Vector2 getJumpInput()
  {
    if (Input.GetKey(Up))
    {
      direction.y = 1;
    }
    else
    {
      direction.y = 0;
    }
    return direction;
  }
  
}
