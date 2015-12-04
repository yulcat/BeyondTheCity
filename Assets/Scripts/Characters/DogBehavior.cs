using UnityEngine;
using System.Collections;

public class DogBehavior : AnimalBehavior
{
	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.transform.tag == "Object")
		{
			bool isOnRightSide = coll.transform.position.x > transform.position.x;
			if (isRightHeading == isOnRightSide && coll.gameObject.GetComponent<MovableObject>().weight <= maxWeight)
				pushingObject = coll.gameObject;
			if (pushingObject!= null)
			{
				MovableObject target = pushingObject.GetComponent<MovableObject>();
				if (target.pusher != null)
					return;
				target.pusher = gameObject;
				isPushing = true;
			}
		}
	}
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject == pushingObject)
		{
			MovableObject target = pushingObject.GetComponent<MovableObject>();
			isPushing = false;
			target.pusher = null;
			pushingObject = null;
		}
	}
}
