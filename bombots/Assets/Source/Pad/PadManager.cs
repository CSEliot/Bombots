using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PadManager : MonoBehaviour
{

  protected int currentPad;
  protected PadController[] pads;

  public bool hasCompleted
  {
    get;
    private set;
  }

  // Use this for initialization
  void Start()
  {
    GameObject[] padObjects = GameObject.FindGameObjectsWithTag ( "Pad" );
    pads = new PadController[padObjects.Length];
    if ( padObjects.Length > 0 )
    {
      int i = 0;
      foreach ( GameObject padObject in padObjects )
      {
        PadController padController = padObject.GetComponent<PadController> ();
        padController.PadNumber = i;
        pads[padController.padIndex] = padController;

        i++;
      }

      currentPad = 0;
    }
  }
  
  // Update is called once per frame
  void Update()
  {
    if (!hasCompleted)
    {
      UpdateCurrentPad();
    }
  }

  protected void UpdateCurrentPad()
  {
    PadController nextPad = GetNextPad();

    if (nextPad == null)
    {
      hasCompleted = true;
    }
    else if (nextPad.hasBeenReached)
    {
      currentPad++;
    }
  }

  public PadController GetCurrentPad()
  {
    return pads[ currentPad ];
  }

  public PadController GetNextPad()
  {
    if (currentPad + 1 < pads.Length)
    {
      return pads[currentPad + 1];
    }

    return null;
  }

  public void AddCommandToCurrentPad( int speed, int time, bool rotationCommand, int rotationDegree )
  {
    if (pads.Length > 0)
    {
      pads[ currentPad ].AddCommand ( speed, time, rotationCommand, rotationDegree );
    }
  }

  public void RemoveCommandToCurrentPad( )
  {
    if (pads.Length > 0)
    {
      pads[ currentPad ].RemoveLastCommand();
    }
  }
}
