using UnityEngine;
using System.Collections;
// TODO: Replace this whole script with the real thing. This is only for testing
public class TempShootingScript : MonoBehaviour {

	public GameObject projectilePrefab;
	public bool isP1 = true;
	
	// Update is called once per frame
	void Update () {
		// P1 Non-piercing
		if(Input.GetButtonDown("Fire1a") && isP1){
			Shoot(false);
		}
		// P1 Piercing 
		if(Input.GetButtonDown("Fire1b") && isP1){
			Shoot(true);
		}
		// P1 Special 
		if(Input.GetButtonDown("Fire1c") && isP1){
			Debug.Log("P1 special ability");
		}

		// P2 Non-piercing
		if(Input.GetButtonDown("Fire2a") && !isP1){
			Shoot(false);
		}
		// P2 Piercing 
		if(Input.GetButtonDown("Fire2b") && !isP1){
			Shoot(true);
		}
		// P1 Piercing 
		if(Input.GetButtonDown("Fire2c") && !isP1){
			Debug.Log("P2 special ability");
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
