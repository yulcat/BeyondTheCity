using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour 
{
	public GameObject mainMenu;
	void Update()
	{
		if (Input.GetButtonDown("Bark1"))
		{
			mainMenu.SetActive(true);
			gameObject.SetActive(false);
		}
	}
}
