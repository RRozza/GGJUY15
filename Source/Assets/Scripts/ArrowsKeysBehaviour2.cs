﻿using UnityEngine;
using System.Collections;

public class ArrowsKeysBehaviour2 : MonoBehaviour {

	public float speed;
	public float dashSpeed;
	public Boundary boundary;

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.A)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.W)) {
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S)) {
			if (Input.GetKey(KeyCode.H)) {
				//Down Dash
				transform.position += Vector3.down * dashSpeed * Time.deltaTime;
			}else{
				transform.position += Vector3.down * speed * Time.deltaTime;
			}
		}

		transform.position = new Vector2(Mathf.Clamp (rigidbody2D.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp (rigidbody2D.position.y, boundary.yMin, boundary.yMax));
	}
}
