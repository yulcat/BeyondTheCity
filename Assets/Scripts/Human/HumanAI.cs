using UnityEngine;
using System.Collections.Generic;

public class HumanAI : MonoBehaviour, IFloorable
{
	public Floor currentFloor;
	
	public void SetFloor(Floor newFloor)
	{
		currentFloor = newFloor;
	}
	
	public enum Favority
	{
		PositiveAttract, NegativeAttract, NegativeRepulse, Indifferent
	}
	
	public Favority towardCat;
	public Favority towardDog;
	public Favority towardMouse;
	
	Vector2 destination;
	List<Vector2>Path;
	GameObject targetAnimal;
	
	void Update()
	{
		
	}
	
	void HearSound()
	{
		
	}
}
