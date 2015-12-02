using UnityEngine;
public interface IFloorable 
{
	void SetFloor(Floor newFloor);
	Floor GetFloor();
	Vector2 GetPos();
}