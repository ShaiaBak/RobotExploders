using UnityEngine;
using System.Collections;

public class ToMainLevel : MonoBehaviour {
//	public string level;
//	public Transform spawnPoint;
//	public GameController gameController;

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Player") {
			Debug.Log("in Frame");
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				Debug.Log("Door");

				Application.LoadLevel ("001");
			}
		}
	}
}
