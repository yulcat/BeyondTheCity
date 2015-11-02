using UnityEngine;
using System.Collections;

namespace BeyondTheCity
{
	public abstract class Controller
	{
		public GameObject character;
		public CharacterController charControl
		{
			get
			{
				return character.GetComponent<CharacterController>();
			}
		}
		
	}
	
	public class KeyboardControll : Controller
	{
		
	}
	
	public class MetaController : MonoBehaviour 
	{
		public CharacterController[] players;
		int playerCount = 0;
		
		void Awake()
		{
			
		}
		
		void Update()
		{
			if (Input.GetKeyDown("tab"))
			{
				players[playerCount].enabled = false;
				playerCount++;
				if (playerCount == 3)
					playerCount = 0;
				players[playerCount].enabled = true;
			}
		}
		
		void TestInitializer()
		{
			
		}
	}	
}