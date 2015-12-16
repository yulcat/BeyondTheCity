using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Paused : MonoBehaviour {
	GameObject prevSelectedObject;
	public GameObject resumeGame;
	
	void OnEnable(){
		prevSelectedObject = EventSystem.current.currentSelectedGameObject;
		EventSystem.current.SetSelectedGameObject(resumeGame);
		resumeGame.GetComponent<Button>().Select();
	}
	void Update(){
		if(Input.GetButtonDown("Bark1")){
			ResumeGame();
		}
	}
	void ResumeGame(){
		gameObject.SetActive(false);
		EventSystem.current.SetSelectedGameObject(prevSelectedObject);
	}
	public void Select(bool quitting){
		if(!quitting){
			ResumeGame();
			return;
		}
		Application.LoadLevel(0);
	}
}
