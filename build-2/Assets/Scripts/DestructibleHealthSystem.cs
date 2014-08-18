using UnityEngine;
using System.Collections;

// TODO: :) Everything.
public class DestructibleHealthSystem : HealthSystem {

	public Color newColour;
	public float timeToFade = .5f;

	protected override void HandleDeath(){
		newColour = renderer.material.color; 
		InvokeRepeating("FadeOut",0,Time.deltaTime);
		collider2D.enabled = false;
	}

	// Fade out effect, destroy at 0 alpha
	private void FadeOut(){
		if(newColour.a > 0){
			float delta = 1/(timeToFade/Time.deltaTime);
			newColour = new Color(newColour.r, newColour.g, newColour.b, newColour.a-delta);
			renderer.material.color = newColour;
		}else{
			CancelInvoke();
			Destroy(gameObject);
		}
	}
}
