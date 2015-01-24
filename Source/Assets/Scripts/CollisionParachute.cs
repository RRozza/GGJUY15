using UnityEngine;
using System.Collections;

public class CollisionParachute : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			Context.SharedInstance.parachute_state = (collision.gameObject.name == "Player1") ? ParachuteState.P1 : ParachuteState.P2;
			gameObject.SetActive(false);	
		}
	}
}
