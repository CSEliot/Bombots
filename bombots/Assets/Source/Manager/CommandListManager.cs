using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandListManager : MonoBehaviour {

  public const float PADDING_X = .8f;

  protected CommandManager commandManager;

  public Transform content;
  public Transform commandCard;

  protected float currentX;
  protected List<CommandMetadataComponent> listItems;


	// Use this for initialization
	void Start () {
    listItems = new List<CommandMetadataComponent>();
    commandManager = transform.GetComponent<CommandManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void AddCommand( CommandMetadata metadata )
  {
    Transform newCommandCard = (Transform) Instantiate( commandCard );
    newCommandCard.name = "Command Card " + listItems.Count;
    newCommandCard.parent = content;

    Vector3 position = newCommandCard.position;
    position.x = content.position.x;
    position.y = content.position.y;
    position.z = content.position.z;
    newCommandCard.position = position;

    Vector3 localPosition = newCommandCard.localPosition;
    localPosition.x = currentX;
    currentX += PADDING_X;
    newCommandCard.localPosition = localPosition;

    if ( listItems.Count > 0 )
    {
      Transform x = listItems[listItems.Count - 1].transform.FindChild("X");
      x.gameObject.SetActive(false);
    }

    CommandMetadataComponent component = newCommandCard.GetComponent<CommandMetadataComponent>();
    component.Metadata = metadata;
    listItems.Add( component );
    Transform newX = listItems[listItems.Count - 1].transform.FindChild("X");
    newX.gameObject.SetActive(true);

  }

  public void RemoveCommand()
  {
    CommandMetadataComponent component = listItems[listItems.Count - 1];
    Transform x = component.transform.FindChild ("X");

    commandManager.RemoveCommand(component.Metadata);
    x.gameObject.SetActive(false);

    listItems.Remove(component);
    GameObject.Destroy(component.gameObject);

    currentX -= PADDING_X;

    if (listItems.Count > 0)
    {
      Transform newX = listItems[listItems.Count - 1].transform.FindChild("X");
      newX.gameObject.SetActive(true);
    }

  }
}
