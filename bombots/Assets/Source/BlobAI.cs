using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class BlobAI : MonoBehaviour {

	private float targetRotation;

	public Animator animator;

	public SkinnedMeshRenderer currentColoredSkin;

	private float currentSpeed;
	private float currentRotation;

	enum States { Moving, Rotating, Waiting, Nothing, Victory };
	private States currentState;

	public List<Command> commandList;

	private float deltaTime;

	private float timeToEnd ; //when the current command ends, =time.now+command length of time
	private float idleDeathTime;
	private bool toBeDeleted;
	private float deleteCounter;
	
	private float delayDeleteTime;

	private int commandListeningToNum;

	// Use this for initialization
	void Start () {
		currentState= States.Nothing;
		currentSpeed = 0f;
		currentRotation = 0f;
		toBeDeleted = false;
		timeToEnd  = 1000000.0f;
		targetRotation = 0f;
		commandList = new List<Command>();
		deltaTime = Time.deltaTime;
		commandListeningToNum = 0;
		idleDeathTime = deltaTime + 5;
	}

	public void assignCommands(List<Command> commandList, Material color){
		this.commandList.Clear();
		foreach(Command command in commandList){
			this.commandList.Add(command);
			Debug.Log(command.ToString());
		}
		commandListeningToNum = 0;

		currentColoredSkin.material = color;
		if (currentState != States.Victory)
			currentState = States.Nothing;
		deltaTime = 0;

		GameObject currentPad = GameObject.Find("GameManager");
		//currentPad.GetComponent<PadController>().changeColor();

	}

	public void victory(GameObject goal) {
		currentState = States.Victory;
		currentSpeed = 0;
		targetRotation = goal.transform.rotation.eulerAngles.y;
	}

	// Update is called once per frame
	void Update () {
		Debug.Log ("Command Size is: " + commandList.Count);
		deltaTime += Time.deltaTime;


		//blobs start doing nothing. Then they check their command list but only if there is a command left.
		if(currentState == States.Nothing && commandListeningToNum < commandList.Count){
			//Debug.Log("deltaTime is: " + deltaTime + " timeToEnd is: " + timeToEnd);
			//how does the blob know if the command is wait, move, or turn?
			// ANSWER: the command has a "is rotation command" bool, and wait it just move w/ speed = 0.
			Command tempCommand = commandList[commandListeningToNum];
			Debug.Log("commandListeningToNum is: " +commandListeningToNum);
			//this is how we know the command is a wait command.
			if(tempCommand.Speed == 0 && tempCommand.RotationCommand == false){
				currentState = States.Waiting;
				currentSpeed = 0f;
			}else if(tempCommand.RotationCommand == true){
				Debug.Log("FOUND A ROTATION COMMAND");
				currentState = States.Rotating;
				currentSpeed = 0f;
				targetRotation = currentRotation + 90*tempCommand.RotationDegree;
			}else{
				currentState = States.Moving;
				currentSpeed = tempCommand.Speed;
			}
			Debug.Log("currentState is: " + currentState);
			timeToEnd = commandList[commandListeningToNum].Time;
		}


		switch(currentState)
		{
			case States.Moving:
				this.transform.Translate(Vector3.forward*currentSpeed*Time.deltaTime*3);
				Debug.Log("State is MOVING");
				animator.SetBool("isKablooey", false);
				animator.SetBool("isIdle", false);
				animator.SetBool("isScooching", true);
				break;
			case States.Nothing:
				Debug.Log("State is NOTHING");
				animator.SetBool("isKablooey", false);
				animator.SetBool("isIdle", true);
				animator.SetBool("isScooching", false);
				break;
			case States.Rotating:
				currentRotation = Mathf.Lerp(currentRotation, targetRotation, (Time.deltaTime*2));
				this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, currentRotation, this.transform.eulerAngles.z);
				if (Mathf.Floor(currentRotation) == targetRotation){
					currentState = States.Nothing;
				}
				Debug.Log("State is ROTATING: " + currentRotation + " " + targetRotation);
				animator.SetBool("isKablooey", false);
				animator.SetBool("isIdle", false);
				animator.SetBool("isScooching", true);
				break;
			case States.Waiting:
				Debug.Log("State is WAITING");
				animator.SetBool("isKablooey", false);
				animator.SetBool("isIdle", true);
				animator.SetBool("isScooching", false);
				break;
			case States.Victory:
				Debug.Log("State is VICTORY");
				// rotate to align with goal (presumably also toward user)
				if (Mathf.Abs(currentRotation - targetRotation) > 1){
					animator.SetBool("isKablooey", false);
					animator.SetBool("isIdle", true);
					animator.SetBool("isScooching", false);
					currentRotation = Mathf.Lerp(currentRotation, targetRotation, (Time.deltaTime*2));
					this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, currentRotation, this.transform.eulerAngles.z);
				}
				else {
					animator.SetBool("isIdle", false);
					animator.SetBool("isWin", true);
				}
				currentState = States.Victory;
				break;
			default:
				Debug.Log("State not found. . .");
				break;
		}

		//if the final command has ended, begin to idle
		if(deltaTime > timeToEnd && currentState != States.Nothing && currentState != States.Victory){
			Debug.Log("Setting State to nothing");
			//the dude won't explode cuz idedeathtime is always > deltatime
			idleDeathTime = deltaTime+3;
			currentSpeed = 0; 
			currentState = States.Nothing;
			commandListeningToNum++;
			deltaTime = 0;
			//targetRotation = 0;
			//currentRotation = 0;
			animator.SetBool("isKablooey", false);
			animator.SetBool("isIdle", true);
			animator.SetBool("isScooching", false);
		} 

		//if the slime idles for too long, KABLOOEY!
		if(currentState == States.Nothing && idleDeathTime <= deltaTime){
			Debug.Log("KABLOOEY!!!");
			animator.SetBool("isKablooey", true);
			animator.SetBool("isIdle", false);
			animator.SetBool("isScooching", false);
			toBeDeleted = true;
			deltaTime = 0;
			deleteCounter = Time.deltaTime + 3;
			idleDeathTime = 1000000f; // so that THIS function isn't called since idle will always be > deltatime
		}

		if(toBeDeleted){
			//Debug.Log("Waiting to be deleted: " + deleteCounter + " deltaTime: " + deltaTime);
			if(deltaTime > deleteCounter){
				Destroy(this.gameObject);
			}
		}
	}
}




















