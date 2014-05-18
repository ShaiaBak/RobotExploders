using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public Transform spawnPoint;

	void Start(){
		PlayerPrefs.SetFloat("SpawnPosX", 0);
		PlayerPrefs.SetFloat("SpawnPosY", 1.25f);
		PlayerPrefs.SetFloat("SpawnPosZ", 0);
	}
	void Awake(){
		DontDestroyOnLoad (this);
	}

	public void createNewSpawnPoint(Transform newSpawnPoint){
		spawnPoint.position = newSpawnPoint.position;
	}
}
