using UnityEngine;
using System.Collections;

public struct Command {


	public Command(int speed, int time, bool rotationCommand, int rotationDegree){
		this.Speed = speed;
		this.Time = time;
		this.RotationCommand = rotationCommand;
		this.RotationDegree = rotationDegree;
	}

	public int Speed {get;private set;}
	public int Time {get;private set;}
	public bool RotationCommand {get;private set;}
	public int RotationDegree {get;private set;}
		

}

/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*public class Commands : MonoBehaviour {

	private List<Command> commandList;
	public int ListSize{get; private set;}

	// Use this for initialization
	void Start () {
	} 

	public Commands(){
		ListSize = 0;
		commandList = new List<Command>();
		Debug.Log("Testing");
	}


	public void addCommand(int speed, int time, bool rotationCommand, int rotationDegree){
		commandList.Add(new Command(speed, time, rotationCommand, rotationDegree));
		ListSize += 1;
	}

	public Command getCurrentCommand(){
		Command tempCommand = new Command(commandList[0]);
		commandList.RemoveAt(0);
		ListSize -= 1;
		return tempCommand;
	}
	
	public List<Commands> getAllCommands(){

		Command tempCommand = new Command(commandList[0]);
		commandList.RemoveAt(0);
		ListSize -= 1;
		return tempCommand;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
*/