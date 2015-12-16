using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GamepadInput;

public class PadController : MonoBehaviour 
{
	// public Text showIntense;
	public int padNumber;
	public AnimalBehavior targetCharacter;
	bool interactSwitch = false;
	bool downfloorSwitch = false;
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
		MoveCommand(GamePad.GetAxis(GamePad.Axis.LeftStick, TargetPad).x);
		if (GamePad.GetButtonDown(GamePad.Button.B, TargetPad))
			targetCharacter.Bark();
		if (GamePad.GetButtonDown(GamePad.Button.A, TargetPad))
			targetCharacter.Jump();
		if (!interactSwitch && GamePad.GetAxis(GamePad.Axis.LeftStick, TargetPad).y > 0.7f)
		{
			interactSwitch = true;
			targetCharacter.Interact();
		}
		if (!downfloorSwitch && GamePad.GetAxis(GamePad.Axis.LeftStick, TargetPad).y < -0.7f)
		{
			downfloorSwitch = true;
			targetCharacter.DownFloor();
		}
		if (interactSwitch && GamePad.GetAxis(GamePad.Axis.LeftStick, TargetPad).y <= 0.7f)
			interactSwitch = false;
		if (downfloorSwitch && GamePad.GetAxis(GamePad.Axis.LeftStick, TargetPad).y >= -0.7f)
			downfloorSwitch = false;
		// showIntense.text = "Pad" + padNumber + ": " + GamePad.GetAxis(GamePad.Axis.LeftStick, TargetPad).y;
	}
	
	void MoveCommand(float moveInput)
	{
		targetCharacter.Move(moveInput);
	}
	void JumpCommand()
	{
		targetCharacter.Jump();
	}
	void BarkCommand()
	{
		targetCharacter.Bark();
	}
	void InteractCommand()
	{
		targetCharacter.Bark();
	}
	void DownCommand()
	{
		targetCharacter.DownFloor();
	}
}
