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
	
	public GameObject pushingObject;
	
	void Update()
	{
		if (!isPushing)
			transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * moveSpeed);
		else if (Mathf.Sign(pushingObject.transform.position.x - transform.position.x) != Mathf.Sign(Input.GetAxis("Horizontal")))
			transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * moveSpeed);
		else if (pushingObject.GetComponent<MovableObject>().weight > maxWeight)
			;
		else
			transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * moveSpeed * 0.5f);
	
		if (Input.GetKeyDown("space") && isGrounded)
		{
			//Debug.Log("Jump");
			StartCoroutine(Jump());
		}
	}
	
	IEnumerator Jump()
	{
		float counter = 0.1f;
		while(counter > 0)
		{
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower);
			counter -= Time.deltaTime;
			yield return null;
		}
		yield break;
	}
}