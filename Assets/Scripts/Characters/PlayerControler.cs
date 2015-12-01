using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour 
{
	public int playerNumber;
	public AnimalBehavior targetCharacter;
	
	void Start()
	{
		horControl = "Horizontal" + playerNumber;
	}
	
	string horControl;
	
	void Update()
	{
		MoveCommand(Input.GetAxis(horControl));
		if (Input.GetButtonDown("Bark1"))
			targetCharacter.Bark();
		if (Input.GetButtonDown("Jump1"))
			targetCharacter.Jump();
		if (Input.GetButtonDown("Interact1"))
			targetCharacter.Interact();
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
}
