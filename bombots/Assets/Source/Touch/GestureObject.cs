using UnityEngine;
using System.Collections;

public class GestureObject
{
  public enum GameGestureType
  {
    None,
    Tap,
    Pull,
    Drag,
    Down,
    Hold,
    Pinch
  }

  private GameGestureType state;
  private double startTime;
  private Vector3 start;
  private Vector3 end;
  private Vector3 screenStart;
  private Vector3 screenEnd;
  private int id;

  public GameGestureType GameGesture {
    get { return state; }
    set { state = value; }
  }

  public Vector3 StartPosition {
    get { return start; }
  }

  public Vector3 EndPosition {
    get { return end; }
    set { end = value; }
  }

  public Vector3 ScreenStart {
    get { return screenStart; }
    set { screenStart = value; }
  }

  public Vector3 ScreenEnd {
    get { return screenEnd; }
    set { screenEnd = value; }
  }

  public double StartTime {
    get { return startTime; }
  }

  public int Id {
    get { return id; }
  }

  // Use this for initialization
  void Start()
  {
  
  }

  public void Initialize( GameGestureType gameGestureType, Vector3 start, Vector3 end, Vector3 screenStart, Vector3 screenEnd, double startTime, int id )
  {
    this.state = GameGestureType.None;
    this.start = new Vector3 ();
    this.end = new Vector3 ();
    this.screenStart = new Vector3 ();
    this.screenEnd = new Vector3 ();

    this.state = gameGestureType;
    this.start = start;
    this.end = end;
    this.screenStart = screenStart;
    this.screenEnd = screenEnd;
    this.startTime = startTime;
    this.id = id;
  }
  
  // Update is called once per frame
  void Update()
  {
  
  }

  public bool Equals( GestureObject g )
  {
    return ( g.Id == this.Id );
  }

  public static Vector3 GetAveragePosition( GestureObject g1, GestureObject g2 )
  {
    float xavg, yavg;

    xavg = ( g1.EndPosition.x + g2.EndPosition.x ) / 2;
    yavg = ( g1.EndPosition.y + g2.EndPosition.y ) / 2;

    return new Vector3 ( xavg, yavg, 0.0f );
  }

  public override string ToString()
  {
    return this.Id + " " + this.GameGesture + "\n"
      + "Start: " + StartPosition + " "
      + "End: " + EndPosition;
  }
}
