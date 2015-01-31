using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {
	private ArrayList spawnsIds;
	
	public int obstacleCount;
	public Vector3 spawnValues;
	public float spawnWait, startWait; 

	void Start() {
		spawnsIds = new ArrayList();

		spawnsIds.Add("Seat");
		spawnsIds.Add("Bag");
		spawnsIds.Add("Turbine");
		spawnsIds.Add("Metal");
		spawnsIds.Add("Tail");

		StartCoroutine(SpawnWaves());
	}

	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds(startWait);
		
		bool first = true;
		int i = 1;
		while (i < obstacleCount) {
			if (first){
				yield return new WaitForSeconds(spawnWait*2);
				first = false;
			}

			if (Context.Instance.GameState() == GameStates.STARTED) {
				int index = Random.Range(0,5);	

				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				GameObject spawn = Instantiate(Registry.Find (spawnsIds[index] as string), spawnPosition, new Quaternion()) as GameObject;

				spawn.AddComponent("ObjectsBehaviour");
				i++;
			}

			yield return new WaitForSeconds(spawnWait);	
		}
	}
}
