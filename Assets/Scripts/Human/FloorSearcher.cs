using UnityEngine;
using System.Collections.Generic;

public class FloorSearcher 
{
	Vector2 startingPos;
	Vector2 targetPos;
	List<List<Vector2>> pathList;
	Floor startingFloor;
	Floor targetFloor;
	
	public FloorSearcher(Vector2 startingPos, Vector2 targetPos, Floor startingFloor, Floor targetFloor)
	{
		pathList = new List<List<Vector2>>();
		this.startingPos = startingPos;
		this.targetPos = targetPos;
		this.startingFloor = startingFloor;
		this.targetFloor = targetFloor;
	}
	
	void PathFind(Floor StartingFloor, List<Vector2> prevPath)
	{
		List<Vector2> resultPath = new List<Vector2>();
		for (int i=0; i<prevPath.Count; i++)
		{
			resultPath.Add(prevPath[i]);
		}
		if (StartingFloor.stairs == null)
		{
			Debug.Log("No stairs on "  + StartingFloor.gameObject.name);
			return;
		}
		for (int i=0; i<StartingFloor.stairs.Count; i++)
		{
			Floor.Stair targetStair = StartingFloor.stairs[i];
			if (resultPath.Contains(targetStair.enterPos))
				continue;
			resultPath.Add(targetStair.enterPos);
			resultPath.Add(targetStair.outPos);
			if (targetStair.destination == targetFloor)
			{
				resultPath.Add(targetPos);
				pathList.Add(resultPath);
			}
			else
				PathFind(targetStair.destination, resultPath);
		}
	}
	
	List<Vector2> FindBestPath()
	{
		float bestLength;
		List<Vector2> bestPath = null;
		
		if (pathList.Count == 0)
			return null;
		
		bestLength = CalPathLength(pathList[0]);
		bestPath = pathList[0];
		
		for (int i=0; i<pathList.Count; i++)
		{
			float newLength = CalPathLength(pathList[i]);
			if (newLength < 0)
				continue;
			if (newLength < bestLength)
			{
				bestLength = newLength;
				bestPath = pathList[i];
			}	
		}
		
		return bestPath;
	}
	
	float CalPathLength(List<Vector2> path)
	{
		float length = 0;
		if (path == null || path.Count == 0)
			return -1;
		length = (path[0] - startingPos).magnitude;
		if (path.Count == 1)
			return length;
		
		for (int i=1; i<path.Count; i++)
		{
			length += (path[i] - path[i-1]).magnitude;
		}
		
		return length;
	}
	
	public List<Vector2> GetPath()
	{
		if (startingFloor == targetFloor)
		{
			List<Vector2> path = new List<Vector2>();
			path.Add(targetPos);
			return path;
		}
		PathFind(startingFloor, new List<Vector2>());
		return FindBestPath();
	}
}