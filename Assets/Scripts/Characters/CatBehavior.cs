using UnityEngine;
using System.Collections;

public class CatBehavior : AnimalBehavior
{
	public bool isWalled = false;
	
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Wall")
			isWalled = true;
	}
	
	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Wall")
			isWalled = false;
	}
	
	public override void Jump()
	{
		if (isGrounded || isWalled)
		{
			//  StartCoroutine(JumpUp());
			body.velocity = new Vector2(body.velocity.x, jumpPower);
		}
	}
	
	IEnumerator WallJump(float direction)
	{
		
		yield break;
	}
}
