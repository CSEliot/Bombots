using UnityEngine;
using System.Collections;

public class CommandMetadataComponent : MonoBehaviour {

  public Texture moveTexture;
  public Texture turnTexture;
  public Texture waitTexture;

  protected Material material;
  protected CommandMetadata metadata;

  public CommandMetadata Metadata
  {
    get { return metadata; }
    set { metadata = value; }
  }

	// Use this for initialization
	void Start () 
  {
    material = this.gameObject.renderer.materials[0];
	}
	
	// Update is called once per frame
	void Update () 
  {
    SetImages();
	}

  protected void SetImages()
  {
    
    switch (metadata.type)
    {
    case CommandType.MOVE:
        material.SetTexture("_MainTex", moveTexture);
      break;
    case CommandType.TURN:
        material.SetTexture("_MainTex", turnTexture);
      break;
    case CommandType.WAIT:
        material.SetTexture("_MainTex", waitTexture);
      break;
    }
  }
}
