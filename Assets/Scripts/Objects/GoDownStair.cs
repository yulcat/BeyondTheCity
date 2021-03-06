﻿using UnityEngine;
using System.Collections;

public class GoDownStair : MonoBehaviour 
{
	public StairCase owner;
	
	void OnTriggerStay2D(Collider2D coll)
	{
		// Debug.Log("Wow");
		if (coll.transform.tag == "Ground")
		{
			// Debug.Log("Found Ground");
			Floor target = coll.GetComponent<SolidGround>().floor;
			if (target != null && !target.stairInit.Contains(owner))
			{
				owner.goDown = transform.position;
				owner.upper = target;
				target.stairInit.Add(owner);
				gameObject.SetActive(false);
			}
		}
	}
}
