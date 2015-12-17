using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameEnd : MonoBehaviour 
{
	public Image mask;
	IEnumerator EndGame()
	{
		float timer = 0f;
		float timing = 1f;
		Time.timeScale = 0.3f;
		while (timer < timing)
		{
			float percentage = timer/timing;
			mask.color = new Color(0, 0, 0, percentage);
			timer += Time.deltaTime;
			yield return null;
		}
		Time.timeScale = 1;
		Application.LoadLevel("EndingCredit");
		yield break;
	}
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
			StartCoroutine(EndGame());
		}
	}
}
