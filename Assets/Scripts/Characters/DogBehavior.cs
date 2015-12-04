using UnityEngine;
using System.Collections;

public class DogBehavior : AnimalBehavior
{
	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.transform.tag == "Object")
		{
			bool isOnRightSide = coll.transform.position.x > transform.position.x;
			if (isRightHeading == isOnRightSide)
				pushingObject = coll.gameObject;
		}
	}
	
	
}
