using UnityEngine;
using System.Collections;

public class CollisionPlayer : MonoBehaviour {

	private Animator anim;
	private string animId;
	private int timer = 100;

	public float speed;
	public int impulse;

	void Start() {
		anim = GetComponent<Animator> ();
	}

	void Stun(Players player) {
		GameObject parachute = Registry.Find("Parachute");

		if (player == Players.P1) {
			Context.SharedInstance.player1Mutex = true;
			if (Context.SharedInstance.parachute_state == ParachuteState.P1) {
				parachute.SetActive(true);	
			}
		} else {
			Context.SharedInstance.player2Mutex = true;
			if (Context.SharedInstance.parachute_state == ParachuteState.P2) {
				parachute.SetActive(true);	
			}
		}

		animId = (player == Players.P1) ? "P1Stun" : "P2Stun";
		anim.SetBool (animId, true);
	}

	// Update is called once per frame
	void Update () {
		GameObject player1 = Registry.Find("Player1");
		GameObject player2 = Registry.Find("Player2");
		
		if (timer > 0) {
			if (Context.SharedInstance.player2Mutex) {
				player2.transform.position += (Vector3.up * speed * Time.deltaTime);
				timer--;
			}
			
			if (Context.SharedInstance.player1Mutex) {
				player1.transform.position += (Vector3.up * speed * Time.deltaTime);
				timer--;
			}
		} else {
			Context.SharedInstance.player1Mutex = false;
			Context.SharedInstance.player2Mutex = false;
			anim.SetBool ("P1Stun", false);
			anim.SetBool ("P2Stun", false);
			timer = 100;
		}		
	}
	
	void OnTriggerEnter2D (Collider2D collision){
		GameObject player1 = Registry.Find("Player1");
		GameObject player2 = Registry.Find("Player2");
		
		if (collision.gameObject.CompareTag("Player")) {
			bool p1AtTop = player1.transform.position.y > player2.transform.position.y;
			bool p2AtTop = player1.transform.position.y < player2.transform.position.y;
			
			if (Input.GetKey (KeyCode.Keypad2) && p1AtTop) {
				Stun (Players.P2);
			}
			
			if (Input.GetKey (KeyCode.H) && p2AtTop) {
				Stun (Players.P1);
			}

			int punch1 = (Input.GetKey (KeyCode.Keypad1) && Context.SharedInstance.parachute_state != ParachuteState.P1) ? 4 : 1;
			int punch2 = (Input.GetKey (KeyCode.G) && Context.SharedInstance.parachute_state != ParachuteState.P2 ) ? 4 : 1;
			if (player1.transform.position.x > player2.transform.position.x) {
				player1.transform.position += (Vector3.right * speed * Time.deltaTime) * impulse * punch2;
				player2.transform.position += (Vector3.left * speed * Time.deltaTime) * impulse * punch1;
			} else {
				player1.transform.position += (Vector3.left * speed * Time.deltaTime) * impulse * punch2;
				player2.transform.position += (Vector3.right * speed * Time.deltaTime) * impulse * punch1;
			}
		}

		if (collision.gameObject.CompareTag ("Obstacle")) {
			Players player = (gameObject.name == "Player1") ? Players.P1 : Players.P2;	
			Stun (player);
		}
	}
}
