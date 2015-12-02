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
	
	public List<StairCase> stairInit;
	public List<Stair> stairs;
	
	void Start()
	{
		stairInit = new List<StairCase>();
	}
	
	public bool switchOn;
	void Initialize()
	{
		stairs = new List<Stair>();
		for (int i=0; i<stairInit.Count; i++)
		{
			Floor destination;
			Vector2 enterPos;
			Vector2 outPos;
			if (stairInit[i].upper == this)
			{
				destination = stairInit[i].lower;
				enterPos = stairInit[i].goDown;
				outPos = stairInit[i].goUp;
			}
			else
			{
				destination = stairInit[i].upper;
				enterPos = stairInit[i].goUp;
				outPos = stairInit[i].goDown;
			}
			Stair newStair = new Stair(enterPos, outPos, destination);
			stairs.Add(newStair);
		}
	}
	
	void Update()
	{
		if (switchOn)
		{
			Initialize();
			switchOn = false;
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll)
	{
		IFloorable target = coll.gameObject.GetComponent<IFloorable>();
		if (target != null)
			target.SetFloor(this);
	}
}