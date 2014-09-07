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

  protected struct CommandMetadata
  {
    public CommandType type;
    public int speed;
    public int time;
    public int rotation;
  }


  protected PadManager padManager;
  protected List<Command> currentCommands;

  protected CommandMetadata newCommand;

  public GameObject coreInstructions;
  public GameObject paramForwards;
  public GameObject paramStop;
  public GameObject paramTurn;

	// Use this for initialization
	void Start () {
    padManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PadManager>();

    newCommand = new CommandMetadata();
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

  public void CommandLine()
  {
    if (!coreInstructions.activeSelf && !paramStop.activeSelf && !paramTurn.activeSelf
        && !paramTurn.activeSelf)
    {
      coreInstructions.SetActive(true);
    }
  }

  public void Stop()
  {
    newCommand.speed = 0;
    newCommand.rotation = 0;
    newCommand.type = CommandType.WAIT;
    coreInstructions.SetActive(false);
    paramStop.SetActive(true);
  }

  public void Turn()
  {
    newCommand.speed = 1;
    newCommand.time = 0;
    newCommand.type = CommandType.TURN;
    coreInstructions.SetActive(false);
    paramTurn.SetActive(true);
  }

  public void Forward()
  {
    newCommand.rotation = 0;
    newCommand.type = CommandType.MOVE;
    coreInstructions.SetActive(false);
    paramForwards.SetActive(true);
  }

  public void MedSpeed()
  {
    newCommand.speed = 2;
    paramForwards.SetActive(false);
    paramStop.SetActive(true);
  }

  public void LoSpeed()
  {
    newCommand.speed = 1;
    paramForwards.SetActive(false);
    paramStop.SetActive(true);
  }

  public void HiSpeed()
  {
    newCommand.speed = 3;
    paramForwards.SetActive(false);
    paramStop.SetActive(true);
  }

  public void MedWait()
  {
    newCommand.time = 2;
    AddCommand(newCommand.type, newCommand.speed, newCommand.time, newCommand.rotation);
    paramStop.SetActive(false);
  }

  public void LoWait()
  {
    newCommand.time = 1;
    AddCommand(newCommand.type, newCommand.speed, newCommand.time, newCommand.rotation);
    paramStop.SetActive(false);    
  }

  public void HiWait()
  {
    newCommand.time = 3;
    AddCommand(newCommand.type, newCommand.speed, newCommand.time, newCommand.rotation);
    paramStop.SetActive(false);
  }

  public void LeftTurn()
  {
    newCommand.rotation = 90;
    AddCommand(newCommand.type, newCommand.speed, newCommand.time, newCommand.rotation);
    paramTurn.SetActive(false);
  }

  public void RightTurn()
  {
    newCommand.rotation = -90;
    AddCommand(newCommand.type, newCommand.speed, newCommand.time, newCommand.rotation);
    paramTurn.SetActive(false);
  }
}
