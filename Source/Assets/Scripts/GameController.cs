using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject flyingObstacle;
	public Vector3 spawnValues;
	public int obstacleCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public int sec=0;
	public float timecount=0;
	public float starttime=0;
	public int parachutePopTime;

	// Use this for initialization
	void Start () {
		starttime = Time.time;
		StartCoroutine (SpawnWaves ());

	}
	
	// Update is called once per frame
	void Update () {
		timecount = Time.time - starttime;
		sec = (int)(timecount % 60f);
		if (sec == parachutePopTime) {
			//parachute appears from down the screen
		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);
		while (true) {
			for (int i = 0; i < obstacleCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = new Quaternion ();
				Instantiate (flyingObstacle, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
		}
	}
	
}
