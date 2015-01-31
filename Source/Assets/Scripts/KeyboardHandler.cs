using UnityEngine;
using System.Collections;

public class KeyboardHandler : MonoBehaviour {
	private Animator anim;
	private bool left, right, up, down;
	private string animId, animDashId, animMoveHId, animMoveVId, animPanchId;

	public Players player;
	public Boundary boundary;
	public float speed;
	public int dashTime, dashBlockTime;

	void Start() {
		anim = GetComponent<Animator> ();
		animMoveHId = (player == Players.P1) ? "P1MoveH" : "P2MoveH";
		animMoveVId = (player == Players.P1) ? "P1MoveV" : "P2MoveV";
		animDashId = (player == Players.P1) ? "P1Dash" : "P2Dash";
		animPanchId = (player == Players.P1) ? "P1Punch" : "P2Punch";

		left = false;
		right = false;
		up = false;
		down = false;
	}

	void CheckKeys() {
		left = Context.Instance.isKeyPress(player, Keys.LEFT);
		right = Context.Instance.isKeyPress(player, Keys.RIGHT);
		up = Context.Instance.isKeyPress(player, Keys.UP);
		down = Context.Instance.isKeyPress(player, Keys.DOWN);

		if (left) {
			anim.SetBool(animMoveHId, true);
			transform.position += Vector3.left * speed * Time.deltaTime;
		} 

		if (right) {
			anim.SetBool(animMoveHId, true);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		if (!left && !right){
			anim.SetBool(animMoveHId, false);
		}

		if (up) {
			transform.position += Vector3.up * speed * Time.deltaTime;
		}

		if (down) {
			anim.SetBool(animMoveVId, true);

			if (Context.Instance.isKeyPress(player, Keys.DASH) && Context.Instance.ParachuteState() != player) {		
				Context.Instance.SetDash(player, true);
				anim.SetBool(animDashId, true);
			} else {
				transform.position += Vector3.down * speed * Time.deltaTime;
			}
		} else {
			anim.SetBool (animMoveVId, false);
		}

		if (!Context.Instance.IsDashing(player)) {
			if (Context.Instance.isKeyPress(player, Keys.PUNCH) && Context.Instance.ParachuteState() != player) {
				anim.SetBool(animPanchId, true);
			} else {
				anim.SetBool(animPanchId, false);
			}				
		}
	}
	
	void Update () {
		if (Context.Instance.GameState() == GameStates.STARTED) {
			if (!Context.Instance.IsDashing(player)) {
				if (!Context.Instance.IsStuned(player)) {
					CheckKeys();
				}

				boundary.yMin = -2;
			} else {
				boundary.yMin = -4;
			}

			float x = Mathf.Clamp(rigidbody2D.position.x, boundary.xMin, boundary.xMax);
			float y = Mathf.Clamp(rigidbody2D.position.y, boundary.yMin, boundary.yMax);
			transform.position = new Vector2(x, y);
		}
	}
}
