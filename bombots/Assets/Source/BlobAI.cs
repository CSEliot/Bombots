using UnityEngine;
using System.Collections;
using System;


public class BlobAI : MonoBehaviour {

	private float targetRotation;

	private float currentSpeed;
	private float currentRotation;

	enum States { Moving, Rotating, Waiting, Nothing};
	private int currentState;

	public Commands commandList;

	private DateTime timeToEnd ; //when the current command ends, =time.now+command length of time


	// Use this for initialization
	void Start () {
		currentState= (int)States.Nothing;
		currentSpeed = 0f;
		currentRotation = 0f;
		timeToEnd  = DateTime.Now;
		targetRotation = 0f;
		commandList = new Commands();
	
	}

	public void assignCommands(Commands commandList){
		this.commandList = commandList;
	}


	// Update is called once per frame
	void Update () {
		//get the current time and compare to the time the current command should end.
		DateTime timeRightNow = DateTime.Now;

		//if the final command has ended AND there are none stored in commandList remaining, idle
		if(timeRightNow.CompareTo(timeToEnd) > 0){
			currentSpeed = 0; 
			currentState = (int)States.Nothing;
		}

		//blobs start doing nothing. Then they check their command list but only if there is a command left.
		if(currentState == (int)States.Nothing && commandList.ListSize != 0){
			//how does the blob know if the command is wait, move, or turn?
			// ANSWER: the command has a "is rotation command" bool, and wait it just move w/ speed = 0.
			Commands.Command tempCommand = commandList.getCurrentCommand();
			//this is how we know the command is a wait command.
			if(tempCommand.Speed == 0){
				currentState = (int)States.Waiting;
				currentSpeed = 0f;
			}else if(tempCommand.RotationCommand == true){
				currentState = (int)States.Rotating;
				currentSpeed = 0f;
				targetRotation = 90*tempCommand.RotationDegree;
			}else{
				currentState = (int)States.Moving;
				currentSpeed = tempCommand.Speed;
			}

			//copy down the time we received the command.
			timeRightNow = DateTime.Now;
			timeToEnd = timeRightNow .AddSeconds(commandList.getCurrentCommand().Time);
		}

		switch(currentState)
		{
		case (int)States.Moving:
			this.transform.Translate(Vector3.forward*currentSpeed);
			if(timeToEnd.CompareTo(timeRightNow) > 0){
				currentState = (int)States.Nothing;
				currentSpeed = 0;
			}
			break;
		case (int)States.Nothing:
			break;
		case (int)States.Rotating: 
			currentRotation = Mathf.Lerp(currentRotation, targetRotation, (Time.deltaTime * 2));
			this.transform.eulerAngles = new Vector3(currentRotation, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
			if (Mathf.Floor(currentRotation) == targetRotation){
				currentState = (int)States.Nothing;
			}
			break;
		case (int)States.Waiting:
			if(timeToEnd.CompareTo(timeRightNow) > 0){
				currentState = (int)States.Nothing;
			}
			break;
		default:
			break;
		}

	}
}




















