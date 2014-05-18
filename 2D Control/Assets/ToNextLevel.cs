using UnityEngine;
using System.Collections;

public class ToNextLevel : MonoBehaviour {
	public string level;
	public GameController gameController;
	
	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Player") {
			Debug.Log("in Frame");
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				Debug.Log("Door");

				PlayerPrefs.SetFloat ("SpawnPosX", transform.position.x);
				PlayerPrefs.SetFloat ("SpawnPosY", transform.position.y);
				Application.LoadLevel(level);
			}
		}
	}
}
