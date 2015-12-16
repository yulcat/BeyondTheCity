using UnityEngine;
using System.Collections;

public class EscapeToPause : MonoBehaviour {
	public GameObject pauseMenu;
	// Update is called once per frame
	void Update () {
		if(pauseMenu.activeSelf) return;
		if(Input.GetKey(KeyCode.Escape)){
			Time.timeScale = 0;
			pauseMenu.SetActive(true);
		}
	}
}
