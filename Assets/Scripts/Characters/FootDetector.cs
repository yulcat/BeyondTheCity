using UnityEngine;
using System.Collections;

public class FootDetector : MonoBehaviour 
{
	public AnimalBehavior owner;
	
	void OnCollisionStay2D(Collision2D coll)
	{
		if (coll.transform.tag == "Ground")
			owner.isGrounded = true;
	}
	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.transform.tag == "Ground")
			owner.isGrounded = false;
	}
}
