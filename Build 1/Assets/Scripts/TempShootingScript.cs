using UnityEngine;
using System.Collections;
// TODO: Replace this whole script with the real thing. This is only for testing
public class TempShootingScript : MonoBehaviour {

	public GameObject projectilePrefab;
	public bool isP1 = true;
	private float cooldownTimer = 0f;
	private float cooldownEnd = .5f;
	
	// Update is called once per frame
	void Update () {
		// P1 Non-piercing
		if(Input.GetButtonDown("Fire1a") && isP1 && CheckAnimationCooldown()){
			Shoot(false,3,2,Vector2.right,1);
		}
		// P1 Piercing 
		if(Input.GetButtonDown("Fire1b") && isP1 && CheckAnimationCooldown()){
			Shoot(true,3,2,Vector2.right,1);
		}
		// P1 Special 
		if(Input.GetButtonDown("Fire1c") && isP1 && CheckAnimationCooldown()){
			StartCoroutine(Melee ());
		}

		// P2 Non-piercing
		if(Input.GetButtonDown("Fire2a") && !isP1 && CheckAnimationCooldown()){
			Shoot(false,3,2,Vector2.right,1);
		}
		// P2 Piercing 
		if(Input.GetButtonDown("Fire2b") && !isP1 && CheckAnimationCooldown()){
			Shoot(true,3,2,Vector2.right,1);
		}
		// P2 Melee 
		if(Input.GetButtonDown("Fire2c") && !isP1 && CheckAnimationCooldown()){
			StartCoroutine(Melee ());
		}
		cooldownTimer += Time.deltaTime;
	}

	// Short cooldown in between attacks/different attacks
	private bool CheckAnimationCooldown(){
		// Check cooldown period
		if(cooldownTimer >= cooldownEnd){
			cooldownTimer = 0;
			return true;
		}
		return false;
	}

	private void Shoot(bool isPiercing, float ms, float dur, Vector2 dir, int dmg){
		// Set position for creating the projectile
		//Vector2 pos = new Vector2(transform.position.x+.4f, transform.position.y);
		Vector2 pos = transform.position;
			
		GameObject proj = (GameObject) Instantiate(projectilePrefab, pos, Quaternion.identity);
		Projectile p = proj.GetComponent<Projectile>();
			
		// Make the projectile shoot to the direction of DIR for DUR sec with a movespeed of MS, damage of DMG
		p.SetParameters(isPiercing, ms, dur, dir, dmg, 0, 0, 0);
		Physics2D.IgnoreCollision(collider2D, p.collider2D);
	}

	// Fires 3 projectiles NE, E, SE in quick succession to imitate a downward slash
	private IEnumerator Melee(){
		//(time*deltatime)*speed=dist
		//(time*deltatime)=dist/speed
		float duration = .06f;

		Vector2 direction = new Vector2(1,1);
		Shoot(true,10,duration,direction,1);
		yield return new WaitForSeconds(.05f);

		direction = new Vector2(1,0);
		Shoot(true,10,duration,direction,1);
		yield return new WaitForSeconds(.05f);

		direction = new Vector2(1,-1);
		Shoot(true,10,duration,direction,1);
		yield return new WaitForSeconds(.05f);
	}
}
