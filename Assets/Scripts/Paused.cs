using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GamepadInput;

public class Paused : MonoBehaviour {
	GameObject prevSelectedObject;
	public GameObject resumeGame;
	
	void OnEnable(){
		prevSelectedObject = EventSystem.current.currentSelectedGameObject;
		EventSystem.current.SetSelectedGameObject(resumeGame);
		resumeGame.GetComponent<Button>().Select();
	}
	void Update(){
		if(Input.GetButtonDown("Bark1") || GamePad.GetState(0).B){
			Time.timeScale = 1;
			ResumeGame();
		}
	}
	void ResumeGame(){
		gameObject.SetActive(false);
		EventSystem.current.SetSelectedGameObject(prevSelectedObject);
	}
	public void Select(int todo){
		Time.timeScale = 1;
		if(todo==0){
			ResumeGame();
			return;
		}else if(todo==1){
			Application.LoadLevel(0);
		}else{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
