using UnityEngine;
using System.Collections;

public class DebugStatementComponent : MonoBehaviour
{
  
  protected string message;

  public string Message
  {
    set { message = value; }
  }

  // Use this for initialization
  void Start ()
  {
    message = string.Empty;
  }
  
  // Update is called once per frame
  void Update ()
  {
    
  }

  public void Reset()
  {
    message = string.Empty;
  }

}

