using UnityEngine;
using System.Collections;

namespace BeyondTheCity
{
	public class CharacterController : MonoBehaviour 
	{
		enum State
		{
			onGround, onBack, onAir
		}
		State characterState = State.onGround;
		
		void OnCollisionStay(Collision2D coll)
		{
			characterState = State.onGround;
		}
		void OnCollisionExit(Collision2D coll)
		{
			characterState = State.onAir;
		}
		Rigidbody2D body;
		
		public float baseSpeed = 0.01f;
		public float jumpPower = 0.1f;
		float WalkSpeed()
		{
			return baseSpeed;
		}
		
		void Awake()
		{
			body = GetComponent<Rigidbody2D>();
		}
		void Update()
		{
			transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * baseSpeed);
			
			if (Input.GetKeyDown("space") && characterState == State.onGround)
			{
				Debug.Log("Jump");
				StartCoroutine(Jump());
			}
		}
		
		IEnumerator Jump()
		{
			float counter = 0.1f;
			while(counter > 0)
			{
				body.AddForce(Vector2.up * jumpPower);
				counter -= Time.deltaTime;
				yield return null;
			}
			yield break;
		}
	}	
}