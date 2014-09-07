#pragma strict

var targCam : GameObject;
var speed : float = .7;

var spotlight : GameObject;


function Start () {

}

function Update () {


	this.transform.position = Vector3.Lerp(this.transform.position, targCam.transform.position, Time.deltaTime * speed);

	this.transform.LookAt(spotlight.transform);
	
	speed += .1 * Time.deltaTime;
}
