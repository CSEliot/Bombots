#pragma strict
var here : GameObject;

function Move () {


	this.transform.position = Vector3.Lerp(this.transform.position, here.transform.position, Time.deltaTime);
}
