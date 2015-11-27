using UnityEngine;
using System.Collections;

public class ContactDetector : MonoBehaviour 
{
	public AnimalBehavior owner;
	public string targetTag;
	
	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.transform.tag == targetTag)
			owner.isGrounded = true;
	}
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.transform.tag == targetTag)
			owner.isGrounded = false;
	}
}

