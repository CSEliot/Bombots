#pragma strict


var MoveSpeed : float = 3;


var aniMaster : Animator;
var isDead : boolean;


var timerNum : float;
var lengthTimer : float;


var Instructions : Instruction[];
var currentInstruction : Instruction;
var currentInsNumber : float;

enum aiState{idle, moving, turning, dead}
var state : aiState;


InvokeRepeating("StateLogic", 0.0, 0.05);


class Instruction {

	var IfTag : String;
	var ThenAction : Action[];
	
	var curActionNum : float;
}

class Action {

	var isForward : boolean;
	var isRotation : boolean;
	var isWaiting : boolean;
	
	var timeLength : float;
}


function Start () {
	
	
	
	
	yield StateMachine();
}

function Update(){

	timerNum += Time.deltaTime;
	
	rigidbody.velocity = transform.forward * MoveSpeed;
	
}


function StateLogic () {

	currentInstruction = Instructions[currentInsNumber];

	
	if(timerNum >= currentInstruction.ThenAction[currentInstruction.curActionNum].timeLength){
	
		nextAction();
		timerNum = 0;
	}
	
	if(isDead == true)
		state = aiState.dead;
	
	else if(currentInstruction.ThenAction[currentInstruction.curActionNum].isForward == true)
		state = aiState.moving;

	else if(currentInstruction.ThenAction[currentInstruction.curActionNum].isRotation == true)
		state = aiState.turning;
			
	else if(currentInstruction.ThenAction[currentInstruction.curActionNum].isWaiting == true)	
		state =	aiState.idle;
}


function nextInstruction(){

	
	if(currentInsNumber + 1 < Instructions.Length){
	
		currentInsNumber ++;
		
		currentInstruction = Instructions[currentInsNumber];
	
	}

}


function nextAction(){

	
	if(currentInstruction.curActionNum + 1 <= Instructions.Length){
	
		
		currentInstruction.curActionNum ++;
		Instructions[currentInsNumber].curActionNum = currentInstruction.curActionNum;
		
		timerNum = 0;
	
	
	}else{
	
	
		nextInstruction();
	
	}

}

function StateMachine(){
	while(true){
		switch(state){
			case aiState.dead:
			 	Dead();
				break;			
			
			case aiState.idle:
				Idle();
				break;
				
			case aiState.turning:
				Turning();
				break;
				
			case aiState.moving:
				Moving();
				break;
		
		}
		yield;
	}
}

function Idle(){

	aniMaster.SetBool("isIdle", true);
	aniMaster.SetBool("isScooching", false);
	aniMaster.SetBool("isKablooey", false);
	
	MoveSpeed = 0;
}

function Moving(){

	aniMaster.SetBool("isIdle", false);
	aniMaster.SetBool("isScooching", true);
	aniMaster.SetBool("isKablooey", false);


	MoveSpeed = 3;


}

function Turning(){

	aniMaster.SetBool("isIdle", false);
	aniMaster.SetBool("isScooching", true);
	aniMaster.SetBool("isKablooey", false);


	
}


function Dead(){

	aniMaster.SetBool("isIdle", false);
	aniMaster.SetBool("isScooching", false);
	aniMaster.SetBool("isKablooey", true);

}




