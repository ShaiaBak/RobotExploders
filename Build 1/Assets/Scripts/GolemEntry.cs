using UnityEngine;
using System.Collections;

public class GolemEntry : MonoBehaviour {
	private Transform golem;
	public bool pilotInGolem = false;
	public bool isP1entering = true;

	void Start() {
		//Find golem object position
		golem = transform.parent.Find ("Golem");

	}

	void Update() {

		if (pilotInGolem) {
			//Add a controller to the golem and destroy the entry point
			Debug.Log("Pilot inside Golem");
			golem.GetComponent<GolemController1>().enableControl = true;
			if (isP1entering) {
				golem.GetComponent<GolemController1>().isP1 = true;
			}
			if (!isP1entering) {
				golem.GetComponent<GolemController1>().isP1 = false;
			}
			Destroy(gameObject);
		}
		//follow the golems position
		transform.position = new Vector2(golem.position.x, golem.position.y);
	}
}