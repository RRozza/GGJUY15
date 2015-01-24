using UnityEngine;
using System.Collections;

public class Context : MonoBehaviour {

	public bool player1_mutex = false;
	public bool player2_mutex = false;
	public bool collisioning = false;

	private static Context instance = null;
	public static Context SharedInstance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<Context>();
			}
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
