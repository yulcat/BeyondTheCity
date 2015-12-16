﻿using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.transform.tag != "Sound" && coll.GetComponent<AnimalBehavior>() != null)
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
