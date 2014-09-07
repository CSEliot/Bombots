using UnityEngine;
using System.Collections;

public class RampScript : MonoBehaviour {
	public float targetAngle = -22.5f;
	public float speed = 10f;
	public Vector3 pivot = new Vector3(0,0,-6);		// pivot point in world space

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
		if (angleMag < 0.001f) {
			targetAngle = -targetAngle;
			//laser.renderer.enabled = targetAngle > 0;
		}

		angleOffset = Mathf.Clamp(Mathf.Sign(angleOffset) * speed * Time.deltaTime, 
		                          -angleMag, angleMag);

		// rotate toward target
		transform.RotateAround(pivot, Vector3.right, angleOffset);

	}
}
