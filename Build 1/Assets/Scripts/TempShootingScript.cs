using UnityEngine;
using System.Collections;
// TODO: Replace this whole script with the real thing. This is only for testing
public class TempShootingScript : MonoBehaviour {

	public GameObject projectilePrefab;
	
	// Update is called once per frame
	void Update () {
		// Non-piercing
		if(Input.GetButtonDown("Fire1")){
			Shoot(false);
		}

		// Piercing 
		if(Input.GetButtonDown("Fire2")){
			Shoot(true);
		}
	}

	private void Shoot(bool isPiercing){
		// Set position for creating the projectile
		Vector2 pos = new Vector2(transform.position.x+.4f, transform.position.y);
		
		GameObject proj = (GameObject) Instantiate(projectilePrefab, pos, Quaternion.identity);
		Projectile p = proj.GetComponent<Projectile>();
		
		// Make the projectile shoot to the right for 3s with a movespeed of 2, damage of 1
		p.SetParameters(isPiercing, 2f, 3f, Vector2.right, 1, 0, 0, 0);
		Physics2D.IgnoreCollision(collider2D, p.collider2D);
	}
}
