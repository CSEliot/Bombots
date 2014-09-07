using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandManager : MonoBehaviour {

  public enum CommandType
  {
    MOVE,
    TURN,
    WAIT
  }


  protected PadManager padManager;
  protected List<Command> currentCommands;

	// Use this for initialization
	void Start () {
    padManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PadManager>();
	}
	
	// Update is called once per frame
	void Update () 
  {
	
	}

  public void AddCommand(CommandType type, int speed, int time, int rotation)
  {
    switch (type)
    {
    case CommandType.MOVE:
      padManager.AddCommandToCurrentPad(speed, time, false, rotation);
      break;
    case CommandType.TURN:
      padManager.AddCommandToCurrentPad(0, time, false, rotation);
      break;
    case CommandType.WAIT:
      padManager.AddCommandToCurrentPad(speed, time, true, rotation);
      break;
    }
  }
}
