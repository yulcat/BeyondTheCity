using UnityEngine;
using System.Collections;

public class BoxFeet : MonoBehaviour 
{
	public MovableObject owner;
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.GetComponent<StairCase>() != null)
			owner.SetStair(coll.gameObject.GetComponent<StairCase>());
		if (coll.gameObject.GetComponent<SolidGround>() != null)
			owner.SetFloor(coll.gameObject.GetComponent<SolidGround>().floor);
		IFloorable target = coll.gameObject.GetComponent<IFloorable>();
		if (target != null)
		{
			if (target.IsOnStair() && target.GetStair() != null)
				owner.SetStair(target.GetStair());
			else if (!target.IsOnStair() && target.GetFloor() != null)
				owner.SetFloor(target.GetFloor());
		}
	}
}
