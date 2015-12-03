using UnityEngine;
using System.Collections;

public class SceneControl : MonoBehaviour 
{
	public void RestartLevel()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
			RestartLevel();
	}
}
