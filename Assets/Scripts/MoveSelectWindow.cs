using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveSelectWindow : MonoBehaviour {
	public GameObject firstSelected;
	public GameObject mainMenu;
	public GameObject stageIcons;
	public Text text;
	public string[] stageNames;
	public void MoveWindowAt (int target) {
		Debug.Log(target);
		text.text = stageNames[target];
		iTween.ValueTo(gameObject,iTween.Hash("onupdate","PositionUpdate","onupdatetarget",gameObject,
			"from",stageIcons.GetComponent<RectTransform>().localPosition.x,
			"to",target*-300f,
			"time",0.3f,
			"easetype",iTween.EaseType.easeOutCubic));
	}
	void PositionUpdate(float position) {
		stageIcons.GetComponent<RectTransform>().localPosition = Vector3.right * position;
	}
	void OnEnable () {
		EventSystem.current.SetSelectedGameObject(firstSelected);
	}
	public void SelectStage(int stage){
		Application.LoadLevel(stage + 1);
	}
	public void BackToMain(){
		mainMenu.SetActive(true);
		gameObject.SetActive(false);
		EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
	}
	
	void Update(){
		if(Input.GetButtonDown("Bark1")){
			BackToMain();
		}
	}
}
