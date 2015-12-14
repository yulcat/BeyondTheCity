using UnityEngine;
using System.Collections;

public class CatBehavior : AnimalBehavior
{
	public bool isWalled = false;
	bool wallOnRight;
	
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Wall")
		{
			isWalled = true;
			wallOnRight = coll.transform.position.x > transform.position.x;
		}
	}
	
	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Wall")
			isWalled = false;
		if (coll.gameObject == pushingObject)
		{
			MovableObject target = pushingObject.GetComponent<MovableObject>();
			isPushing = false;
			target.pusher = null;
			pushingObject = null;
		}
	}
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
	public override void Jump()
	{
		if (isGrounded)
		{
			//  StartCoroutine(JumpUp());
			body.velocity = new Vector2(body.velocity.x, jumpPower);
		}
		else if(isWalled)
		{
			int dirc;
			if(wallOnRight) dirc = -1;
			else dirc = 1;
			body.velocity = new Vector2(jumpPower*dirc, jumpPower);
		}
	}
	
	IEnumerator WallJump(float direction)
	{
		yield break;
	}
}
