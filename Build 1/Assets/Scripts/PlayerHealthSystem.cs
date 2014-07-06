using UnityEngine;
using System.Collections;

// TODO: :) Everything.
public class PlayerHealthSystem : HealthSystem {

	protected override void HandleDeath(){
		renderer.material.color = Color.gray;
		collider2D.enabled = false;
	}

}
