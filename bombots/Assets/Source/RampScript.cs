using UnityEngine;
using System.Collections;

public class RampScript : MonoBehaviour {
	public float targetAngle;

	private GameObject laser;	// gate laser to turn on and off
	private Vector3 pivot;		// pivot point in world space
	private float probability;	// chance to move

	// Use this for initialization
	void Start () {
		targetAngle = -22.5f;
		probability = 0.5f;
		laser = GameObject.Find ("gate/laser_004");
		pivot = new Vector3(0,0,-1);
	}
	
	// Update is called once per frame
	void Update () {
		// move toward target angle
		float currentAngle = transform.eulerAngles.x;
		if (currentAngle > 180f) currentAngle -= 360f;
		float angleOffset = targetAngle - currentAngle;

		// change direction
		float angleMag = Mathf.Abs (angleOffset);
		if (angleMag < 0.001f && Random.value < probability*Time.deltaTime) {
			targetAngle = -targetAngle;
			laser.renderer.enabled = targetAngle > 0;
		}

		angleOffset = Mathf.Clamp(10f * Mathf.Sign(angleOffset)*Time.deltaTime, 
		                          -angleMag, angleMag);

		// rotate toward target
		transform.RotateAround(pivot, Vector3.right, angleOffset);

	}
}
