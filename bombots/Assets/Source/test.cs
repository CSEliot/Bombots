using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class test : MonoBehaviour {

	private bool gaveCommands;
	private GameObject datBlob;
	private List<Command> testList;


	// Use this for initialization
	void Start () {
		gaveCommands = false;
		datBlob = GameObject.FindGameObjectWithTag("Player");
		testList = new List<Command>();
		testList.Add(new Command(0, 5, false, 0));
		testList.Add(new Command(1, 4, false, 0));
		testList.Add(new Command(2, 5, true, 1));
	}
	
	// Update is called once per frame
	void Update () {
	
		if(!gaveCommands){
			gaveCommands = true;
			datBlob.GetComponent<BlobAI>().assignCommands(testList);
		}
	}
}
