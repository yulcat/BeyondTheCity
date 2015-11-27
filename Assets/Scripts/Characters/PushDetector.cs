using UnityEngine;
using System.Collections;

public class PushDetector : MonoBehaviour 
{
	public AnimalBehavior owner;
	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.transform.tag == "Object")
			owner.pushingObject = coll.gameObject;
	}
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.transform.tag == "Object")
			owner.pushingObject = null;
	}
}
