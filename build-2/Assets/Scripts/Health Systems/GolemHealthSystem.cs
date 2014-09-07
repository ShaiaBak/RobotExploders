using UnityEngine;
using System.Collections;

// TODO: :) Everything.
public class GolemHealthSystem : HealthSystem {
	private GameController gameController;
	public int escapeTime = 2;

	protected override void HandleDeath(){
		GetComponent<Golem>().enableControl = false;
		StartCoroutine(Wait());
	}
	IEnumerator Wait() {
		yield return new WaitForSeconds(escapeTime);

		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		gameController.GameOver();
		
		renderer.material.color = Color.gray;
		collider2D.enabled = false;
		
		tag = "Dead";
	}
}
