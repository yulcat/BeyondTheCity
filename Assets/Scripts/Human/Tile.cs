﻿using UnityEngine;
using System.Collections;


public struct Point
{
	public int x;
	public int y;
	
	public Point(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}
public class Tile
{
	public Point pos;
	
	public bool isOccupied;
	public bool isGrounded;
}
