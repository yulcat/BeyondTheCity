using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour 
{
	public GameObject mainMenu;
	void Update()
	{
		if (Input.GetButtonDown("Cancel"))
		{
			mainMenu.SetActive(true);
			gameObject.SetActive(false);
		}
	}
}
