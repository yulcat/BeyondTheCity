using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour 
{
	public Animator animator;
	enum AnimState
	{
		Closed, Close, Opened, Open
	}
	AnimState currentAnim = AnimState.Closed;
	public Collider2D doorCol;
	float doorTimer;
	
	bool isHumaned = false;
	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.transform.tag == "Human")
			isHumaned = true;
	}
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.transform.tag == "Human")
			isHumaned = false;
	}
	
	void AnimationChange(AnimState newState)
	{
		if (newState == currentAnim)
			return;
		else
		{
			// Debug.Log(newState.ToString());
			currentAnim = newState;
			animator.SetTrigger(currentAnim.ToString());
		}
	}
	
	void Update()
	{
		if (isHumaned && doorCol.enabled)
		{
			GetComponent<AudioSource>().Play();
			AnimationChange(AnimState.Open);
			doorTimer += Time.deltaTime;
		}
		else
		{
			doorTimer = 0;
		}
		
		if (doorTimer > 0.8f)
		{
			AnimationChange(AnimState.Opened);
			doorCol.enabled = false;
			doorTimer = 0;
		}
	}
}
