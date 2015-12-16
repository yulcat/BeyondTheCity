using UnityEngine;
using System.Collections;

public class SolidGround : MonoBehaviour 
{
	public Floor floor;
	void OnTriggerEnter2D(Collider2D coll)
	{
		IFloorable target = coll.gameObject.GetComponent<IFloorable>();
		if (target != null && target.GetType() != typeof(HumanAI) && target.GetType() != typeof(MovableObject))
		{
			target.SetFloor(floor);
			// Debug.Log(coll.gameObject.name);
		}
	}
}