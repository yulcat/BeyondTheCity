using UnityEngine;
using System.Collections;

public class MinimapControl : MonoBehaviour 
{
	public GameObject minimap;
	void Update () 
	{
		if (Input.GetKey(KeyCode.Tab))
		{
			minimap.SetActive(true);
		}
		else if(Input.GetKeyUp(KeyCode.Tab))
		{
			minimap.SetActive(false);
		}
	}
}
