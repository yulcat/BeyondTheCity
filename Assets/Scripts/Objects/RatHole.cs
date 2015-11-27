using UnityEngine;
using System.Collections;

public class RatHole : MonoBehaviour 
{
	public GameObject pairHole;
	float holeTimer;
	
	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.transform.tag == "Mouse" && holeTimer == 0)
		{
			StartCoroutine(TeleportTarget(coll.gameObject));
		}
	}
	
	IEnumerator TeleportTarget(GameObject target)
	{
		holeTimer = 3;
		target.SetActive(false);
		yield return new WaitForSeconds(1.0f);
		target.SetActive(true);
		target.transform.position = pairHole.transform.position;
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
