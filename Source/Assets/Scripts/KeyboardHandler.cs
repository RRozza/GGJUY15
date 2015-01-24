using UnityEngine;
using System.Collections;

public class KeyboardHandler : MonoBehaviour {

	private Animator anim;

	public float speed;
	public float dashSpeed;
	public Boundary boundary;

	void Start() {
		anim = GetComponent<Animator> ();
	}

	void CheckArrows(Players player) {
		if (Context.SharedInstance.isKeyPress (player, Keys.LEFT)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Context.SharedInstance.isKeyPress (player, Keys.RIGHT)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		if (Context.SharedInstance.isKeyPress (player, Keys.UP)) {
			transform.position += Vector3.up * speed * Time.deltaTime;
		}

		if (Context.SharedInstance.isKeyPress (player, Keys.DOWN)) {
			ParachuteState playerGrabParachute = (player == Players.P1) ? ParachuteState.P1 : ParachuteState.P2;
			bool dashPressed = (player == Players.P1) ? Context.SharedInstance.player1_dash > 0 : Context.SharedInstance.player2_dash > 0;
			bool canDash = (Context.SharedInstance.parachute_state != playerGrabParachute && dashPressed);

			if (Context.SharedInstance.isKeyPress (player, Keys.DASH)) {

				switch(player) {
					case Players.P1:
						Context.SharedInstance.player1_dash++;
						break;
					case Players.P2:
						Context.SharedInstance.player2_dash++;
						break;
				}

			    if (canDash) {
					transform.position += Vector3.down * dashSpeed * Time.deltaTime;

					switch(player) {
						case Players.P1: 
							Context.SharedInstance.player1_dash++;
							if (Context.SharedInstance.player1_dash > 50) {
								Context.SharedInstance.player1_dash = -20;
							}
							break;
						case Players.P2:
							Context.SharedInstance.player2_dash++;
							if (Context.SharedInstance.player2_dash > 50) {
								Context.SharedInstance.player2_dash = -20;
							}
							break;					
					}
				}
			} else {
				transform.position += Vector3.down * speed * Time.deltaTime;
			}
		}
	}

	// Update is called once per frame
	void Update () {

		Players player = (gameObject.name == "Player1") ? Players.P1 : Players.P2;
		//Check Player Movements
		CheckArrows(player);
		//Air mattress
		if (Context.SharedInstance.isKeyPress(player, Keys.DASH)) {
			if (transform.position.y < -1.8f) {
				transform.position = new Vector2(Mathf.Clamp (rigidbody2D.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp (rigidbody2D.position.y, -4, boundary.yMax));
				return;
			}
		}

		if (Context.SharedInstance.isKeyPress(player, Keys.PUNCH)) {
			anim.SetBool("P1Punch",true);
		}

		//Limites de campo de juego
		transform.position = new Vector2(Mathf.Clamp (rigidbody2D.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp (rigidbody2D.position.y, boundary.yMin, boundary.yMax));

	}
}
