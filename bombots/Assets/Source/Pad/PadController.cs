using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PadController : MonoBehaviour {

  public Material padColor;
  public int padIndex;
  private int currentColor;
	
  protected PadManager padManager;

  protected List<Command> commands;

  public List<Material> materials;
  public List<Material> laserColors;

  private void changeColor(){
	currentColor = (currentColor+1)%materials.Count;
	padColor = materials[currentColor];
	gameObject.transform.GetChild(0).FindChild("laser_004").GetComponent<SkinnedMeshRenderer>().material = laserColors[currentColor];
  }

  public int PadNumber
  {
    private get;
    set;
  }

  public bool hasBeenReached
  {
    get;
    private set;
  }

	// Use this for initialization
	void Start () {
		commands = new List<Command>();
	    GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
	    padManager = gameManager.GetComponent<PadManager>();
		currentColor = padIndex%8; //set the color equivalent to the pad's index
		gameObject.transform.GetChild(0).FindChild("laser_004").GetComponent<SkinnedMeshRenderer>().material = laserColors[currentColor];
		padColor = materials[currentColor];
	}
	
	// Update is called once per frame
	void Update () {
    print ("PAD " + padIndex + " LENGTH: " + commands.Count);
	}

  void OnTriggerEnter(Collider other)
  {
    if (other.tag.Equals("Blob"))
    {
      BlobAI blob = other.gameObject.GetComponent<BlobAI>();
      blob.assignCommands(commands, padColor);

      hasBeenReached = true;
    }
  }

  public void AddCommand(int speed, int time, bool rotationCommand, int rotationDegree)
  {
	changeColor();
    commands.Add( new Command(speed, time, rotationCommand, rotationDegree));
  }
}
