using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public GameObject animals;
	public Image mask;
	public AnimalBehavior[] friends;
	public void StartNewLevel()
	{
		StartCoroutine(NewLevel());
	}
	IEnumerator NewLevel()
	{
		float timer = 0f;
		float timing = 1f;
		float animalSpeed = 7f;
		while (timer < timing)
		{
			float percentage = timer/timing;
			mask.color = new Color(0, 0, 0, percentage);
			animals.transform.Translate(animalSpeed * Time.deltaTime * Vector2.right);
			timer += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}
		Application.LoadLevel(1);
		yield break;
	}
}
