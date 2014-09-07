using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PadManager : MonoBehaviour
{

  protected int currentPad;
  protected PadController[] pads;

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

      pads[ currentPad ].SetNext ();
    }
  }
  
  // Update is called once per frame
  void Update()
  {
  
  }

  public void AdvanceToNextPad()
  {
    if (pads.Length > 0)
    {
      currentPad++;
	  if (currentPad >= pads.Length)
	  {
        pads[ currentPad ].SetNext ();
	  }
    }
  }

  public PadController GetCurrentPad()
  {
    return pads[ currentPad ];
  }

  public void AddCommandToCurrentPad( int speed, int time, bool rotationCommand, int rotationDegree )
  {
    if (pads.Length > 0)
    {
      pads[ currentPad ].AddCommand ( speed, time, rotationCommand, rotationDegree );
    }
  }
}
