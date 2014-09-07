using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandListManager : MonoBehaviour {

  public const float PADDING_X = 1.2f;

  public Transform scrollable;
  public Transform commandCard;

  protected float currentX;
  protected List<CommandMetadataComponent> listItems;


	// Use this for initialization
	void Start () {
    listItems = new List<CommandMetadataComponent>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void AddCommand( CommandMetadata metadata )
  {
    Transform newCommandCard = (Transform) Instantiate( commandCard );
    newCommandCard.name = "Command Card " + listItems.Count;
    newCommandCard.parent = scrollable;

    Vector3 position = newCommandCard.position;
    position.x = scrollable.position.x;
    position.y = scrollable.position.y;
    position.z = scrollable.position.z;
    newCommandCard.position = position;

    Vector3 localPosition = newCommandCard.localPosition;
    localPosition.x = currentX;
    currentX += PADDING_X;
    newCommandCard.localPosition = localPosition;


    CommandMetadataComponent component = newCommandCard.GetComponent<CommandMetadataComponent>();
    component.Metadata = metadata;
    listItems.Add( component );
  }

  void RemoveCommand()
  {

  }
}
