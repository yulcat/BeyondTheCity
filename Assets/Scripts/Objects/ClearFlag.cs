using UnityEngine;
using System.Collections;

public class ClearFlag : MonoBehaviour 
{
	bool isCat;
	bool isDog;
	bool isMouse;
	
	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.transform.tag == "Cat")
			isCat = true;
		if (coll.transform.tag == "Dog")
			isDog = true;
		if (coll.transform.tag == "Mouse")
			isMouse = true;														
	}
	
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.transform.tag == "Cat")
			isCat = false;
		if (coll.transform.tag == "Dog")
			isDog = false;
		if (coll.transform.tag == "Mouse")
			isMouse = false;														
	}
	
	void Update()
	{
		if (isDog && isCat && isMouse)
		{
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
}
