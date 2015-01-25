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
		public int sec = 0;
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
				timecount = Time.time - starttime;
				sec = (int)(timecount % 60f);
				if (sec >= parachutePopTime) {
						if (parachute.transform.position.y < parachuteTrembleY) {
								parachute.transform.position += (Vector3.up * parachuteAsensionSpeed * Time.deltaTime);
								//parachute appears from down the screen
						} else if (parachute.transform.position.y >= parachuteTrembleY - 1) {
								Context.SharedInstance.startParachuteTremble ();
						}
				}
				if (sec >= parachuteOpenTime) {
						Context.SharedInstance.enableParachuteOpening ();
				}
				if (sec == this.gameEndTime) {
						//GAME END LOGIC
				
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
								Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
								Quaternion spawnRotation = new Quaternion ();
								Instantiate (flyingObstacle, spawnPosition, spawnRotation);
								yield return new WaitForSeconds (spawnWait);
						}
						yield return new WaitForSeconds (waveWait);
				}
		}
	
}
