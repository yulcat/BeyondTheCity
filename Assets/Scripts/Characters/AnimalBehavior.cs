using UnityEngine;
using System.Collections;

public class AnimalBehavior : MonoBehaviour 
{
	public enum AnimalState
	{
		Gronded, Airbourne
	}
	public AnimalState State
	{
		get
		{
			return AnimalState.Gronded;
		}
	}
	public float jumpPower;
	public float moveSpeed;
	public void Jump()
	{
		
	}
	public void Walk()
	{
		
	}
}
