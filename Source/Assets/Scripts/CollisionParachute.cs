using UnityEngine;
using System.Collections;

public class CollisionParachute : MonoBehaviour {

	private Animator anim;
	private string animId;

	void Start() {
		anim = GetComponent<Animator> ();
		anim.SetBool("P1Parachute", true);
	}

	void OnTriggerEnter2D (Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			Context.SharedInstance.parachute_state = (collision.gameObject.name == "Player1") ? ParachuteState.P1 : ParachuteState.P2;
			gameObject.SetActive(false);
		}
	}
}
