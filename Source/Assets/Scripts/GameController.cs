using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	private float timer;
	
	public int gameEndTime;
	public int parachuteOpenTime;
	
	void Start() {
		timer = 0;
	}
	
	void Update () {		
		if (Context.Instance.GameState() == GameStates.STARTED) {
			timer = Time.time - Context.Instance.StartTime();
			Context.Instance.UpdateTimer((int)(timer % 60f));

			if ((Context.Instance.TimerSeconds() >= parachuteOpenTime) && (Context.Instance.ParachuteState() != Players.NONE)) {
					if (Context.Instance.isKeyPress (Context.Instance.ParachuteState(), Keys.PUNCH)) {
						EndGame(Context.Instance.ParachuteState());
					}
			}

			if (Context.Instance.TimerSeconds() >= gameEndTime) {
				EndGame(Players.NONE);
			}
		} 
	}
		
	void EndGame (Players winner) {
		Context.Instance.SetGameEnded(winner);

		GameObject parachute = Registry.Find("Parachute");
		parachute.SetActive(false);
		GameObject player1 = Registry.Find("Player1");
		player1.SetActive(false);
		GameObject player2 = Registry.Find("Player2");
		player2.SetActive(false);
	}
}
