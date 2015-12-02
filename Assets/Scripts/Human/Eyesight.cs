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
			Debug.DrawLine(myPos, targetPos);
			if (ray == false)
			{
				continue;
			}
			if (ray.transform != players[i])
				continue;
			float newDist = (targetPos - myPos).magnitude;
			if (dist == -1 || dist > newDist)
			{
				playerIndex = i;
				dist = newDist;
			}
		}
		
		if (dist != -1 && dist <= sightRange)
			owner.SeePlayer(players[playerIndex]);
		else
			owner.SeeNothing();
	}
	
	bool CheckAngle(Vector2 myPos, Vector2 targetPos)
	{
		float direction = (transform.eulerAngles.y == 0)? 1 : -1;
		float angle = Mathf.Atan2(targetPos.y - myPos.y, direction * (-targetPos.x + myPos.x)) * Mathf.Rad2Deg;
		return (Mathf.Abs(angle) < fov/2);
	}
}
