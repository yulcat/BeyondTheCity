using UnityEngine;
using System.Collections.Generic;


public class Floor : MonoBehaviour 
{
	public struct Stair
	{
		public Vector2 enterPos;
		public Vector2 outPos;
		public Floor destination;
		public StairCase target;
		public Stair(Vector2 enterPos, Vector2 outPos, Floor destination, StairCase target)
		{
			this.enterPos = enterPos; 
			this.outPos = outPos; 
			this.destination = destination;
			this.target = target;
		}
	}
	
	public bool isStair;
	
	public List<StairCase> stairInit = new List<StairCase>();
	public List<Stair> stairs;
	
	bool isInitialized = false;
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
			Stair newStair = new Stair(enterPos, outPos, destination, stairInit[i]);
			stairs.Add(newStair);
		}
		Debug.Log(stairs.Count);
		isInitialized = true;
	}
	
	void Update()
	{
		if (!isInitialized)
		{
			GoUpStair upStair = (GoUpStair)FindObjectOfType(typeof (GoUpStair));
			GoDownStair downStair = (GoDownStair)FindObjectOfType(typeof (GoDownStair));
			if (upStair == null && downStair == null)
				Initialize();
		}
	}
}