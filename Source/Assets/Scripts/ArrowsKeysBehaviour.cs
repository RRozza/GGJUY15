using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, yMin, yMax;
}

public class ArrowsKeysBehaviour : MonoBehaviour {

	public float speed;	
	public Boundary boundary;
	
	// Update is called once per frame
	void Update () {
		if (!Context.SharedInstance.collisioning && !Context.SharedInstance.player1_mutex) {
			if (Input.GetKey (KeyCode.LeftArrow)) {
				transform.position += Vector3.left * speed * Time.deltaTime;
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				transform.position += Vector3.right * speed * Time.deltaTime;
			}
			if (Input.GetKey (KeyCode.UpArrow)) {
				transform.position += Vector3.up * speed * Time.deltaTime;
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				transform.position += Vector3.down * speed * Time.deltaTime;
			}

			transform.position = new Vector2 (Mathf.Clamp (rigidbody2D.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp (rigidbody2D.position.y, boundary.yMin, boundary.yMax));
		}
	}
}
