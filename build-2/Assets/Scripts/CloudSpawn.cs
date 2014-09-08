using UnityEngine;
using System.Collections;

public class CloudSpawn : MonoBehaviour {
	public GameObject Cloud;
	public Vector2 spawnValues;
	public int cloudCount = 10;
	public bool right;
	public bool left;
	public float cloudWait;

	void Start()
	{ 
		StartCoroutine ("cloudSpawnLeft");
		//cloudSpawnLeft ();
		//cloudSpawnRight ();
	}
	IEnumerator cloudSpawnLeft(){
			Debug.Log ("lol");
			for(int i = 0; i < cloudCount; i++){
				Vector2 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), Random.Range (-spawnValues.y, spawnValues.y));
				GameObject CloudLeft = (GameObject) Instantiate (Cloud, spawnPosition, transform.rotation);
				// does not work
				left = true;
			}
			yield return new WaitForSeconds(cloudWait);
		StartCoroutine ("cloudSpawnLeft");
	}
	void cloudSpawnRight(){
		for(int i = 0; i < cloudCount; i++){
			Vector2 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), Random.Range (-spawnValues.y, spawnValues.y));
			Instantiate (Cloud, spawnPosition, transform.rotation);
			// does not work
			right = true;
		}
	}
	


}