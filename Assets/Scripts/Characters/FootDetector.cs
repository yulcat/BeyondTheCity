using UnityEngine;
using System.Collections;

public class FootDetector : MonoBehaviour 
{
	public AnimalBehavior owner;
	
	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject!=this.gameObject && coll.transform.tag != "BackGround" && coll.transform.tag != "Sound")
			owner.isGrounded = true;
		if (coll.gameObject.tag == "Cat" || coll.gameObject.tag == "Dog" || coll.gameObject.tag == "Mouse")
			owner.RideOn(coll.GetComponent<Rigidbody2D>());
	}
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject!=this.gameObject && coll.transform.tag != "BackGround" && coll.transform.tag != "Sound")
			owner.isGrounded = false;
		if (coll.gameObject.tag == "Cat" || coll.gameObject.tag == "Dog" || coll.gameObject.tag == "Mouse")
			owner.Free();
	}
}
