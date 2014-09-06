using UnityEngine;
using System.Collections;

// TODO: :) Everything.
public class GolemHealthSystem : HealthSystem {
	private GameController gameController;
	
	protected override void HandleDeath(){

		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		gameController.GameOver();

		renderer.material.color = Color.gray;
		collider2D.enabled = false;

		tag = "Dead";
	}
}
