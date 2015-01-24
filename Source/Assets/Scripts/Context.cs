using UnityEngine;
using System.Collections;

public enum Players { P1, P2 }
public enum Keys { UP, DOWN, LEFT, RIGHT, PUNCH, DASH }

[System.Serializable]
public class Boundary {
	public float xMin, xMax, yMin, yMax;
}

public class Context : MonoBehaviour {

	private static Context instance = null;
	public static Context SharedInstance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<Context>();
			}
			return instance;
		}
	}
	
	public bool player1_mutex = false;
	public bool player2_mutex = false;
	public bool collisioning = false;

	public bool isKeyPress(Players player, Keys key) {
		switch (key) {
			case Keys.UP:
				return (player == Players.P1) ? Input.GetKey(KeyCode.UpArrow) : Input.GetKey(KeyCode.W); 
			case Keys.DOWN:
				return (player == Players.P1) ? Input.GetKey(KeyCode.DownArrow) : Input.GetKey(KeyCode.S); 
			case Keys.LEFT:
				return (player == Players.P1) ? Input.GetKey(KeyCode.LeftArrow) : Input.GetKey(KeyCode.A);
			case Keys.RIGHT:
				return (player == Players.P1) ? Input.GetKey(KeyCode.RightArrow) : Input.GetKey(KeyCode.D);
			case Keys.PUNCH:
				return (player == Players.P1) ? Input.GetKey(KeyCode.Keypad1) : Input.GetKey(KeyCode.G);
			case Keys.DASH:
				return (player == Players.P1) ? Input.GetKey(KeyCode.Keypad2) : Input.GetKey(KeyCode.H);
		}
		return false;
	}
}
