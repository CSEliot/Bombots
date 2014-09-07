using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class BlobAI : MonoBehaviour {

	private float targetRotation;

	private float currentSpeed;
	private float currentRotation;

	enum States { Moving, Rotating, Waiting, Nothing};
	private States currentState;

	public List<Command> commandList;

	private float deltaTime;

	private float timeToEnd ; //when the current command ends, =time.now+command length of time

	private int commandListeningToNum;

	// Use this for initialization
	void Start () {
		currentState= States.Nothing;
		currentSpeed = 0f;
		currentRotation = 0f;
		timeToEnd  = 0.0f;
		targetRotation = 0f;
		commandList = new List<Command>();
		deltaTime = Time.deltaTime;
		commandListeningToNum = 0;
	}

	public void assignCommands(List<Command> commandList){
		this.commandList.Clear();
		foreach(Command command in commandList){
			this.commandList.Add(command);
		}
		commandListeningToNum = 0;
	}


	// Update is called once per frame
	void Update () {
		deltaTime += Time.deltaTime;


		//blobs start doing nothing. Then they check their command list but only if there is a command left.
		if(currentState == States.Nothing && commandListeningToNum < commandList.Count){
			//Debug.Log("deltaTime is: " + deltaTime + " timeToEnd is: " + timeToEnd);
			//how does the blob know if the command is wait, move, or turn?
			// ANSWER: the command has a "is rotation command" bool, and wait it just move w/ speed = 0.
			Command tempCommand = commandList[commandListeningToNum];
			Debug.Log("commandListeningToNum is: " +commandListeningToNum);
			//this is how we know the command is a wait command.
			if(tempCommand.Speed == 0){
				currentState = States.Waiting;
				currentSpeed = 0f;
			}else if(tempCommand.RotationCommand == true){
				currentState = States.Rotating;
				currentSpeed = 0f;
				targetRotation = 90*tempCommand.RotationDegree;
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
				this.transform.Translate(Vector3.forward*currentSpeed*Time.deltaTime);
				Debug.Log("State is MOVING");
				break;
			case States.Nothing:
				Debug.Log("State is NOTHING");
				break;
			case States.Rotating:
				currentRotation = Mathf.Lerp(currentRotation, targetRotation, (Time.deltaTime * 2));
				this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, currentRotation, this.transform.eulerAngles.z);
				if (Mathf.Floor(currentRotation) == targetRotation){
					currentState = States.Nothing;
				}
				Debug.Log("State is ROTATING");
				break;
			case States.Waiting:
				Debug.Log("State is WAITING");
				break;
			default:
				break;
		}

		//if the final command has ended, begin to idle
		if(deltaTime > timeToEnd){
			currentSpeed = 0; 
			currentState = States.Nothing;
			commandListeningToNum++;
			deltaTime = 0;
		}
	}
}




















