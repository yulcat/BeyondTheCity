using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour 
{
	public int playerNumber;
	public AnimalBehavior targetCharacter;
	
	void Start()
	{
		horControl = "Horizontal" + playerNumber;
		vertControl = "Vertical" + playerNumber;
		jumpControl = "Jump" + playerNumber;
		barkControl = "Bark" + playerNumber;
	}
	
	string horControl;
	string vertControl;
	string jumpControl;
	string barkControl;
	
	bool interactSwitch = false;
	bool downfloorSwitch = false;
	
	void Update()
	{
		MoveCommand(Input.GetAxis(horControl));
		if (Input.GetButtonDown(barkControl))
			targetCharacter.Bark();
		if (Input.GetButtonDown(jumpControl))
			targetCharacter.Jump();
		if (!interactSwitch && Input.GetAxis(vertControl) > 0.7f)
		{
			interactSwitch = true;
			targetCharacter.Interact();
		}	
		if (!downfloorSwitch && Input.GetAxis(vertControl) < -0.7f)
		{
			downfloorSwitch = true;
			targetCharacter.DownFloor();
		}
		if (interactSwitch && Input.GetAxis(vertControl) <= 0.7f)
			interactSwitch = false;
		if (downfloorSwitch && Input.GetAxis(vertControl) >= -0.7f)
			downfloorSwitch = false;
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
