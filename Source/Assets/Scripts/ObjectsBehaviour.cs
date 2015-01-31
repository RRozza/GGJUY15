using UnityEngine;
using System.Collections;

public class ObjectsBehaviour : MonoBehaviour {

	private float speed;

	void Start() {
		speed = 3.0f;
	}

	void Update () {
		transform.position += (Vector3.up * speed * Time.deltaTime);	
		
		if ((transform.position.y > 7.0f) || (Context.Instance.GameEnded())) {
			gameObject.SetActive(false);
			GameObject.Destroy(gameObject);
		}
	}
}
