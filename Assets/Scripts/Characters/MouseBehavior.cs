using UnityEngine;
using System.Collections;

public class MouseBehavior : AnimalBehavior 
{
	GameObject ratHole;
	
	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "RatHole")
			ratHole = coll.gameObject;
	}
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "RatHole")
			ratHole = null; 
	}
	
	public override void Interact()
	{
		if (ratHole != null)
			StartCoroutine(ratHole.GetComponent<RatHole>().TeleportTarget(this.gameObject));
	}
}
