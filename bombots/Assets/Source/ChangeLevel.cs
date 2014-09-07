using UnityEngine;
using System.Collections;

public class ChangeLevel : MonoBehaviour {
	public string levelName;
	public float loadDelay = 10f;

	private float loadTime = 0f;

	// Use this for initialization
	//void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (loadTime > 0 && Time.time > loadTime) {
			loadTime = 0;
			Application.LoadLevel(levelName);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag.Equals("Blob") && levelName.Length > 0)
			loadTime = Time.time + loadDelay;
	}
}
