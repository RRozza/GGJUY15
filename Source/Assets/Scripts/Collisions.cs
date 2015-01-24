using UnityEngine;
using System.Collections;

public class Collisions : MonoBehaviour {

	public int speed;
	public int impulse;

	void OnTriggerEnter2D (Collider2D collision){
		if (collision.gameObject.name == "Player2") {
			if (Input.GetKey (KeyCode.D)) {
				GameObject.Find ("Player1").transform.position += (Vector3.right * speed * Time.deltaTime) * impulse;        
			}

			if (Input.GetKey (KeyCode.A)) {
				GameObject.Find ("Player1").transform.position += (Vector3.left * speed * Time.deltaTime) * impulse;        
			}
		}
	}
}
