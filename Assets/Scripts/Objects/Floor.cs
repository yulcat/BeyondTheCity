using UnityEngine;
using System.Collections.Generic;


public class Floor : MonoBehaviour 
{
	public struct Stair
	{
		public Vector2 enterPos;
		public Vector2 outPos;
		public Floor destination;
		
		public Stair(Vector2 enterPos, Vector2 outPos, Floor destination)
		{
			this.enterPos = enterPos; 
			this.outPos = outPos; 
			this.destination = destination;
		}
	}
	
	public float height;
	public StairCase[] stairInit;
	public List<Stair> stairs;
	
	void Start()
	{
		stairs = new List<Stair>();
		for (int i=0; i<stairInit.Length; i++)
		{
			Floor destination;
			Vector2 enterPos;
			Vector2 outPos;
			if (stairInit[i].upper == this)
			{
				destination = stairInit[i].lower;
				enterPos = stairInit[i].downStair.transform.position;
				outPos = stairInit[i].upStair.transform.position;
			}
			else
			{
				destination = stairInit[i].upper;
				enterPos = stairInit[i].upStair.transform.position;
				outPos = stairInit[i].downStair.transform.position;
			}
			Stair newStair = new Stair(enterPos, outPos, destination);
			stairs.Add(newStair);
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll)
	{
		IFloorable target = coll.gameObject.GetComponent<IFloorable>();
		if (target != null)
			target.SetFloor(this);
	}
}