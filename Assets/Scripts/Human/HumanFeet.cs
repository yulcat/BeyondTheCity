using UnityEngine;
using System.Collections;

public class HumanFeet : MonoBehaviour 
{
	public HumanAI owner;
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.GetComponent<StairCase>() != null)
			owner.SetStair(coll.gameObject.GetComponent<StairCase>());
		if (coll.gameObject.GetComponent<SolidGround>() != null)
			owner.SetFloor(coll.gameObject.GetComponent<SolidGround>().floor);
	}
}
