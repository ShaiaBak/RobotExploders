﻿using UnityEngine;
using System.Collections;

// TODO: :) Everything.
public class PlayerHealthSystem : HealthSystem {
	private GameController gameController;

	protected override void HandleDeath(){
		gameController = GameObject.Find("GameController").GetComponent<GameController>();

		renderer.material.color = Color.gray;
		gameController.GameOver();
		//@todo: take out for more elegant solution
		collider2D.enabled = false;

		tag = "Dead";
	}
}
