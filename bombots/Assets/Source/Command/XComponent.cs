using UnityEngine;
using System.Collections;

public class XComponent : MonoBehaviour {

  protected GameObject gameManager;

	// Use this for initialization
	void Start () {
    gameManager = GameObject.FindGameObjectWithTag("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void RemoveCommand()
  {
    gameManager.GetComponent<CommandListManager>().RemoveCommand();
  }
}
