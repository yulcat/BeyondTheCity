using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanAI : MonoBehaviour, IFloorable
{
	public Floor currentFloor;
	public float moveSpeed;
	public GameObject emoticonHostile;
	public GameObject emoticonLove;
	public GameObject emoticonScared;
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
		
		if (currentAnim == AnimState.Stay)
			body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		else
			body.constraints = RigidbodyConstraints2D.FreezeRotation;
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
	Dictionary<string, Favority> favorDic;
	void Start()
	{
		favorDic = new Dictionary<string, Favority>();
		favorDic.Add("Cat", towardCat);
		favorDic.Add("Dog", towardDog);
		favorDic.Add("Mouse", towardMouse);
	}
	void FixedUpdate()
	{
		if (body.velocity.x == 0 || body.velocity.magnitude < 0.7f)
			AnimationChange(AnimState.Stay);
		else
			AnimationChange(AnimState.Walk);
	}
	
	public void SeePlayer(Transform target)
	{
		if (favorDic[target.tag] == Favority.Indifferent)
			return;
		else if (favorDic[target.tag] == Favority.NegativeAttract)
			HostileApproach(target);
		else if (favorDic[target.tag] == Favority.PositiveAttract)
			FriendlyApproach(target);
		else if (favorDic[target.tag] == Favority.NegativeRepulse)
			RunAway(target);
		
		StopAllCoroutines();
		chasingPlayer = target;
		
	}
	void HostileApproach(Transform target)
	{
		emoticonLove.SetActive(false);
		emoticonScared.SetActive(false);
		if (!emoticonHostile.activeInHierarchy)
			emoticonHostile.SetActive(true);
		float deltaX = (target.position.x - transform.position.x);
		if (Mathf.Abs(deltaX) < 0.5f)
		{
			BeStay();
			return;
		}
		//  if (target == chasingPlayer)
		//  {
		//  	BeStay();
		//  	return;
		//  }
		float direction = Mathf.Sign(deltaX);
		Move(direction);
	}
	void FriendlyApproach(Transform target)
	{
		emoticonHostile.SetActive(false);
		emoticonScared.SetActive(false);
		if (!emoticonLove.activeInHierarchy)
			emoticonLove.SetActive(true);
		float deltaX = (target.position.x - transform.position.x);
		if (Mathf.Abs(deltaX) < 2f)
		{
			BeStay();
			return;
		}
		//  if (target == chasingPlayer)
		//  {
		//  	BeStay();
		//  	return;
		//  }
		float direction = Mathf.Sign(deltaX);
		Move(direction);
	}
	void RunAway(Transform target)
	{
		emoticonLove.SetActive(false);
		emoticonHostile.SetActive(false);
		if (!emoticonScared.activeInHierarchy)
			emoticonScared.SetActive(true);
		float deltaX = (target.position.x - transform.position.x);
		MoonWalk(Mathf.Sign(deltaX));
	}
	public void SeeNothing()
	{
		emoticonLove.SetActive(false);
		emoticonScared.SetActive(false);
		emoticonHostile.SetActive(false);
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
		//  AnimationChange(AnimState.Walk);
		body.velocity = new Vector2(direction * moveSpeed, body.velocity.y);
	}
	
	void MoonWalk(float direction)
	{
		AnimationChange(AnimState.Walk);
		body.velocity = new Vector2(-direction * moveSpeed, body.velocity.y);
	}
	
	void BeStay()
	{
		body.velocity = new Vector2(0, body.velocity.y);
		//  AnimationChange(AnimState.Stay);
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
		if (targetTag == "Sound")
		{
			Debug.Log("Sounded");
			NoticeSound(coll.transform.parent.GetComponent<IFloorable>());
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
		emoticonLove.SetActive(false);
		emoticonScared.SetActive(false);
		emoticonHostile.SetActive(false);
		StopAllCoroutines();
		Vector2 destination = target.GetPos();
		Floor targetFloor = target.GetFloor();
		StartCoroutine(ChaseSound(destination, targetFloor));
	}
	
	IEnumerator ChaseSound(Vector2 destination, Floor targetFloor)
	{
		Debug.Log("Begin");
		bool isOnStair = false;
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
		while(pathQueue.Count > 0)
		{
			Vector2 nextDestine = pathQueue.Dequeue();
			Vector2 pastDestine = Vector2.zero;
			if (!isOnStair)
			{
				float direction = Mathf.Sign(nextDestine.x - GetPos().x);
				Move(direction);
				while(Mathf.Abs(nextDestine.x - GetPos().x) > 0.05f)
				{
					Move(direction);
					yield return null;
				}
				BeStay();
				isOnStair = true;
				Debug.Log("Stair!");
			}
			else
			{
				float direction = Mathf.Sign(nextDestine.x - GetPos().x);
				if (transform.position.y < nextDestine.y)
				{
					nextDestine += Vector2.right * direction;
					body.velocity = Vector2.up * 5;
					yield return new WaitForSeconds(0.5f);
				}
				else
				{
					nextDestine += Vector2.right * 2f * direction;
					DownFloor();
				}
				Move(direction);
				while(Mathf.Abs(nextDestine.x - GetPos().x) > 0.5f)
				{
					Move(direction);
					yield return null;
				}
				BeStay();
				isOnStair = false;
			}
			pastDestine = nextDestine;
		}
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
}
