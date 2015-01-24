using UnityEngine;
using System.Collections;

public class KeyboardHandler : MonoBehaviour {
	public float speed;
	public float dashSpeed;
	public Boundary boundary;

	void CheckArrows(Players player) {
		if (Context.SharedInstance.isKeyPress(player, Keys.LEFT)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Context.SharedInstance.isKeyPress(player, Keys.RIGHT)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		if (Context.SharedInstance.isKeyPress(player, Keys.UP)) {
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		if (Context.SharedInstance.isKeyPress(player, Keys.DOWN)) {
			if (Context.SharedInstance.isKeyPress(player, Keys.DASH)) {
				transform.position += Vector3.down * dashSpeed * Time.deltaTime;
			} else	{
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
		
		//Limites de campo de juego
		transform.position = new Vector2(Mathf.Clamp (rigidbody2D.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp (rigidbody2D.position.y, boundary.yMin, boundary.yMax));

	}
}
