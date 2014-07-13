using UnityEngine;
using System.Collections;

public class GolemEntry : MonoBehaviour {
	private Transform golem;
	public bool pilotInGolem = false;

	void Start() {
		//Find golem object position
		golem = transform.parent.Find ("Golem");

	}

	void Update() {

		if (pilotInGolem) {
			//Add a controller to the golem and destroy the entry point
			Debug.Log("Pilot inside Golem");
			golem.gameObject.AddComponent<Controller1>();
			DestroyObject(this.gameObject);
		}
		//follow the golems position
		transform.position = new Vector2(golem.position.x, golem.position.y);
	}
}