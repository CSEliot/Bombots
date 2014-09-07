using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	public float spawnRate;	// how often to spawn, in seconds
	public float lastSpawn;	// time of last spawn

	// Use this for initialization
	void Start () {
		spawnRate = 1f;
		lastSpawn = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float now = Time.time;
		if (now > lastSpawn + spawnRate) {
			lastSpawn = now;
			Instantiate(GameObject.Find("Blob"), transform.position, Quaternion.identity);
		}
	}
}
