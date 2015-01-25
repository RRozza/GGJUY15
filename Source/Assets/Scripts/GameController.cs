using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	private List<GameObject> spawns = new List<GameObject> ();
	private ArrayList  spawnsIds = new ArrayList();

	public Vector3 spawnValues;
	public int obstacleCount;
	public float spawnWait;
	public float startWait;
	public float spawnSpeed; 

	void Start () {
		StartCoroutine(SpawnWaves());

		spawnsIds.Add ("Seat");
		spawnsIds.Add ("Bag");
	}

	void Update() {
		foreach (GameObject spawn in spawns) {
			spawn.transform.position += (Vector3.up * spawnSpeed * Time.deltaTime);	

			if (spawn.transform.position.y > 7.0f) {
				spawns.Remove(spawn);
				//GameObject.Destroy(spawn);
			}
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
	
}
