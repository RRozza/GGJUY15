using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, yMin, yMax;
}

public class ArrowsKeysBehaviour : MonoBehaviour {

	public float speed;
	public float dashSpeed;
	public Boundary boundary;

	void Update () {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.UpArrow)) {
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			if (Input.GetKey(KeyCode.Keypad2)) {
				//Down Dash
				transform.position += Vector3.down * dashSpeed * Time.deltaTime;
			}
			else
			{
				transform.position += Vector3.down * speed * Time.deltaTime;
			}
		}


		//Rebote zona de aire
		if (Input.GetKey(KeyCode.Keypad2))
		{
			if (transform.position.y < -1.8f)
			{
				transform.position = new Vector2
					(
						Mathf.Clamp (rigidbody2D.position.x, boundary.xMin, boundary.xMax),
						Mathf.Clamp (rigidbody2D.position.y, -4, boundary.yMax)
						);
				return;
			}
		}
		
		//Limites de campo de juego
		transform.position = new Vector2 
			(
				Mathf.Clamp (rigidbody2D.position.x, boundary.xMin, boundary.xMax),
				Mathf.Clamp (rigidbody2D.position.y, boundary.yMin, boundary.yMax)
			);
		
	}

}
