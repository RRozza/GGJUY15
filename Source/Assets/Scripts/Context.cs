using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Players
{
		P1,
		P2,
		NONE
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
		public bool parachuteIsOpened = false;

		public bool isKeyPress (Players player, Keys key)
		{
				switch (key) {
				case Keys.UP:
						return (player == Players.P1) ? Input.GetKey (KeyCode.UpArrow) : Input.GetKey (KeyCode.W); 
				case Keys.DOWN:
						return (player == Players.P1) ? Input.GetKey (KeyCode.DownArrow) : Input.GetKey (KeyCode.S); 
				case Keys.LEFT:
						return (player == Players.P1) ? Input.GetKey (KeyCode.LeftArrow) : Input.GetKey (KeyCode.A);
				case Keys.RIGHT:
						return (player == Players.P1) ? Input.GetKey (KeyCode.RightArrow) : Input.GetKey (KeyCode.D);
				case Keys.PUNCH:
						return (player == Players.P1) ? Input.GetKey (KeyCode.Keypad1) : Input.GetKey (KeyCode.G);
				case Keys.DASH:
						return (player == Players.P1) ? Input.GetKey (KeyCode.Keypad2) : Input.GetKey (KeyCode.H);
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

		public void enableParachuteOpening ()
		{
			parachuteOpenEnabled = true;
		}
	
}
