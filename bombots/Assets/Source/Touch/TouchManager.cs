using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchManager : MonoBehaviour
{

  protected GameObject touchedObject;
  private Dictionary<int, GestureObject> gestures;
  private Dictionary<int, GameObject> boundObjects;
  private List<GestureObject> remove;

  // Use this for initialization
  void Start()
  {
    gestures = new Dictionary<int, GestureObject> ();
    boundObjects = new Dictionary<int, GameObject> ();
    remove = new List<GestureObject> ();

    Input.multiTouchEnabled = true;
  }

  // Update is called once per frame
  void Update()
  {
    remove.Clear ();
    int touches = Input.touchCount;
    for ( int i = 0; i < touches; i++ )
    {
      Touch touch = Input.GetTouch ( i );
      ConvertToGestureObject ( touch );
    }

    ConvertMouseToGestureObject ();

    foreach ( GestureObject g in gestures.Values )
    {
      if ( g != null )
      {
        boundObjects.TryGetValue ( g.Id, out touchedObject );
        if ( touchedObject != null )
        {
          TouchObject touchObj = touchedObject.GetComponent<TouchObject> ();
          if ( touchObj != null && touchObj.gameObject.activeSelf )
          {
            if ( g.GameGesture == GestureObject.GameGestureType.Tap )
            {
              touchObj.OnTap ( g );
            }
            else
            if ( g.GameGesture == GestureObject.GameGestureType.Pull )
            {
              touchObj.OnPull ( g );
            }
            else
            if ( g.GameGesture == GestureObject.GameGestureType.Drag )
            {
              touchObj.OnDrag ( g );
            }
            else
            if ( g.GameGesture == GestureObject.GameGestureType.Hold )
            {
              touchObj.OnHold ( g );
            }
            else
            if ( g.GameGesture == GestureObject.GameGestureType.Pinch )
            {
              touchObj.OnPinch ( g );
            }
          }
        }
        else
        {
          Ray ray = Camera.main.ScreenPointToRay ( g.ScreenEnd );
          var hits = Physics.RaycastAll ( ray, 100.0f );
          foreach ( RaycastHit hit in hits )
          {
            touchedObject = hit.transform.gameObject;
            TouchObject touchObj = touchedObject.GetComponent<TouchObject> ();
            if ( touchObj != null )
            {
              bool actionPerformed = false;
              if ( g.GameGesture == GestureObject.GameGestureType.Tap )
              {
                actionPerformed = touchObj.OnTap ( g );
              }
              else
              if ( g.GameGesture == GestureObject.GameGestureType.Pull )
              {
                actionPerformed = touchObj.OnPull ( g );
              }
              else
              if ( g.GameGesture == GestureObject.GameGestureType.Drag )
              {
                actionPerformed = touchObj.OnDrag ( g );
                if ( actionPerformed )
                  boundObjects.Add ( g.Id, touchedObject );
              }
              else
              if ( g.GameGesture == GestureObject.GameGestureType.Hold )
              {
                actionPerformed = touchObj.OnHold ( g );
                if ( actionPerformed )
                  boundObjects.Add ( g.Id, touchedObject );
              }
              else
              if ( g.GameGesture == GestureObject.GameGestureType.Pinch )
              {
                actionPerformed = touchObj.OnPinch ( g );
              }
              if ( actionPerformed )
                break;
            }
          }
        }
        if ( g.GameGesture == GestureObject.GameGestureType.Tap )
        {
          remove.Add ( g );
        }
        else
        if ( g.GameGesture == GestureObject.GameGestureType.Pull )
        {
          remove.Add ( g );
        }
        else
        if ( g.GameGesture == GestureObject.GameGestureType.Pinch )
        {
          remove.Add ( g );
        }
      }
    }

    foreach ( GestureObject g in remove )
    {
      gestures.Remove ( g.Id );
      boundObjects.Remove ( g.Id );
    }
  }

  protected GestureObject ConvertToGestureObject( Touch t )
  {
    if ( t.phase == TouchPhase.Began )
    {
      Vector3 worldStartPosition = Camera.main.ScreenToWorldPoint ( t.position );
      GestureObject g = new GestureObject ();
      g.Initialize ( GestureObject.GameGestureType.Down, worldStartPosition, worldStartPosition, t.position, t.position, Time.time, t.fingerId );
      gestures.Add ( g.Id, g );
      return g;
    }
    else
    {
      GestureObject g;
      gestures.TryGetValue ( t.fingerId, out g );

      Vector3 worldEndPosition = Camera.main.ScreenToWorldPoint ( t.position );
      if ( g != null )
      {
        if ( t.phase == TouchPhase.Ended )
        {
          if ( g.GameGesture == GestureObject.GameGestureType.Down || g.GameGesture == GestureObject.GameGestureType.Hold
            || g.GameGesture == GestureObject.GameGestureType.None )
          {
            g.GameGesture = GestureObject.GameGestureType.Tap;
          }
          else
          if ( g.GameGesture == GestureObject.GameGestureType.Drag )
          {
            g.GameGesture = GestureObject.GameGestureType.Pull;
            g.EndPosition = worldEndPosition;
            g.ScreenEnd = t.position;
          }
        }
        else
        if ( t.phase == TouchPhase.Moved )
        {
          if ( g.GameGesture == GestureObject.GameGestureType.Down ||
            g.GameGesture == GestureObject.GameGestureType.Drag ||
            g.GameGesture == GestureObject.GameGestureType.None )
          {
            g.GameGesture = GestureObject.GameGestureType.Drag;
            g.EndPosition = worldEndPosition;
            g.ScreenEnd = t.position;
          }
        }
        else
        if ( t.phase == TouchPhase.Stationary )
        {
          if ( g.GameGesture == GestureObject.GameGestureType.Down ||
            g.GameGesture == GestureObject.GameGestureType.None )
          {
            if ( Time.time - g.StartTime > 2 )
            {
              g.GameGesture = GestureObject.GameGestureType.Hold;
            }
            else
            {
              g.GameGesture = GestureObject.GameGestureType.Down;
            }
            g.EndPosition = worldEndPosition;
            g.ScreenEnd = t.position;
          }
        }
      }
      else
      {
        g = new GestureObject ();
        g.Initialize ( GestureObject.GameGestureType.None, worldEndPosition, worldEndPosition, t.position, t.position, Time.time, t.fingerId );
        gestures.Add ( g.Id, g );
      }

      return g;
    }

  }

  private void ConvertMouseToGestureObject()
  {
    GestureObject mouseGesture;
    try
    {
      gestures.TryGetValue ( -1, out mouseGesture );

      if ( Input.GetMouseButton ( 1 ) )
      {
        Vector3 worldStartPosition = Camera.main.ScreenToWorldPoint ( Input.mousePosition );
        mouseGesture = new GestureObject ();
        mouseGesture.Initialize ( GestureObject.GameGestureType.Pinch, worldStartPosition, worldStartPosition, Input.mousePosition, Input.mousePosition, Time.time, -1 );
        gestures.Add ( mouseGesture.Id, mouseGesture );
      }
      else
      if ( Input.GetMouseButton ( 0 ) )
      {
        Vector3 worldEndPosition = Camera.main.ScreenToWorldPoint ( Input.mousePosition );
        if ( mouseGesture == null )
        {
          mouseGesture = new GestureObject ();
          mouseGesture.Initialize ( GestureObject.GameGestureType.Down, worldEndPosition, worldEndPosition, Input.mousePosition, Input.mousePosition, Time.time, -1 );
          gestures.Add ( mouseGesture.Id, mouseGesture );
        }
        else
        {
          if ( !worldEndPosition.Equals ( mouseGesture.StartPosition ) )
          {
            mouseGesture.GameGesture = GestureObject.GameGestureType.Drag;
          }
          else
          {
            if ( Time.time - mouseGesture.StartTime > 2 )
            {
              mouseGesture.GameGesture = GestureObject.GameGestureType.Hold;
            }
            else
            {
              mouseGesture.GameGesture = GestureObject.GameGestureType.Down;
            }
          }
          mouseGesture.EndPosition = worldEndPosition;
          mouseGesture.ScreenEnd = Input.mousePosition;
        }
      }
      else
      if ( Input.GetMouseButtonUp ( 0 ) )
      {
        Vector3 worldEndPosition = Camera.main.ScreenToWorldPoint ( Input.mousePosition );
        if ( mouseGesture.GameGesture == GestureObject.GameGestureType.Drag )
        {
          mouseGesture.GameGesture = GestureObject.GameGestureType.Pull;
          mouseGesture.EndPosition = worldEndPosition;
          mouseGesture.ScreenEnd = Input.mousePosition;
        }
        else
        if ( mouseGesture.GameGesture == GestureObject.GameGestureType.Down
          || mouseGesture.GameGesture == GestureObject.GameGestureType.Hold )
        {
          mouseGesture.GameGesture = GestureObject.GameGestureType.Tap;
          mouseGesture.EndPosition = worldEndPosition;
          mouseGesture.ScreenEnd = Input.mousePosition;
        }
      }
    }
    catch
    {
      mouseGesture = null;
    }
  }
}
