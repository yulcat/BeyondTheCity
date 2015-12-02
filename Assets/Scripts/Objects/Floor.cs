using UnityEngine;
using System.Collections.Generic;


public class Floor : MonoBehaviour 
{
	public struct Stair
	{
		public Vector2 enterPos;
		public Vector2 outPos;
		public Floor destination;
	}
	
	public float height;
	public GameObject[] stairInit;
	public List<Stair> stairs;
	
	void Start()
	{
		for (int i=0; i<stairInit.Length; i++)
		{
			
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll)
	{
		IFloorable target = coll.gameObject.GetComponent<IFloorable>();
		if (target != null)
			target.SetFloor(this);
	}
}