﻿using UnityEngine;
using System.Collections;

public class FootDetector : MonoBehaviour 
{
	public AnimalBehavior owner;
	
	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject!=this.gameObject && coll.transform.tag != "BackGround" && coll.transform.tag != "Sound")
			owner.isGrounded = true;
	}
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject!=this.gameObject && coll.transform.tag != "BackGround" && coll.transform.tag != "Sound")
			owner.isGrounded = false;
	}
}
