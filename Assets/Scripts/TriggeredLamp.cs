using UnityEngine;
using System.Collections;

public class TriggeredLamp : MonoBehaviour {
	public GameObject[] lights;
	public Sprite lightOn;
	public Sprite lightOff;
	public bool lightIsOn = false;
	public bool lightIsOnLastFrame = false;
	
	// Update is called once per frame
	// void FixedUpdate () {
	// 	if(!lightIsOn && lightIsOnLastFrame){
	// 		lightIsOnLastFrame = false;
	// 		GetComponent<SpriteRenderer>().sprite = lightOff;
	// 		foreach(GameObject obj in lights){
	// 			obj.SetActive(false);
	// 		}
	// 	}
	// 	lightIsOn = false;
	// }
	
	void OnTriggerStay2D(Collider2D coll)
	{
		lightIsOn = true;
		if(!lightIsOnLastFrame){
			lightIsOnLastFrame = true;
			GetComponent<SpriteRenderer>().sprite = lightOn;
			foreach(GameObject obj in lights){
				obj.SetActive(true);
			}
		}
	}
}
