using UnityEngine;
using System.Collections;
using System.Linq;

public class MovableObject : MonoBehaviour, IFloorable
{
	private GameObject _pusher;
	public GameObject pusher
	{
		get
		{
			return _pusher;
		}
		set
		{
			if (value == null)
			{
				//  transform.parent = GameObject.Find("Map").transform;
				body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
				body.velocity = Vector2.zero;
				//  body.gravityScale = 0.5f;
				Debug.Log(body.velocity);
			}
			else
			{
				//  transform.parent = value.transform;
				body.constraints = RigidbodyConstraints2D.FreezeRotation;
				//  body.gravityScale = 0;
			}
			_pusher = value;
		}
	}
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
	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.GetComponent<Water>()!=null && weight<2)
		{
			Collider2D myCollider = GetComponent<Collider2D>();
			float tall = myCollider.bounds.size.y;
			float sink = Mathf.Clamp(coll.bounds.max.y - myCollider.bounds.min.y,0,tall);
			float buoyancy = sink/tall*body.gravityScale*Physics2D.gravity.y;
			body.AddForce(Vector2.down * buoyancy * body.mass * 2, ForceMode2D.Force);
		}
	}
}