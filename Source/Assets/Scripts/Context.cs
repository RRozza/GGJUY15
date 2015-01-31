using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Players { NONE, P1, P2 }
public enum Keys { UP, DOWN, LEFT, RIGHT, PUNCH, DASH }
public enum GameStates { INTRO, START, STARTED, ENDED }

[System.Serializable]
public class Boundary {
	public float xMin, xMax, yMin, yMax;
}

public static class Registry {
	static List<GameObject> register = new List<GameObject> ();

	public static void Add (GameObject go) {
		register.Add (go);
	}

	public static void Remove (GameObject go) {
		register.Remove (go);
	}

	public static GameObject Find (string name) {
		return register.Find (x => x.name == name);
	}
}

public class Context : MonoBehaviour {
	private float startTime;
	private int timerSeconds;
	private GameStates gameState;
	private Players winner, parachuteState;
	private bool player1Stun, player2Stun, player1Dash, player2Dash;

	private static Context instance = null;

	public static Context Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindObjectOfType<Context> ();
			}
			return instance;
		}
	}

	public bool isKeyPress (Players player, Keys key) {
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

	public void UpdateTimer (int seconds) {
		timerSeconds = seconds;
	}

	public GameStates GameState() {
		return gameState;
	}

	public int TimerSeconds() {
		return timerSeconds;
	}

	public void SetStartTime(float time) {
		startTime = time;
	}

	public float StartTime() {
		return startTime;
	}

	public void SetParachuteState(Players state) {
		parachuteState = state;
	}

	public Players ParachuteState() {
		return parachuteState;
	}

	public void SetGameState(GameStates state) {
		gameState = state;
	}

	public void SetGameEnded(Players player) {
		winner = player;
		gameState = GameStates.ENDED;
	}

	public bool GameEnded() {
		return (gameState == GameStates.ENDED);
	}

	public Players Winner() {
		return winner;
	}

	public bool IsStuned(Players player) {
		return (player == Players.P1) ? player1Stun : player2Stun; 
	}

	public void SetStun(Players player, bool value) {
		switch (player) {
			case Players.P1:		
				player1Stun = value;
				break;
			case Players.P2:		
				player2Stun = value;
				break;
		}
	}

	public bool IsDashing(Players player) {
		return (player == Players.P1) ? player1Dash : player2Dash; 
	}
	
	public void SetDash(Players player, bool value) {
		switch (player) {
			case Players.P1:		
				player1Dash = value;
				break;
			case Players.P2:		
				player2Dash = value;
				break;
		}
	}

	void Start() {
		startTime = 0;
		player1Stun = false;
		player2Stun = false;
		player1Dash = false;
		player2Dash = false;
		timerSeconds = 0;
		winner = Players.NONE;
		gameState = GameStates.INTRO;
		parachuteState = Players.NONE;

	}	
}
