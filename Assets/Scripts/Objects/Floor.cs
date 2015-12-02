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
	public List<Stair> stairs;
	
	void Start()
	{
		
	}
}
