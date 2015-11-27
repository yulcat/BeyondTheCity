using UnityEngine;
using System.Collections;

public class HumanAI : MonoBehaviour 
{
	public enum Favority
	{
		PositiveAttract, NegativeAttract, NegativeRepulse, Indifferent
	}
	
	public Favority towardCat;
	public Favority towardDog;
	public Favority towardMouse;
	public GameObject sight;
	Vector3 destination;
	GameObject targetAnimal;
	
	void SetTarget()
	{
		
	}
	void Update()
	{
		
	}
}
