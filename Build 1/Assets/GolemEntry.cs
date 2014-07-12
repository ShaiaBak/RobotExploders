using UnityEngine;
using System.Collections;

public class GolemEntry : MonoBehaviour {
	public Transform golem;

	void Update() {
		transform.position = new Vector2(golem.position.x, golem.position.y);
	}
}