using UnityEngine;
using System.Collections;

public abstract class AnimalBehavior : MonoBehaviour, IFloorable 
{
	public Floor currentFloor;
	public void SetFloor(Floor newFloor)
	{
		currentFloor = newFloor;
	}
	
	public GameObject barkSound;
	public float barkPower;
	public Animator animator;
	public Rigidbody2D body;
	public bool isGrounded;
	public bool isRightHeading = true;
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
			Debug.Log(newState.ToString());
			currentAnim = newState;
			animator.SetTrigger(currentAnim.ToString());
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
				AnimationChange(AnimState.Stay);
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
		Debug.Log("Jump");
		body.velocity = new Vector2(body.velocity.x, jumpPower);
		//  StartCoroutine(JumpUp());
	}
	public void Bark()
	{
		barkSound.SetActive(true);
		barkSound.transform.position = transform.position + Vector3.forward * -4 ;
		barkSound.transform.localScale = barkPower * Vector3.one;
		Debug.Log("Bark");
		Invoke("DisableBark", 0.5f);
	}
	
	void DisableBark()
	{
		barkSound.SetActive(false);
	}
	
	public virtual void Interact()
	{
		Debug.Log("Interact");	
	}
	
	public IEnumerator JumpUp()
	{
		float jumpTimer = 0.2f;
		
		while (jumpTimer > 0)
		{
			body.AddForce(jumpPower * Vector2.up);
			jumpTimer -= Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}
	}
}