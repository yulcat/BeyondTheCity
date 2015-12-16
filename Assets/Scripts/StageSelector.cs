using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageSelector : MonoBehaviour 
{
	public GameObject mainMenu;
	public Text stageNameText;
	public string[] levelNames;
	int stageIndex = 0;
	void Start()
	{
		ShowSelectedLevel();
	}
	void Update()
	{
		if (Input.GetButtonDown("Horizontal1"))
		{
			if (Input.GetAxis("Horizontal1") > 0)
			{
				if (stageIndex >= levelNames.Length - 1)
					return;
				stageIndex++;
			}
			else if(Input.GetAxis("Horizontal1") < 0)
			{
				if (stageIndex <= 0)
					return;
				stageIndex--;
			}
			ShowSelectedLevel();
		}
		else if (Input.GetButtonDown("Jump1"))
		{
			LoadStage();
		}
		else if (Input.GetButtonDown("Bark1"))
		{
			mainMenu.SetActive(true);
			gameObject.SetActive(false);
		}
	}
	
	void ShowSelectedLevel()
	{
		stageNameText.text = levelNames[stageIndex];
	}
	
	void LoadStage()
	{
		Application.LoadLevel(stageIndex + 1);
	}
}
