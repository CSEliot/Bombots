using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Commands : MonoBehaviour {

	private List<Command> commandList;
	public int ListSize{get; private set;}

	// Use this for initialization
	void Start () {
		commandList = new List<Command>();
		ListSize = 0;
	}


	public class Command { 
		public Command(int speed, int time, bool rotationCommand, int rotationDegree){
			this.Speed = speed;
			this.Time = time;
			this.RotationCommand = rotationCommand;
			this.RotationDegree = rotationDegree;
		}
		public Command(Command command){
			this.Speed = command.Speed;
			this.Time = command.Time;
			this.RotationCommand = command.RotationCommand;
			this.RotationDegree = command.RotationDegree;
		}
		public int Speed {get;private set;}
		public int Time {get;private set;}
		public bool RotationCommand {get;private set;}
		public int RotationDegree {get;private set;}

	}

	void addCommand(int speed, int time, bool rotationCommand, int rotationDegree){
		commandList.Add(new Command(speed, time, rotationCommand, rotationDegree));
		ListSize += 1;
	}

	public Command getCurrentCommand(){
		Command tempCommand = new Command(commandList[0]);
		commandList.RemoveAt(0);
		ListSize -= 1;
		return tempCommand;
	}
	


	// Update is called once per frame
	void Update () {
	
	}
}
