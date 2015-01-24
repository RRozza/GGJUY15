using UnityEngine;
using System.Collections;

public class TremblingBehaviour : MonoBehaviour {

	//public int times;
	public float speed;
	public int limit;
	private int position = 0; 
	private bool direction = true; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/* tembleque
		int update = Random.Range (0, 100);
		int mov = Random.Range(0, 2); 

		if (update > 70) {
			if (limit < -2) {
				transform.position += (Vector3.right * speed * Time.deltaTime) * times;
				limit++;
				return;
			} 
			if (limit > 2) {
				transform.position += (Vector3.left * speed * Time.deltaTime) * times;
				limit--;
				return;
			}

			if (mov == 0) {
				transform.position += (Vector3.left * speed * Time.deltaTime) * times;
				limit--;
			} else {
				transform.position += (Vector3.right * speed * Time.deltaTime) * times;
				limit++;
			}
		}
		*/

		if (position < (-1*limit)) {
			direction = true;	
		} 
		if (position > limit) {
			direction = false;
		}
		
		if (direction) {
			transform.position += (Vector3.left * speed * Time.deltaTime);
			position++;
		} else {
			transform.position += (Vector3.right * speed * Time.deltaTime);
			position--;
		}

	}
}
