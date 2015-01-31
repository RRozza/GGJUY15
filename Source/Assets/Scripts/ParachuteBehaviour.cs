using UnityEngine;
using System.Collections;

public class ParachuteBehaviour : MonoBehaviour {
	private int position;
	private Animator anim;
	private string animId;
	private bool direction;
	
	public int limit;
	public float speed;
	public float location;
	public float showsTime;
	public float asensionSpeed;
	
	void Start() {
		position = 0;
		direction = true;
		
		anim = GetComponent<Animator> ();
		anim.SetBool("Parachute", true);
		
		transform.position = new Vector3 (0, -9, 0);
	}
	
	void Update () {
		if (Context.Instance.GameState() == GameStates.STARTED) {
			if (position < (-1 * limit)) {
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
			
			if (Context.Instance.TimerSeconds() >= showsTime) {
				if (transform.position.y < location) {
					transform.position += (Vector3.up * asensionSpeed * Time.deltaTime);
				} 
			}
		}
	}
}
