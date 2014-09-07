using UnityEngine;
using System.Collections;

public class RampScript : MonoBehaviour {
	public float targetAngle;

	// Use this for initialization
	void Start () {
		targetAngle = -22.5f;
	}
	
	// Update is called once per frame
	void Update () {
		// move toward target angle
		float currentAngle = transform.eulerAngles.x;
		if (currentAngle > 180f) currentAngle -= 360f;
		float angleOffset = targetAngle - currentAngle;

		// change direction
		float angleMag = Mathf.Abs (angleOffset);
		if (angleMag < 0.001f && Random.value < 0.5f*Time.deltaTime) {
			targetAngle = -targetAngle;

			GameObject laser = GameObject.Find ("/gate/laser_004");
			laser.renderer.enabled = targetAngle > 0;
		}

		angleOffset = Mathf.Clamp(10f * Mathf.Sign(angleOffset)*Time.deltaTime, 
		                          -angleMag, angleMag);

		// rotate toward target
		Vector3 pivot = new Vector3(0,0,-1);
		transform.RotateAround(pivot, Vector3.right, angleOffset);

	}
}
