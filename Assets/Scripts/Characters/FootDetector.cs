using UnityEngine;
using System.Collections;

public class FootDetector : MonoBehaviour 
{
	public AnimalBehavior owner;
	
	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject!=this.gameObject && coll.transform.tag != "BackGround" && coll.transform.tag != "Sound" && coll.transform.tag != "Object")
			owner.isGrounded = true;
		if (coll.gameObject.tag == "Cat" || coll.gameObject.tag == "Dog" || coll.gameObject.tag == "Mouse" || coll.gameObject.tag == "Human")
			owner.RideOn(coll.GetComponent<Rigidbody2D>());
		if (coll.transform.parent != null && coll.transform.parent.GetComponents<Rigidbody2D>() != null)
			owner.RideOn(coll.transform.parent.GetComponent<Rigidbody2D>());
	}
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject!=this.gameObject && coll.transform.tag != "BackGround" && coll.transform.tag != "Sound" && coll.transform.tag != "Object")
			owner.isGrounded = false;
		if (coll.gameObject.tag == "Cat" || coll.gameObject.tag == "Dog" || coll.gameObject.tag == "Mouse" || coll.gameObject.tag == "Human")
			owner.Free();
		if (coll.transform.parent != null && coll.transform.parent.GetComponents<Rigidbody2D>() != null)
			owner.Free();
	}
	
	void OnCollisionStay2D(Collision2D coll)
	{
		if (coll.transform.parent != null && coll.transform.parent.GetComponents<Rigidbody2D>() != null)
			owner.RideOn(coll.transform.parent.GetComponent<Rigidbody2D>());
	}
	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.transform.parent != null && coll.transform.parent.GetComponents<Rigidbody2D>() != null)
			owner.Free();
	}
}
