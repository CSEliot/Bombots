﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PadController : MonoBehaviour {

  public Material padColor;
  public int padIndex;

  protected PadManager padManager;

  protected List<Command> commands;

  public int PadNumber
  {
    private get;
    set;
  }

  public bool hasBeenReached
  {
    get;
    private set;
  }

	// Use this for initialization
	void Start () {
		commands = new List<Command>();
	    GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
	    padManager = gameManager.GetComponent<PadManager>();
	}
	
	// Update is called once per frame
	void Update () {
    print ("PAD " + padIndex + " LENGTH: " + commands.Count);
	}

  void OnTriggerEnter(Collider other)
  {
    if (other.tag.Equals("Blob"))
    {
      BlobAI blob = other.gameObject.GetComponent<BlobAI>();

      blob.assignCommands(commands, padColor);

      hasBeenReached = true;
    }
  }

  public void AddCommand(int speed, int time, bool rotationCommand, int rotationDegree)
  {
    commands.Add( new Command(speed, time, rotationCommand, rotationDegree));
  }
}
