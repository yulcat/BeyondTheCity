using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanAI : MonoBehaviour, IFloorable
{
	public Floor currentFloor;
	public float moveSpeed;
	bool isLeftHeaded
	{
		get
		{
			if (transform.eulerAngles == Vector3.zero)
				return true;
			else
				return false;
		}
	}
	Transform touchingPlayer;
	Transform chasingPlayer;
	public Animator animator;
	enum AnimState
	{
		Stay, Walk
	}
	public Rigidbody2D body;
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
	public void SetFloor(Floor newFloor)
	{
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
	
	public enum Favority
	{
		PositiveAttract, NegativeAttract, NegativeRepulse, Indifferent
	}
	
	public Favority towardCat;
	public Favority towardDog;
	public Favority towardMouse;
	
	enum HumanState
	{
		Idle, Flee, Angry, Love, Noticed
	}
	
	public GameObject tempTest;
	void Start()
	{
		NoticeSound(tempTest.GetComponent<IFloorable>());
	}
	void Update()
	{
		
	}
	
	void HearSound()
	{
		
	}
	
	public void SeePlayer(Transform target)
	{
		chasingPlayer = target;
		float deltaX = (target.position.x - transform.position.x);
		if (Mathf.Abs(deltaX) < 0.5f)
		{
			BeStay();
			return;
		}
		if (touchingPlayer == target)
		{
			BeStay();
			return;
		}
		float direction = Mathf.Sign(deltaX);
		Move(direction);
	}
	public void SeeNothing()
	{
		chasingPlayer = null;
		BeStay();
	}
	
	void Move(float direction)
	{
		if (isLeftHeaded && direction > 0)
		{
			transform.eulerAngles = 180 * Vector3.up;
		}
		if (!isLeftHeaded && direction < 0)
		{
			transform.eulerAngles = Vector3.zero;
		}
		AnimationChange(AnimState.Walk);
		body.velocity = new Vector2(direction * moveSpeed, body.velocity.y);
	}
	
	void BeStay()
	{
		body.velocity = new Vector2(0, body.velocity.y);
		AnimationChange(AnimState.Stay);
	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		string targetTag = coll.transform.tag;
		if (targetTag == "Cat" || targetTag == "Dog" || targetTag == "Mouse")
		{
			float deltaX = coll.transform.position.x - transform.position.x;
			if (deltaX > 0 && isLeftHeaded)
			{
				Debug.Log("Oh");
				transform.eulerAngles = 180 * Vector3.up;
			}
			else if (deltaX < 0 && !isLeftHeaded)
			{
				Debug.Log("Oh");
				transform.eulerAngles = Vector3.zero;
			}
		}
	}
	void OnTriggerStay2D(Collider2D coll)
	{
		string targetTag = coll.transform.tag;
		if (targetTag == "Cat" || targetTag == "Dog" || targetTag == "Mouse")
			touchingPlayer = coll.transform;
	}
	void OnTriggerExit2D(Collider2D coll)
	{
		string targetTag = coll.transform.tag;
		if (targetTag == "Cat" || targetTag == "Dog" || targetTag == "Mouse")
			touchingPlayer = null;
	}
	
	public void NoticeSound(IFloorable target)
	{
		Vector2 destination = target.GetPos();
		Floor targetFloor = target.GetFloor();
		StartCoroutine(ChaseSound(destination, targetFloor));
	}
	
	IEnumerator ChaseSound(Vector2 destination, Floor targetFloor)
	{
		Debug.Log("Begin");
		bool isOdd = true;
		List<Vector2> path = new FloorSearcher(GetPos(), destination, currentFloor, targetFloor).GetPath();
		Queue<Vector2> pathQueue = new Queue<Vector2>();
		if (path == null || path.Count == 0)
		{
			Debug.Log("Null or empty");
			yield break;
		}
		for (int i=0; i<path.Count; i++)
		{
			pathQueue.Enqueue(path[i]);
		}
		while(pathQueue.Count > 1)
		{
			Vector2 nextDestine = pathQueue.Dequeue();
			if (isOdd)
			{
				float direction = Mathf.Sign(nextDestine.x - GetPos().x);
				Move(direction);
				while(Mathf.Abs(nextDestine.x - GetPos().x) > 0.5f)
				{
					Move(direction);
					yield return null;
				}
				Debug.Log("Upstair");
				BeStay();
				isOdd = false;
			}
			else
			{
				transform.position = nextDestine;
				isOdd = true;
			}
		}
	}
}
