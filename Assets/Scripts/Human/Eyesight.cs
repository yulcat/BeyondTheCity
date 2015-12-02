using UnityEngine;
using System.Collections;

public class Eyesight : MonoBehaviour 
{
	public float sightRange;
	public float fov;
	public HumanAI owner;
	public Transform[] players;
	
	void Update()
	{
		float dist = -1;
		int playerIndex = -1;
		
		for (int i=0; i<players.Length; i++)
		{
			Vector2 targetPos = players[i].position;
			Vector2 myPos = transform.position;
			if (!CheckAngle(myPos, targetPos))
				continue;
			RaycastHit2D ray = Physics2D.Linecast(myPos, targetPos);
			if (ray.transform.gameObject != players[i])
				continue;
			float newDist = (targetPos - myPos).magnitude;
			if (dist == -1 || dist > newDist)
			{
				playerIndex = i;
				dist = newDist;
			}
		}
		
		if (dist != -1 && dist <= sightRange)
			CatchOnSight(players[playerIndex]);
	}
	
	bool CheckAngle(Vector2 myPos, Vector2 targetPos)
	{
		float angle = Mathf.Atan2(targetPos.y - myPos.y, targetPos.x - myPos.x) * Mathf.Rad2Deg;
		return (angle <= 180 + fov/2 && angle >= 180 + fov/2);
	}
	
	void CatchOnSight(Transform target)
	{
		
	}
}
