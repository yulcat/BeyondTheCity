using UnityEngine;
public interface IFloorable 
{
	void SetFloor(Floor newFloor);
	void SetStair(StairCase newStair);
	Floor GetFloor();
	StairCase GetStair();
	bool IsOnStair();
	Vector2 GetPos();
}