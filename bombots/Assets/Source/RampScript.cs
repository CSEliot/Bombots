using UnityEngine;
using System.Collections;

public class RampScript : MonoBehaviour {
	public float targetAngle = -22.5f;
	public Vector3 pivot = new Vector3(0,0,-6);		// pivot point in world space
	public float probability = 0.5f;				// chance to move

	private GameObject laser;	// gate laser to turn on and off

	// Use this for initialization
	void Start () {
		laser = GameObject.Find ("gate/laser_004");
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
