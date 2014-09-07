using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	public float spawnRate;	// how often to spawn, in seconds
	public float lastSpawn;	// time of last spawn
	public GameObject baseBlob;	// base blob object

	// Use this for initialization
	void Start () {
		spawnRate = 1f;
		lastSpawn = Time.time;
		baseBlob = GameObject.Find("BlobBob");
	}
	
	// Update is called once per frame
	void Update () {
		float now = Time.time;
		if (now > lastSpawn + spawnRate) {
			lastSpawn = now;
			Instantiate(baseBlob, transform.position, Quaternion.identity);
		}
	}
}
