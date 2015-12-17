using UnityEngine;
using GamepadInput;

public class MinimapControl : MonoBehaviour 
{
	public GameObject minimap;
	void Update () 
	{
		if (Input.GetKey(KeyCode.Tab) || GamePad.GetButton(GamePad.Button.Y,0))
		{
			minimap.SetActive(true);
		}
		else if(Input.GetKeyUp(KeyCode.Tab) || GamePad.GetButtonUp(GamePad.Button.Y,0))
		{
			minimap.SetActive(false);
		}
	}
}
