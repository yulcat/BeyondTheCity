using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour 
{
	public GameObject block;
	void OnTriggerStay2D(Collider2D coll)
	{
		//  Debug.Log("Enabled");
		Color color;
		if (coll.gameObject.tag == "Mouse")
		{
			color = block.GetComponent<SpriteRenderer>().color;
			block.GetComponent<SpriteRenderer>().color = new Vector4(color.r, color.g, color.b, 0.3f);
		}
	}
	
	void OnTriggerExit2D(Collider2D coll)
	{
		//  Debug.Log("Disabled");
		Color color;
		if (coll.gameObject.tag == "Mouse")
		{
			color = block.GetComponent<SpriteRenderer>().color;
			block.GetComponent<SpriteRenderer>().color = new Vector4(color.r, color.g, color.b, 1);
		}
	}
}
