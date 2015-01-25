using UnityEngine;
using System.Collections;

public class ObstacleAsension : MonoBehaviour {

	public float speed;

	void Update () {
		transform.position += (Vector3.up * speed * Time.deltaTime);
	}
}
