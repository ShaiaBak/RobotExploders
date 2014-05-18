using UnityEngine;
using System.Collections;

public class NewRoom : MonoBehaviour {

	public GameObject moveToDoor;
	private bool inDoor = false;
	private GameObject otherObject;

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Player") {
			otherObject = other.gameObject;
			Debug.Log("in Frame");
			inDoor = true;
			if (Input.GetKeyUp (KeyCode.UpArrow)) {
				Debug.Log("Door");
				other.transform.position = moveToDoor.transform.position;
				other.transform.position -= new Vector3(0.0f, 0.3f, 0.0f);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			inDoor = false;
		}
	}

	void Update() {
	
		if (inDoor && Input.GetKeyUp (KeyCode.UpArrow)) {
			Debug.Log("Door");
			otherObject.transform.position = moveToDoor.transform.position;
			otherObject.transform.position -= new Vector3(0.0f, 0.3f, 0.0f);
		}
	
	}


}
