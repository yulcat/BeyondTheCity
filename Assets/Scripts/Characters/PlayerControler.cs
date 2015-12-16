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
	
	void Update()
	{
		MoveCommand(Input.GetAxis(horControl));
		if (Input.GetButtonDown(barkControl))
			targetCharacter.Bark();
		if (Input.GetButtonDown(jumpControl))
			targetCharacter.Jump();
		if (Input.GetAxis(vertControl) > 0)
			targetCharacter.Interact();
		if (Input.GetAxis(vertControl) < 0)
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
