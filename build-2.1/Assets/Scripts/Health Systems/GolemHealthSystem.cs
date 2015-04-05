using UnityEngine;
using System.Collections;

// TODO: :) Everything.
public class GolemHealthSystem : HealthSystem {
	private GameController gameController;
	public int escapeTime = 2;

	protected override void HandleDeath(){


		StartCoroutine(Wait());

	}
	IEnumerator Wait() {
		Golem gs = GetComponent<Golem>();
		gs.enableControl = false;
		yield return new WaitForSeconds(escapeTime);

		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		gameController.GameOver();
		
		GetComponent<Renderer>().material.color = Color.gray;
		GetComponent<Collider2D>().enabled = false;
		
		tag = "Dead";

		// Kill pilot if still inside the golem
		if (gs.currentPilot != null) {
			Debug.Log("pilot die");
			gs.currentPilot.GetComponent<PlayerHealthSystem>().SetHealth(0);
		}
	}
}
