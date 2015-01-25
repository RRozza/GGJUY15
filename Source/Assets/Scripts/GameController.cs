using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	private List<GameObject> spawns = new List<GameObject> ();
	private ArrayList  spawnsIds = new ArrayList();
	private Vector3 parachuteStartPosition = new Vector3 (0, -6, 0);
	private float starttime;
	private float timecount;

	public float parachutePopTime;
	public Vector3 spawnValues;
	public int obstacleCount;
	public float spawnWait;
	public float startWait;
	public float spawnSpeed; 
	public int parachuteTrembleY;
	public int parachuteAsensionSpeed;
	public int parachuteOpenTime;
	public int gameEndTime;


	void Start() {
		starttime = Time.time;

		spawnsIds.Add ("Seat");
		spawnsIds.Add ("Bag");

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
		foreach (GameObject spawn in spawns) {
			spawn.transform.position += (Vector3.up * spawnSpeed * Time.deltaTime);	

			if (spawn.transform.position.y > 7.0f) {
				spawns.Remove(spawn);
				//GameObject.Destroy(spawn);
			}
		}

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

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);

		for (int i=1; i < obstacleCount; i++) {
			int index = Random.Range(0,2);	

			Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
			GameObject spawn = Instantiate(Registry.Find (spawnsIds[index] as string), spawnPosition, new Quaternion()) as GameObject;

			spawns.Add(spawn);

			yield return new WaitForSeconds(spawnWait);
		}
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
