using UnityEngine;
using System.Collections;

public class CollisionPlayer : MonoBehaviour {
	
	public float speed;
	public int impulse;
	
	private int timer = 100;
	
	// Update is called once per frame
	void Update () {
		GameObject player1 = GameObject.Find("Player1");
		GameObject player2 = GameObject.Find("Player2");
		
		if (timer > 0) {
			if (Context.SharedInstance.player2_mutex) {
				player2.transform.position += (Vector3.up * speed * Time.deltaTime);
				timer--;
			}
			
			if (Context.SharedInstance.player1_mutex) {
				player1.transform.position += (Vector3.up * speed * Time.deltaTime);
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
		GameObject player1 = GameObject.Find("Player1");
		GameObject player2 = GameObject.Find("Player2");
		
		if (collision.gameObject.CompareTag("Player")) {
			Context.SharedInstance.collisioning = true;
			bool p1AtTop = player1.transform.position.y > player2.transform.position.y;
			bool p2AtTop = player1.transform.position.y < player2.transform.position.y;
			
			if (Input.GetKey (KeyCode.Keypad2) && p1AtTop) {
				Context.SharedInstance.player2_mutex = true;
			}
			
			if (Input.GetKey (KeyCode.H) && p2AtTop) {
				Context.SharedInstance.player1_mutex = true;
			}
			
			int punch1 = Input.GetKey (KeyCode.Keypad1) ? 4 : 1;
			int punch2 = Input.GetKey (KeyCode.G) ? 4 : 1;
			if (player1.transform.position.x > player2.transform.position.x) {
				player1.transform.position += (Vector3.right * speed * Time.deltaTime) * impulse * punch2;
				player2.transform.position += (Vector3.left * speed * Time.deltaTime) * impulse * punch1;
			} else {
				player1.transform.position += (Vector3.left * speed * Time.deltaTime) * impulse * punch2;
				player2.transform.position += (Vector3.right * speed * Time.deltaTime) * impulse * punch1;
			}
		}
	}
}
