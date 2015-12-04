using UnityEngine;
using System.Collections;

public class MovableObject : MonoBehaviour, IFloorable
{
	public GameObject block;
	public float weight;
	public GameObject crackSound;
	public Rigidbody2D body;
	public Vector2 GetPos()
	{
		return transform.position;
	}
	public void SetFloor(Floor newFloor)
	{
		Debug.Log("newFloor");
		currentFloor = newFloor;
		if (body.velocity.y < -0.5f)
			CrackSound();
	}
	public Floor GetFloor()
	{
		return currentFloor;
	}
	public Floor currentFloor;
	void DisableSound()
	{
		crackSound.SetActive(false);
	}
	void CrackSound()
	{
		crackSound.SetActive(true);
		Invoke("DisableSound", 1f);
	}
	
	void Update()
	{
		transform.position = block.transform.position;
	}
}
