using UnityEngine;
using System.Collections;

public class GoUpStair : MonoBehaviour 
{
	public StairCase owner;
	
	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.transform.tag == "Ground")
		{
			Floor target = coll.GetComponent<Floor>();
			if (target != null && !target.stairInit.Contains(owner))
			{
				owner.goUp = transform.position;
				owner.lower = target;
				target.stairInit.Add(owner);
				gameObject.SetActive(false);
			}
		}
	}
}
