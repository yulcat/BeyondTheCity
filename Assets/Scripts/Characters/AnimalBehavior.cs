using UnityEngine;
using System.Collections;

public abstract class AnimalBehavior : MonoBehaviour, IFloorable 
{
	Rigidbody2D veh;
	public bool isPushing = false;
	public Floor currentFloor;
	public StairCase currentStair;
	bool _isOnStair;
	public void SetFloor(Floor newFloor)
	{
		_isOnStair = false;
		currentFloor = newFloor;
	}
	public Floor GetFloor()
	{
		return currentFloor;
	}
	public Vector2 GetPos()
	{
		return transform.position;
	}
	public void SetStair(StairCase newStair)
	{
		_isOnStair = true;
	}
	public StairCase GetStair()
	{
		return currentStair;
	}
	public bool IsOnStair()
	{
		return _isOnStair;
	}
	public GameObject barkSound;
	public float barkPower;
	public Animator animator;
	public Rigidbody2D body;
	public bool isGrounded;
	public bool isRightHeading = true;
	
	enum AnimState
	{
		Stay, Walk, JumpUp, JumpDown
	}
	
	AnimState currentAnim;
	void AnimationChange(AnimState newState)
	{
		if (newState == currentAnim)
			return;
		else
		{
			// Debug.Log(newState.ToString());
			currentAnim = newState;
			animator.ResetTrigger("Stay");
			animator.ResetTrigger("Walk");
			animator.ResetTrigger("JumpUp");
			animator.ResetTrigger("JumpDown");
			animator.SetTrigger(currentAnim.ToString());
		}
		
		if (currentAnim == AnimState.Stay && veh == null)
			body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		else
			body.constraints = RigidbodyConstraints2D.FreezeRotation;
	}
	
	
	//fields
	public float jumpPower;
	public float moveSpeed;
	public float maxWeight;
	
	public GameObject pushingObject;
	
	void Update()
	{
		if (!isGrounded)
		{
			if (body.velocity.y < 0)
				AnimationChange(AnimState.JumpDown);
			else
				AnimationChange(AnimState.JumpUp);
		}
	}
	
	public void Move(float moveInput)
	{
		if (isGrounded)
		{
			if (moveInput == 0)
			{
				AnimationChange(AnimState.Stay);
				if (veh != null)
				{
					body.velocity = veh.velocity;
					return;
				}
			}
			else
			{
				AnimationChange(AnimState.Walk);
			}
		}
		if (isRightHeading && moveInput < 0)
		{
			isRightHeading = false;
			transform.eulerAngles = new Vector3(0, 180, 0);
		}
		else if(!isRightHeading && moveInput > 0)
		{
			isRightHeading = true;
			transform.eulerAngles = Vector3.zero;
		}
		body.velocity = body.velocity.y * Vector2.up + moveSpeed * moveInput * Vector2.right;
	}
	public virtual void Jump()
	{
		if (!isGrounded)
			return;
		body.velocity = new Vector2(body.velocity.x, jumpPower);
	}
	public void Bark()
	{
		if (barkSound.activeInHierarchy)
			return;
		barkSound.SetActive(true);
		barkSound.transform.position = transform.position + Vector3.forward * -4 ;
		barkSound.transform.localScale = barkPower * Vector3.one;
		Invoke("DisableBark", 1f);
	}
	
	public GameObject passableFeet;
	public void DownFloor()
	{
		passableFeet.SetActive(false);
		Invoke("ReActivatePass", 0.3f);
	}
	void ReActivatePass()
	{
		passableFeet.SetActive(true);
	}
	
	void DisableBark()
	{
		barkSound.SetActive(false);
	}
	
	public virtual void Interact()
	{
		
	}
	
	public void Free()
	{
		veh = null;
	}
	public void RideOn(Rigidbody2D target)
	{
		veh = target;
	}
}