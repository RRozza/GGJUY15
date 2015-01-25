using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	private List<GameObject> spawns = new List<GameObject> ();
	private ArrayList  spawnsIds = new ArrayList();
	private Vector3 parachuteStartPosition = new Vector3 (0, -6, 0);
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
		spawnsIds.Add ("Seat");
		spawnsIds.Add ("Bag");
		spawnsIds.Add ("Turbine");
		spawnsIds.Add ("Metal");
		spawnsIds.Add ("Tail");

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
									spawns.Remove (spawn);
									//GameObject.Destroy(spawn);
							}
					}
		
					GameObject parachute = Registry.Find ("Parachute");
		
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
							Players player = (Context.SharedInstance.parachute_state == ParachuteState.P1) ? Players.P1 : Players.P2;
							if (Context.SharedInstance.isKeyPress (player, Keys.PUNCH)) {
								EndGame (player);
							}
					}
		
					//Timeover
					if (Context.SharedInstance.timerSeconds == gameEndTime) {
						EndGame (Players.NONE);
					}
		
					timecount = Time.time - Context.SharedInstance.startTime;
					Context.SharedInstance.updateTimer ((int)(timecount % 60f));
	
			} else {
				foreach (GameObject spawn in spawns) {
					spawn.SetActive(false);
					//GameObject.Destroy(spawn);					
				}
			}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);

		bool first = true;

		for (int i=1; i < obstacleCount; i++) {
			if (first){
				yield return new WaitForSeconds(spawnWait*2);
				first = false;
			}

			int index = Random.Range(0,5);	

			Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
			GameObject spawn = Instantiate(Registry.Find (spawnsIds[index] as string), spawnPosition, new Quaternion()) as GameObject;

			spawns.Add(spawn);

			yield return new WaitForSeconds(spawnWait);
		}
	}
		
	void EndGame (Players winner)
	{
		Context.SharedInstance.gameEnded = true;
		Context.SharedInstance.winner = winner;

		GameObject parachute = Registry.Find("Parachute");
		parachute.SetActive(false);
		GameObject player1 = Registry.Find("Player1");
		player1.SetActive(false);
		GameObject player2 = Registry.Find("Player2");
		player2.SetActive(false);
	}
}
