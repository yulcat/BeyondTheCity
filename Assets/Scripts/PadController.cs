using UnityEngine;
using System.Collections;
using GamepadInput;

public class PadController : MonoBehaviour 
{
	public int padNumber;
	public AnimalBehavior targetCharacter;
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
		if (GamePad.GetButtonDown(GamePad.Button.A, TargetPad))
			targetCharacter.Bark();
		if (GamePad.GetButtonDown(GamePad.Button.A, TargetPad))
			targetCharacter.Jump();
		if (GamePad.GetAxis(GamePad.Axis.LeftStick, TargetPad).y > 1)
			targetCharacter.Interact();
		if (GamePad.GetAxis(GamePad.Axis.LeftStick, TargetPad).y < -1)
			targetCharacter.DownFloor();
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
