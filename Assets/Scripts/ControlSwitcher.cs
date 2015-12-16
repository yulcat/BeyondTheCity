using UnityEngine;
using System.Collections;
using GamepadInput;

public class ControlSwitcher : MonoBehaviour 
{
	public PlayerControler keyboardControl;
	public PadController padControl;
	public int padNumber;
	GamePad.Index TargetPad
	{
		get
		{
			if (padNumber == 1)
				return GamePad.Index.One;
			else if (padNumber == 2)
				return GamePad.Index.Two;
			else if (padNumber == 3)
				return GamePad.Index.Three;
			else
				return GamePad.Index.Four;	
		}
	}	
	
	void Update()
	{
		if (keyboardControl.enabled && GamePad.GetAxis(GamePad.Axis.LeftStick, TargetPad) != Vector2.zero)
		{
			keyboardControl.enabled = false;
			padControl.enabled = true;
		}
	}
}
