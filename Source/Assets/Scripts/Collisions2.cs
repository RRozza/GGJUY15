using UnityEngine;
using System.Collections;

public class Collisions2 : MonoBehaviour {

	public float speed;
	public int impulse;
	
	void OnTriggerEnter2D (Collider2D collision){
		if (collision.gameObject.name == "Player1") {
			if (Input.GetKey (KeyCode.RightArrow)) {
				GameObject.Find ("Player2").transform.position += (Vector3.right * speed * Time.deltaTime) * impulse;        
			}
			
			if (Input.GetKey (KeyCode.LeftArrow)) {
				GameObject.Find ("Player2").transform.position += (Vector3.left * speed * Time.deltaTime) * impulse;        
			}
			if (Input.GetKey (KeyCode.UpArrow)) {
				GameObject.Find ("Player2").transform.position += (Vector3.up * speed * Time.deltaTime) * impulse;        
			}			
			if (Input.GetKey (KeyCode.DownArrow)) {
				GameObject.Find ("Player2").transform.position += (Vector3.down * speed * Time.deltaTime) * impulse;        
			}
		}
	}
}
