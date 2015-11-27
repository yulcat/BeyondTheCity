using UnityEngine;
using System.Collections;

public class AnimalBehavior : MonoBehaviour 
{
	public bool isGrounded;
	public bool isPushing
	{
		get
		{
			if (pushingObject == null)
				return false;
			else
				return true;
		}
	}
	
	//fields
	public float jumpPower;
	public float moveSpeed;
	public float pushPower;
	public float maxWeight;
	
	GameObject pushingObject;
	
	void Update()
	{
		if (!isPushing)
			transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * moveSpeed);
		else if (Mathf.Sign(pushingObject.transform.position.x - transform.position.x) != Mathf.Sign(Input.GetAxis("Horizontal")))
			transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * moveSpeed);
		else if (maxWeight > 0)
			;
		else
			transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * moveSpeed * 0.5f);
	}
}