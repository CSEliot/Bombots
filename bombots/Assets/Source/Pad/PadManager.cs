using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PadManager : MonoBehaviour
{

  protected int currentPad;
  protected List<PadController> pads;

  // Use this for initialization
  void Start()
  {
    GameObject[] padObjects = GameObject.FindGameObjectsWithTag ( "Pad" );

    pads = new List<PadController> ( padObjects.Length );
    
    if ( padObjects.Length > 0 )
    {
      int i = 0;
      foreach ( GameObject padObject in padObjects )
      {
        PadController padController = padObject.GetComponent<PadController> ();
        padController.PadNumber = i;
        pads.Add ( padController );

        i++;
      }

      currentPad = 0;

      pads[ currentPad ].SetNext ();
    }
  }
  
  // Update is called once per frame
  void Update()
  {
  
  }

  public void AdvanceToNextPad()
  {
    if (pads.Count > 0)
    {
      currentPad++;
      pads[ currentPad ].SetNext ();
    }
  }

  public PadController GetCurrentPad()
  {
    return pads[ currentPad ];
  }

  public void AddCommandToCurrentPad( int speed, int time, bool rotationCommand, int rotationDegree )
  {
    if (pads.Count > 0)
    {
      pads[ currentPad ].AddCommand ( speed, time, rotationCommand, rotationDegree );
    }
  }
}
