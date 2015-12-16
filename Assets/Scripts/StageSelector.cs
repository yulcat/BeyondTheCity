using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageSelector : MonoBehaviour 
{
	public GameObject mainMenu;
	public Text stageNameText;
	public string[] levelNames;
	int stageIndex = 0;	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			if (stageIndex >= levelNames.Length - 1)
				return;
			stageIndex++;
			ShowSelectedLevel();
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if (stageIndex <= 0)
				return;
			stageIndex--;
			ShowSelectedLevel();
		}
		else if (Input.GetKeyDown(KeyCode.Return))
		{
			LoadStage();
		}
		else if (Input.GetKeyDown(KeyCode.Backspace))
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
