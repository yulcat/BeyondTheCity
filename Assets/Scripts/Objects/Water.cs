using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {
	void OnTriggerStay2D(Collider2D coll) {
		if (coll.GetComponent<Rigidbody2D>()==null) return;
		Rigidbody2D body = coll.GetComponent<Rigidbody2D>();
		body.velocity = body.velocity * 0.99f;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
