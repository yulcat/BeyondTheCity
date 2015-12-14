using UnityEngine;
using System.Collections;
using System.Linq;

public class DogBehavior : AnimalBehavior
{
	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.transform.tag == "Object")
		{
			bool isOnRightSide = coll.transform.position.x > transform.position.x;
			if (isRightHeading == isOnRightSide && coll.gameObject.GetComponent<MovableObject>().weight <= maxWeight)
			{
				pushingObject = coll.gameObject;
				Debug.Log("Pushing!!!");
			}
			if (pushingObject!= null)
			{
				MovableObject target = pushingObject.GetComponent<MovableObject>();
				if (target.pusher != null)
					return;
				target.pusher = gameObject;
				isPushing = true;
			}
		}
		else if (coll.GetComponent<Water>()!=null)
		{
			Collider2D body = GetComponents<Collider2D>().First(c=>!c.isTrigger);
			float tall = body.bounds.size.y;
			float sink = Mathf.Clamp(coll.bounds.max.y - body.bounds.min.y,0,tall);
			float buoyancy = sink/tall*GetComponent<Rigidbody2D>().gravityScale*Physics2D.gravity.y;
			GetComponent<Rigidbody2D>().AddForce(Vector2.down * buoyancy * GetComponent<Rigidbody2D>().mass, ForceMode2D.Force);
			//GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * 0.99f;
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
