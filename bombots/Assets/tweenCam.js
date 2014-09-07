#pragma strict

var targCam : GameObject;
var speed : float = .7;

var spotlight : GameObject;

var delayGo : float = 6;
var GoCam : GameObject;

var finalSpotlook : GameObject;

private var timey : float;
function Start () {

}

function Update () {

	if(speed < 0){
	
		this.transform.position = Vector3.Lerp(this.transform.position, targCam.transform.position, Time.deltaTime * .005);
	
	}else{
	
		this.transform.position = Vector3.Lerp(this.transform.position, targCam.transform.position, Time.deltaTime * speed);
	
	}

	this.transform.LookAt(spotlight.transform);
	
	speed += .1 * Time.deltaTime;
	
	timey += Time.deltaTime;
	
	
	if(timey >= delayGo){
	
		speed = -.2;
		targCam = GoCam;
		timey = -10;
		spotlight = finalSpotlook;
	}
}
