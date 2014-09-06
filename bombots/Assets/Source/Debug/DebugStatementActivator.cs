using UnityEngine;
using System.Collections;

public class DebugStatementActivator : TouchObject {

  protected bool isActive;
  protected DebugStatementComponent activeComponent;
  protected DebugStatementPool pool;

	// Use this for initialization
	void Start () 
  {
    GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");	
    pool = gameManager.GetComponent<DebugStatementPool>();
    isActive = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public override bool OnTap (GestureObject g)
  {
    if (!isActive)
    {
      activeComponent = pool.RequestObject();
      // TODO: Set active component's message to the command's text.
      activeComponent.Message = "LOL COMMAND";
      isActive = true;
    }
    else
    {
      pool.ReleaseObject(activeComponent);
      isActive = false;
    }
    return false;
  }
}
