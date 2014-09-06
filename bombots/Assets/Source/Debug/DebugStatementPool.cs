using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugStatementPool : MonoBehaviour {

  protected struct DebugStatementMetadata
  {
    public DebugStatementComponent component;
    public bool used;

    public DebugStatementMetadata(DebugStatementComponent component, bool used)
    {
      this.component = component;
      this.used = used;
    }
  }

  protected const int MAX = 50;

  public Transform debugStatement;

  // list of key value pairs where the key is whether the component is active and the value is the component itself.
  protected List<DebugStatementMetadata> pool;
  protected int currentNode;
  protected int usedDebugStatements;
  
  // Use this for initialization
  void Start()
  {
    GameObject uiCamera = GameObject.FindGameObjectWithTag("UI Camera");
    pool = new List<DebugStatementMetadata>();
    for ( int i = 0; i < MAX; i++ )
    {
      Transform debugStatementObj = (Transform) Instantiate( debugStatement );
      debugStatementObj.name = "Debug Statement " + i;
      debugStatementObj.gameObject.SetActive(false);
      debugStatementObj.parent = uiCamera.transform;
      DebugStatementComponent component = debugStatementObj.GetComponent<DebugStatementComponent>();
      pool.Add( new DebugStatementMetadata(component, false) );
    }
    currentNode = 0;
    usedDebugStatements = 0;
  }
  
  // Update is called once per frame
  void Update()
  {

  }

  public DebugStatementComponent RequestObject()
  {
    // do not even attempt to do this if all objects are in use.
    if (usedDebugStatements < MAX)
    {
      while (usedDebugStatements < MAX)
      {
        DebugStatementMetadata node = pool[currentNode];
        // check to see if the item is being used, if not put it up.
        if (node.used)
        {
          usedDebugStatements++;
          node.used = true;
          node.component.gameObject.SetActive( true );
          return node.component;
        }

        currentNode++;
        if (currentNode > MAX)
        {
          currentNode = 0;
        }
      }
    }

    return null;
  }

  public void ReleaseObject(DebugStatementComponent debugStatementComponent)
  {
    for (int i = 0; i < MAX; i++)
    {
      DebugStatementMetadata node = pool[i];
      
      if ( node.component.gameObject != null && node.component.gameObject.Equals(debugStatementComponent.gameObject))
      {
        usedDebugStatements--;
        node.used = false;
        node.component.gameObject.SetActive( false );
        node.component.Reset();
      }
    }
  }
}
