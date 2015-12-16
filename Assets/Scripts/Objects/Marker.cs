using UnityEngine;
using System.Collections;

public class Marker : MonoBehaviour 
{
	public bool isPositive;
	public bool IsSatisfied
	{
		get
		{
			return _isSatisfied;	
		}
	}
	bool _isSatisfied;
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.GetComponent<MovableObject>() != null)
			_isSatisfied = isPositive;
	}
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.GetComponent<MovableObject>() != null)
			_isSatisfied = !isPositive;
	}
}
