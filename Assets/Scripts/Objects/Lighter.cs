using UnityEngine;
using System.Collections;

public class Lighter : MonoBehaviour 
{	
	Light lighter;
	
	void Start()
	{
		lighter = GetComponent<Light>();
	}
	void Update () 
	{
		lighter.intensity = 4 + 4 * Mathf.Sin(2 * Time.time);
	}
}
