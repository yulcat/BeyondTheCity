using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveSelectWindow : MonoBehaviour {
	public GameObject firstSelected;
	public void MoveWindowAt (int target) {
		Debug.Log(target);
		iTween.ValueTo(gameObject,iTween.Hash("onupdate","PositionUpdate","onupdatetarget",gameObject,
			"from",gameObject.GetComponent<RectTransform>().localPosition.x,
			"to",target*-300f,
			"time",0.3f,
			"easetype",iTween.EaseType.easeOutCubic));
	}
	void PositionUpdate(float position) {
		gameObject.GetComponent<RectTransform>().localPosition = Vector3.right * position;
	}
	void OnEnable () {
		EventSystem.current.SetSelectedGameObject(firstSelected);
	}
}
