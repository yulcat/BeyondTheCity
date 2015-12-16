using UnityEngine;
using System.Collections;
using GamepadInput;

public class EscapeToPause : MonoBehaviour {
	public GameObject pauseMenu;
	
	// Update is called once per frame
	void Update () {
		if(pauseMenu.activeSelf) return;
		if(Input.GetKey(KeyCode.Escape) || GamePad.GetState(0).Start){
			Time.timeScale = 0;
			pauseMenu.SetActive(true);
		}
	}
}
