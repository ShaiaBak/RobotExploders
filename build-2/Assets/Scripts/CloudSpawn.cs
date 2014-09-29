using UnityEngine;
using System.Collections;

public class CloudSpawn : MonoBehaviour {
	public GameObject Cloud;
	public Vector2 spawnValues;
	public int cloudCount = 10;
	public float cloudWait;

	// on start cloud variables
	public Vector2 spawnValuesBegin;
	public int cloudCountBegin = 10;

	void Start()
	{
		StartCoroutine ("cloudSpawnLeft");
		StartCoroutine ("cloudSpawnBegin");
		//cloudSpawnLeft ();
		//cloudSpawnRight ();
	}

	IEnumerator cloudSpawnLeft(){
			for(int i = 0; i < cloudCount; i++){
				Vector2 spawnPosition = new Vector2 (Random.Range(-spawnValues.x, spawnValues.x) + transform.position.x, Random.Range (-spawnValues.y, spawnValues.y) + transform.position.y);
				GameObject CloudLeft = (GameObject) Instantiate (Cloud, spawnPosition, transform.rotation);
			}
			yield return new WaitForSeconds(cloudWait);
			StartCoroutine ("cloudSpawnLeft");
	}
	IEnumerator cloudSpawnBegin(){
		for(int i = 0; i < cloudCountBegin; i++){
			Vector2 spawnPosition = new Vector2 (Random.Range(-spawnValuesBegin.x, spawnValuesBegin.x) + transform.position.x - 20, Random.Range (-spawnValuesBegin.y, spawnValuesBegin.y));
			GameObject CloudBegin = (GameObject) Instantiate (Cloud, spawnPosition, transform.rotation);
		}
		return null;
	}
}