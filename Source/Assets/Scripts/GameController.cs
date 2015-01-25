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
		parachute.transform.position = parachuteStartPosition;
		Context.SharedInstance.trembleEnabled = false;
	}
	
	void Update ()
	{		
		if (Context.SharedInstance.gameStarted && !Context.SharedInstance.gameEnded) {
			//Spawn generation
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
			
			//Parachute appears from down the screen
			if (Context.SharedInstance.timerSeconds >= parachutePopTime) {
				if (parachute.transform.position.y < parachuteTrembleY) {
					parachute.transform.position += (Vector3.up * parachuteAsensionSpeed * Time.deltaTime);
				} else if (parachute.transform.position.y >= parachuteTrembleY - 1) {
					Context.SharedInstance.startParachuteTremble ();
				}
			}
			
			//Parachute can be open
			if (Context.SharedInstance.timerSeconds >= parachuteOpenTime) {
				Context.SharedInstance.enableParachuteOpening ();
				if (Context.SharedInstance.isKeyPress (player, Keys.PUNCH) && (Context.SharedInstance.parachute_state == playerGrabParachute)) {
					EndGame (player);
				}
			}
			
			//Timeover
			if (Context.SharedInstance.timerSeconds == gameEndTime) {
				EndGame (Players.NONE);
			}
			
			timecount = Time.time - starttime;
			Context.SharedInstance.updateTimer((int)(timecount % 60f));
		
		}
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
		
	void EndGame (Players winner)
	{
		Context.SharedInstance.parachuteIsOpened = true;
		Context.SharedInstance.gameEnded = true;
		Context.SharedInstance.winner = winner;

		switch (winner) {
			case Players.NONE:
				
				GameObject parachute = Registry.Find("Parachute");
				parachute.SetActive(false);
				GameObject player1 = Registry.Find("Player1");
				player1.SetActive(false);
				GameObject player2 = Registry.Find("Player2");
				player2.SetActive(false);
				break;
			case Players.P1:

				break;
			case Players.P2:

				break;
		} 
	}
}
