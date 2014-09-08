using UnityEngine;
using System.Collections;

public class ChangeLevel : MonoBehaviour {
	private float loadDelay = 10f;	// switched to private for global override

	private float loadTime = 0f;

	// Use this for initialization
	//void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (loadTime > 0 && Time.time > loadTime) {
			loadTime = 0;
			int nextLevel = Application.loadedLevel + 1;
			if (nextLevel >= Application.levelCount)
				nextLevel = Application.levelCount-1;
			Application.LoadLevel(nextLevel);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (loadTime == 0 && other.tag.Equals("Blob")) {
			// delay before loading new level for win animation
			loadTime = Time.time + loadDelay;

			BlobAI blob = other.gameObject.GetComponent<BlobAI>();
			blob.victory(gameObject);
		}
	}
}
