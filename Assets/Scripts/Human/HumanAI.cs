using UnityEngine;
using System.Collections;

public class HumanAI : MonoBehaviour, IFloorable
{
	public enum Favority
	{
		PositiveAttract, NegativeAttract, NegativeRepulse, Indifferent
	}
	
	public Floor currentFloor;
	
	public void SetFloor(Floor newFloor)
	{
		currentFloor = newFloor;
	}
	
	public Favority towardCat;
	public Favority towardDog;
	public Favority towardMouse;
	public GameObject sight;
	Vector2 destination;
	GameObject targetAnimal;
	
	void Update()
	{
		
	}
}
