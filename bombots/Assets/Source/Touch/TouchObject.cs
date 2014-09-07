using UnityEngine;
using System.Collections;

public class TouchObject : MonoBehaviour
{

  // Use this for initialization
  void Start ()
  {
  
  }
  
  // Update is called once per frame
  void Update ()
  {
  
  }

  public virtual bool OnTap (GestureObject g)
  {
    return false;
  }

  public virtual bool OnDrag (GestureObject g)
  {
    return false;
  }

  public virtual bool OnPull (GestureObject g)
  {
    return false;
  }

  public virtual bool OnHold (GestureObject g)
  {
    return false;
  }

  public virtual bool OnDown (GestureObject g)
  {
    return false;
  }

  public virtual bool OnPinch (GestureObject g)
  {
    return false;
  }
}
