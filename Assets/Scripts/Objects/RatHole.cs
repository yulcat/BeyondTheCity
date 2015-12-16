using UnityEngine;
using System.Collections;

public class RatHole : MonoBehaviour 
{
	public GameObject pairHole;
	public float holeTimer;
	
	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.transform.tag == "Mouse" && coll.GetComponent<MouseBehavior>() != null)
		{
			coll.GetComponent<MouseBehavior>().ratHole = gameObject;
		}
	}
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.transform.tag == "Mouse" && coll.GetComponent<MouseBehavior>() != null)
		{
			coll.GetComponent<MouseBehavior>().ratHole = null;
		}
	}
	
	public IEnumerator TeleportTarget(GameObject target)
	{
		holeTimer = 5;
		pairHole.GetComponent<RatHole>().holeTimer = 5;
		target.SetActive(false);
		// Debug.Log("Disappeared");
		// //yield return new WaitForSeconds(1.0f);
		// Debug.Log("Hello");
		target.SetActive(true);
		target.transform.position = pairHole.transform.position;
		target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, 0);
		yield break;
	}
	
	void Update()
	{
		if (holeTimer > 0)
			holeTimer -= Time.deltaTime;
		else
			holeTimer = 0;
	}
}
