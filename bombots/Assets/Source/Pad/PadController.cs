using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PadController : MonoBehaviour {

  protected enum State
  {
    IDLE,
    NEXT,
    COMPLETE
  }

  protected PadManager padManager;

  protected State state;

  protected List<Command> commands;

  public int PadNumber
  {
    private get;
    set;
  }

	// Use this for initialization
	void Start () {
    GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
    padManager = gameManager.GetComponent<PadManager>();
    state = State.IDLE;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnTriggerEnter(Collider other)
  {
    if (other.tag.Equals("Blob"))
    {
      if (state == State.NEXT)
      {
        padManager.AdvanceToNextPad();
        state = State.COMPLETE;
      }

      // give the blobs their commands
      BlobAI blob = other.gameObject.GetComponent<BlobAI>();

      blob.assignCommands(commands);
    }
  }

  public void SetNext()
  {
    if (state == State.IDLE)
    {
      state = State.NEXT;
    }
  }

  public void AddCommand(int speed, int time, bool rotationCommand, int rotationDegree)
  {
    commands.Add( new Command(speed, time, rotationCommand, rotationDegree));
  }
}
