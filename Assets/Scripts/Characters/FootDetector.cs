using UnityEngine;
using System.Collections;

public class FootDetector : MonoBehaviour 
{
	public AnimalBehavior owner;
	
	void OnTriggerStay2D(Collider2D coll)
	{
		Debug.Log(coll.name);
		if (coll.transform.tag == "Ground")
			owner.isGrounded = true;
	}
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.transform.tag == "Ground")
			owner.isGrounded = false;
	}
}
