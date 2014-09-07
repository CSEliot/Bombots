using UnityEngine;
using System.Collections;

public class ChangeLevel : MonoBehaviour {
	public int nextLevel=0;
	public float loadDelay = 12f;

	private float loadTime = 0f;

	// Use this for initialization
	//void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (loadTime > 0 && Time.time > loadTime) {
			loadTime = 0;
			Application.LoadLevel(nextLevel);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (nextLevel > 0 && other.tag.Equals("Blob")) {
			// delay before loading new level for win animation
			loadTime = Time.time + loadDelay;

			BlobAI blob = other.gameObject.GetComponent<BlobAI>();
			blob.victory(gameObject);
		}
	}
}
