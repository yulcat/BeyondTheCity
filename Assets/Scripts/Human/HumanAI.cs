using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanAI : MonoBehaviour, IFloorable
{
	public bool isDebugMode;
	public bool _isOnStair;
	GameObject cube;
	public Floor currentFloor;
	public StairCase currentStair;
	public float moveSpeed;
	public GameObject emoticonHostile;
	public GameObject emoticonLove;
	public GameObject emoticonScared;
	bool isLeftHeaded
	{
		get
		{
			if (transform.eulerAngles == Vector3.zero)
				return false;
			else
				return true;
		}
	}
	Transform targetPlayer;
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
			currentAnim = newState;
			animator.ResetTrigger("Stay");
			animator.ResetTrigger("Walk");
			animator.SetTrigger(currentAnim.ToString());
		}
		
		if (currentAnim == AnimState.Stay)
			body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		else
			body.constraints = RigidbodyConstraints2D.FreezeRotation;
	}
	public void SetFloor(Floor newFloor)
	{
		_isOnStair = false;
		currentFloor = newFloor;
	}
	public void SetStair(StairCase newStair)
	{
		_isOnStair = true;
		currentStair = newStair;
	}
	public Floor GetFloor()
	{
		return currentFloor;
	}
	public StairCase GetStair()
	{
		return currentStair;
	}
	public Vector2 GetPos()
	{
		return transform.position;
	}
	public bool IsOnStair()
	{
		return _isOnStair;
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
		{
			HostileApproach(target);
			targetPlayer = target;
		}
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
			transform.eulerAngles = Vector3.zero;
		}
		if (!isLeftHeaded && direction < 0)
		{
			transform.eulerAngles = 180 * Vector3.up;
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
			if (emoticonHostile.activeInHierarchy && coll.transform == targetPlayer)
				GameObject.Find("SceneControl").GetComponent<SceneControl>().RestartLevel();
			float deltaX = coll.transform.position.x - transform.position.x;
			if (deltaX > 0 && isLeftHeaded)
			{
				transform.eulerAngles = Vector3.zero;
			}
			else if (deltaX < 0 && !isLeftHeaded)
			{
				transform.eulerAngles = 180 * Vector3.up;
			}
		}
		if (targetTag == "Sound")
		{
			if(coll.transform.parent.tag == "Cat" && (towardCat == Favority.Indifferent || towardCat == Favority.NegativeRepulse))
				return;
			if(coll.transform.parent.tag == "Dog" && (towardDog == Favority.Indifferent || towardDog == Favority.NegativeRepulse))
				return;
			if(coll.transform.parent.tag == "Mouse" && (towardMouse == Favority.Indifferent || towardMouse == Favority.NegativeRepulse))
				return;
			
			NoticeSound(coll.transform.parent.GetComponent<IFloorable>());
		}
	}
	void OnTriggerStay2D(Collider2D coll)
	{
		string targetTag = coll.transform.tag;
		if (targetTag == "Cat" || targetTag == "Dog" || targetTag == "Mouse")
			touchingPlayer = coll.transform;
		else if (coll.GetComponent<Water>()!=null)
		{
			Collider2D body = GetComponent<Collider2D>();
			float tall = body.bounds.size.y;
			float sink = Mathf.Clamp(coll.bounds.max.y - body.bounds.min.y,0,tall);
			float buoyancy = sink/tall*GetComponent<Rigidbody2D>().gravityScale*Physics2D.gravity.y;
			GetComponent<Rigidbody2D>().AddForce(Vector2.down * buoyancy * GetComponent<Rigidbody2D>().mass, ForceMode2D.Force);
			//GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * 0.99f;
		}
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
		bool isOnStair = _isOnStair;
		List<Vector2> path;
		if (!isOnStair)
			path = new FloorSearcher(GetPos(), destination, currentFloor, targetFloor).GetPath();
		else
			path = new FloorSearcher(GetPos(), destination, null, targetFloor, currentStair, null).GetPath();
		Queue<Vector2> pathQueue = new Queue<Vector2>();
		if (path == null)
		{
			Debug.Log("Null path");
			yield break;
		}
		if (path.Count == 0)
		{
			Debug.Log("zero path");
			yield break;
		}
		for (int i=0; i<path.Count; i++)
		{
			pathQueue.Enqueue(path[i]);
		}
		while(pathQueue.Count > 0)
		{		
			Vector2 nextDestine = pathQueue.Dequeue();
			if (isDebugMode)
			{
				Destroy(cube);
				cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.position = nextDestine;
			}
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
