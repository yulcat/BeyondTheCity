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
