using UnityEngine;
using System.Collections;

public class TextZone : MonoBehaviour 
{
	public GameObject[] textsToShow;
	bool isShowed = false;
	public float waitTime;
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		string targetTag = coll.transform.tag;
		if (isShowed)
			return;
		if (targetTag == "Cat" || targetTag == "Mouse" || targetTag == "Dog")
		{
			isShowed = true;
			StartCoroutine(TextShow());
		}
	}
	
	IEnumerator TextShow()
	{
		if (textsToShow.Length == 0)
			yield break;
			
		GameObject prevText = textsToShow[0];
		prevText.SetActive(true);
		yield return new WaitForSeconds(waitTime);
		
		for(int i=1; i<textsToShow.Length; i++)
		{
			prevText.SetActive(false);
			prevText = textsToShow[i];
			prevText.SetActive(true);
			yield return new WaitForSeconds(waitTime);
		}
		prevText.SetActive(false);
		
		gameObject.SetActive(false);
		yield break;
	}
}
