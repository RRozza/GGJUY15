using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Players
{ 
		NONE,
		P1,
		P2
		
}

public enum Keys
{
		UP,
		DOWN,
		LEFT,
		RIGHT,
		PUNCH,
		DASH
}

public enum ParachuteState
{
		NONE,
		P1,
		P2
}

[System.Serializable]
public class Boundary
{
		public float xMin, xMax, yMin, yMax;
}

public static class Registry
{
		static List<GameObject> register = new List<GameObject> ();
	
		public static void Add (GameObject go)
		{
				register.Add (go);
		}
	
		public static void Remove (GameObject go)
		{
				register.Remove (go);
		}
	
		public static GameObject Find (string name)
		{
				return register.Find (x => x.name == name);
		}
}

public class Context : MonoBehaviour
{
		private static Context instance = null;

		public static Context SharedInstance {
				get {
						if (instance == null) {
								instance = GameObject.FindObjectOfType<Context> ();
						}
						return instance;
				}
		}

		public int timerSeconds = 0;
		public float startTime = 0;
		public bool trembleEnabled = false;
		public bool parachuteOpenEnabled = false;
		public bool player1Mutex = false;
		public bool player2Mutex = false;
		public int player1Dash = 0;
		public int player2Dash = 0;
		public bool gameEnded = false;
		public bool gameStarted = false;
		public bool introFinished = false;
		public Players winner = Players.NONE;
		public ParachuteState parachute_state = ParachuteState.NONE;

		public bool isKeyPress (Players player, Keys key)
		{
			bool p1key;
			bool p2key;
			switch (key) {
				case Keys.UP:    
					p1key = (Input.GetKey (KeyCode.UpArrow) || Input.GetAxis ("V Joy 1") < 0);
					p2key = (Input.GetKey (KeyCode.W) || Input.GetAxis ("V Joy 2") < 0);
					return (player == Players.P1) ? p1key : p2key; 
				case Keys.DOWN:
					p1key = (Input.GetKey (KeyCode.DownArrow) || Input.GetAxis ("V Joy 1") > 0);
					p2key = (Input.GetKey (KeyCode.S) || Input.GetAxis ("V Joy 2") > 0);
					return (player == Players.P1) ? p1key : p2key; 
				case Keys.LEFT:
					p1key = (Input.GetKey (KeyCode.LeftArrow) || Input.GetAxis ("H Joy 1") < 0);
					p2key = (Input.GetKey (KeyCode.A) || Input.GetAxis ("H Joy 2") < 0);
					return (player == Players.P1) ? p1key : p2key;
				case Keys.RIGHT:
					p1key = (Input.GetKey (KeyCode.RightArrow) || Input.GetAxis ("H Joy 1") > 0);
					p2key = (Input.GetKey (KeyCode.D) || Input.GetAxis ("H Joy 2") > 0);
					return (player == Players.P1) ? p1key : p2key;
				case Keys.PUNCH:
					p1key = (Input.GetKey (KeyCode.Keypad1) || Input.GetAxis ("Punch Joy 1") > 0);
					p2key = (Input.GetKey (KeyCode.G) || Input.GetAxis ("Punch Joy 2") > 0);
					return (player == Players.P1) ? p1key : p2key;
				case Keys.DASH:
					p1key = (Input.GetKey (KeyCode.Keypad2) || Input.GetAxis ("Dash Joy 1") > 0);
					p2key = (Input.GetKey (KeyCode.H) || Input.GetAxis ("Dash Joy 2") > 0);
					return (player == Players.P1) ? p1key : p2key;
			}
			return false;
	}

		public void updateTimer (int seconds)
		{
			timerSeconds = seconds;
		}

		public void startParachuteTremble ()
		{
			trembleEnabled = true;
		}
	
}
