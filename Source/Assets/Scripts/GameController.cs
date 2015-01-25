using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
		public GameObject flyingObstacle;
		public Vector3 spawnValues;
		public int obstacleCount;
		public float spawnWait;
		public float startWait;
		public float waveWait;
		public int spawnStop;
		//public int sec = 0;
		public float timecount = 0;
		public float starttime = 0;
		public int parachutePopTime;
		public int parachuteTrembleY;
		public int parachuteAsensionSpeed;
		public int parachuteOpenTime;
		public int gameEndTime;
		private Vector3 parachuteStartPosition = new Vector3 (0, -6, 0);

		// Use this for initialization
		void Start ()
		{
				starttime = Time.time;
				StartCoroutine (SpawnWaves ());
				GameObject parachute = Registry.Find ("Parachute");
				//parachute.transform.position.x = 0;
				//parachute.transform.position.y = 3;
				parachute.transform.position = parachuteStartPosition;
				Context.SharedInstance.trembleEnabled = false;
			
		}
	
		// Update is called once per frame
		void Update ()
		{
				GameObject parachute = Registry.Find ("Parachute");
				Players player = (gameObject.name == "Player1") ? Players.P1 : Players.P2;
				ParachuteState playerGrabParachute = (player == Players.P1) ? ParachuteState.P1 : ParachuteState.P2;
				timecount = Time.time - starttime;
				Context.SharedInstance.updateTimer ((int)(timecount % 60f));
				//Context.SharedInstance.timerSeconds = (int)(timecount % 60f);
				if (Context.SharedInstance.timerSeconds >= parachutePopTime) {
						if (parachute.transform.position.y < parachuteTrembleY) {
								parachute.transform.position += (Vector3.up * parachuteAsensionSpeed * Time.deltaTime);
								//parachute appears from down the screen
						} else if (parachute.transform.position.y >= parachuteTrembleY - 1) {
								Context.SharedInstance.startParachuteTremble ();
						}
				}
				if (Context.SharedInstance.timerSeconds >= parachuteOpenTime) {
						Context.SharedInstance.enableParachuteOpening ();
						if (Context.SharedInstance.isKeyPress (player, Keys.PUNCH) && (Context.SharedInstance.parachute_state == playerGrabParachute)) {
								if (Context.SharedInstance.parachuteIsOpened == false) {				
										OpenParachute ();
										EndGame (player.ToString ());
								}
						}
				}
				if (Context.SharedInstance.timerSeconds == this.gameEndTime && Context.SharedInstance.gameWinner != "") {
						//GAME END LOGIC
						this.EndGame ("none");
						//CALCULATE ROUND WINNER
						//UPDATE SCORES
						//SHOW ENDING
						//START NEXT GAME

				}
		}

		IEnumerator SpawnWaves ()
		{

				yield return new WaitForSeconds (startWait);
				while (true) {
						for (int i = 0; i < obstacleCount; i++) {
								//spawns obstacles until timer
								if (Context.SharedInstance.timerSeconds <= spawnStop) {
										Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
										Quaternion spawnRotation = new Quaternion ();
										Instantiate (flyingObstacle, spawnPosition, spawnRotation);
										yield return new WaitForSeconds (spawnWait);
										yield return new WaitForSeconds (waveWait);
								}
						}
				}
		}

		void EndGame (string winner)
		{
				if (winner == "none") {
						//both lose
						Debug.Log ("BOTH PLAYERS LOSE");
				} else {
						Context.SharedInstance.winGame (winner);
				}

				//if with parachute and parachute is opened -->one winner
				
				//if with parachute but not opened --> both lose
				//if no one has parachute --> both lose



		}

		void OpenParachute ()
		{
				Context.SharedInstance.parachuteIsOpened = true;
				this.DisablePlayerControls ();
		}

		void DisablePlayerControls ()
		{


		}
	
}
