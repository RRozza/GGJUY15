using UnityEngine;
using System.Collections;

public class KeyboardHandler : MonoBehaviour {

	private Animator anim;
	private string animId;

	public float speed;
	public float dashSpeed;
	public int dashTime;
	public int dashBlockTime;
	public Boundary boundary;

	void Start() {
		anim = GetComponent<Animator> ();
	}

	void CheckArrows(Players player) {
		bool left = Context.SharedInstance.isKeyPress (player, Keys.LEFT);
		bool right = Context.SharedInstance.isKeyPress (player, Keys.RIGHT);
		bool up = Context.SharedInstance.isKeyPress (player, Keys.UP);
		bool down = Context.SharedInstance.isKeyPress (player, Keys.DOWN);

		if (left) {
			animId = (player == Players.P1) ? "P1MoveH" : "P2MoveH";
			anim.SetBool (animId, true);
			transform.position += Vector3.left * speed * Time.deltaTime;
		} 

		if (right) {
			animId = (player == Players.P1) ? "P1MoveH" : "P2MoveH";
			anim.SetBool (animId, true);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		
		if (!left && !right){
			animId = (player == Players.P1) ? "P1MoveH" : "P2MoveH";
			anim.SetBool (animId, false);
		}

		if (up) {
			transform.position += Vector3.up * speed * Time.deltaTime;
		}

		if (down) {
			animId = (player == Players.P1) ? "P1MoveV" : "P2MoveV";
			anim.SetBool (animId, true);

			if (Context.SharedInstance.isKeyPress (player, Keys.DASH)) {
				switch (player) {
				case Players.P1: 
					if (Context.SharedInstance.player1_dash == 0) {
						Context.SharedInstance.player1_dash++;
					}
					break;
				case Players.P2:
					if (Context.SharedInstance.player2_dash == 0) {
						Context.SharedInstance.player2_dash++;
					}
					break;					
				} 

				ParachuteState playerGrabParachute = (player == Players.P1) ? ParachuteState.P1 : ParachuteState.P2;
				bool dashPressed = (player == Players.P1) ? Context.SharedInstance.player1_dash > 0 : Context.SharedInstance.player2_dash > 0;
				bool canDash = (Context.SharedInstance.parachute_state != playerGrabParachute && dashPressed);

				if (canDash) {
					animId = (player == Players.P1) ? "P1Dash" : "P2Dash";
					anim.SetBool (animId, true);
					transform.position += Vector3.down * dashSpeed * Time.deltaTime;
				} 
			} else {
				transform.position += Vector3.down * speed * Time.deltaTime;
			}
		} else {
			animId = (player == Players.P1) ? "P1MoveV" : "P2MoveV";
			anim.SetBool (animId, false);
		}

		switch(player) {
			case Players.P1: 
				if (Context.SharedInstance.player1_dash > 0 || Context.SharedInstance.player1_dash < 0) {
					Context.SharedInstance.player1_dash++;
				}
			if (Context.SharedInstance.player1_dash > dashTime) {	
					Context.SharedInstance.player1_dash = (-1*dashTime);
					anim.SetBool ("P1Dash", false);
				}
				break;
			case Players.P2:
				if (Context.SharedInstance.player2_dash > 0 || Context.SharedInstance.player2_dash < 0) {
					Context.SharedInstance.player2_dash++;
				}
				if (Context.SharedInstance.player2_dash > dashTime) {
					Context.SharedInstance.player2_dash = (-1*dashTime);
					anim.SetBool ("P2Dash", false);
				}
				break;					
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

		//Limites de campo de juego
		transform.position = new Vector2(Mathf.Clamp (rigidbody2D.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp (rigidbody2D.position.y, boundary.yMin, boundary.yMax));

		//Animation
		animId = (player == Players.P1) ? "P1Punch" : "P2Punch" ;
		if (Context.SharedInstance.isKeyPress (player, Keys.PUNCH)) {
			anim.SetBool (animId, true);
		} else {
			anim.SetBool (animId, false);
		}
	}
}
