using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	public float spawnRate = 1f;	// how often to spawn, in seconds

	private float lastSpawn;	// time of last spawn
	private GameObject baseBlob;	// base blob object

	// Use this for initialization
	void Start () {
		lastSpawn = Time.time;
		baseBlob = GameObject.Find("BlobBob");
	}
	
	// Update is called once per frame
	void Update () {
		float now = Time.time;
		if (now > lastSpawn + spawnRate) {
			lastSpawn = now;
			Instantiate(baseBlob, transform.position, transform.rotation);
		}
	}
}
