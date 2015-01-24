using UnityEngine;
using System.Collections;

public class Collisions2 : MonoBehaviour {

	public float speed;
	public int impulse;

	private int timer = 100;
	
	// Update is called once per frame
	void Update () {
		if (timer > 0) {
			if (Context.SharedInstance.player2_mutex) {
				GameObject.Find("Player2").transform.position += (Vector3.up * speed * Time.deltaTime);
				timer--;
			}

			if (Context.SharedInstance.player1_mutex) {
				GameObject.Find("Player1").transform.position += (Vector3.up * speed * Time.deltaTime);
				timer--;
			}
		} else {
			Context.SharedInstance.player1_mutex = false;
			Context.SharedInstance.player2_mutex = false;
			timer = 100;
		}		
		
		if (Context.SharedInstance.collisioning == true) {
			Context.SharedInstance.collisioning = false;		
		}
	}

	void OnTriggerEnter2D (Collider2D collision){
		Context.SharedInstance.collisioning = true;
		bool p1AtTop = GameObject.Find("Player1").transform.position.y > GameObject.Find("Player2").transform.position.y;
		bool p2AtTop = GameObject.Find("Player1").transform.position.y < GameObject.Find("Player2").transform.position.y;
		
		if (Input.GetKey(KeyCode.Keypad2) && p1AtTop) {
			Context.SharedInstance.player2_mutex = true;
		}
		
		if (Input.GetKey(KeyCode.H) && p2AtTop) {
			Context.SharedInstance.player1_mutex = true;
		}
		
		if (GameObject.Find("Player1").transform.position.x > GameObject.Find("Player2").transform.position.x) {
			GameObject.Find("Player1").transform.position += (Vector3.right * speed * Time.deltaTime) * impulse;
			GameObject.Find("Player2").transform.position += (Vector3.left * speed * Time.deltaTime) * impulse;
		} else {
			GameObject.Find("Player1").transform.position += (Vector3.left * speed * Time.deltaTime) * impulse;
			GameObject.Find("Player2").transform.position += (Vector3.right * speed * Time.deltaTime) * impulse;
		}
		if (collision.gameObject.name == "Parachute")
		{
			//Activar animaciones con parachute
		}

	}
}
