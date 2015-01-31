using UnityEngine;
using System.Collections;

public class CollisionPlayer : MonoBehaviour {
	private int sLapse, dLapse;
	private Animator anim;
	private Players enemy;
	private string enemyId, animStunId, animDashId, animGrabParachuteId;
	
	public int stunLapse, dashLapse;
	public float stunSpeed, dashSpeed;
	public int impulse;
	public Players player;

	void Start() {
		enemyId = (player == Players.P1) ? "Player2" : "Player1";

		enemy = (player == Players.P1) ? Players.P2 : Players.P1;

		anim = GetComponent<Animator> ();
		animStunId = (player == Players.P1) ? "P1Stun" : "P2Stun";
		animDashId = (player == Players.P1) ? "P1Dash" : "P2Dash";
		animGrabParachuteId = (player == Players.P1) ? "P1Parachute" : "P2Parachute";

		sLapse = stunLapse;
		dLapse = dashLapse;
	}

	void Stun() {
		Context.Instance.SetStun(player, true);

		if (Context.Instance.ParachuteState() == player) {
			Context.Instance.SetParachuteState(Players.NONE);

			GameObject parachute = Registry.Find("Parachute");
			anim.SetBool(animGrabParachuteId, false);
			parachute.SetActive(true);
		} 

		anim.SetBool(animStunId, true);
	}

	void Update () {
		if (Context.Instance.GameState() == GameStates.STARTED) {
			if (Context.Instance.IsStuned(player)) {
				if (sLapse > 0) {
					transform.position += (Vector3.up * stunSpeed * Time.deltaTime);
					sLapse--;
				} else {
					Context.Instance.SetStun(player, false);
					sLapse = stunLapse;
					anim.SetBool(animStunId, false);
				}
			}

			if (Context.Instance.IsDashing(player)) {
				if (dLapse > 0) {
					transform.position += (Vector3.down * dashSpeed * Time.deltaTime);
					dLapse--;
				} else {
					Context.Instance.SetDash(player, false);
					dLapse = dashLapse;
					anim.SetBool(animDashId, false);
				}
			}
		}
	}
	
	void OnTriggerEnter2D (Collider2D collision){
		if (collision.gameObject.CompareTag("Player")){
			GameObject enemyGO = Registry.Find(enemyId);
			
			bool enemyOnTop = enemyGO.transform.position.y > transform.position.y;
			if (Context.Instance.isKeyPress(enemy, Keys.DASH) && enemyOnTop) {
				Stun();
			}
			
			bool enemyWithoutParachute = Context.Instance.ParachuteState() != enemy;
			if (Context.Instance.isKeyPress(enemy, Keys.PUNCH) && enemyWithoutParachute) {
				Stun();
			}

		}
	
		if (collision.gameObject.CompareTag("Obstacle")) {
			Stun();
		}
		
		if (collision.gameObject.CompareTag("Parachute")) {
			Context.Instance.SetParachuteState(player);
			anim.SetBool(animGrabParachuteId, true);
			Registry.Find("Parachute").SetActive(false);
		}
	}
}
