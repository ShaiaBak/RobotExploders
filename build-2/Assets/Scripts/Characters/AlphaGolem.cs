using UnityEngine;
using System.Collections;

public class AlphaGolem : Golem {

	public GameObject projectilePrefab;
	private float cooldownTimer = 0f;
	private float cooldownEnd = .5f;

	void Awake(){
		projectilePrefab = Resources.golemProjectile;
	}

	protected override void HandleAttack(){
		// Non-piercing
		if(Input.GetButton(controls.fireA) && CheckAnimationCooldown()){
			Shoot(false,3,2,GetFacingDirection(),1);
		}
		// Piercing 
		if(Input.GetButton(controls.fireB) && CheckAnimationCooldown()){
			Shoot(true,3,2,GetFacingDirection(),1);
		}
		// Special 
		if(Input.GetButton(controls.fireC) && CheckAnimationCooldown()){
			StartCoroutine(Melee(GetFacingDirection()));
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
	
	// Fires 3 projectiles NE, E, SE in quick succession to imitate a downward slash (if facing right)
	private IEnumerator Melee(Vector2 dir){
		//(time*deltatime)*speed=dist
		//(time*deltatime)=dist/speed
		float duration = .1f;

		// Shoot at dir - 45 degrees
		Quaternion rotation = Quaternion.AngleAxis(-45, Vector3.forward);
		Vector2 direction = rotation*dir;
		Shoot(true,10,duration,direction,1);
		yield return new WaitForSeconds(.05f);
		
		// Shoot at dir
		Shoot(true,10,duration,dir,1);
		yield return new WaitForSeconds(.05f);

		// Shoot at dir + 45 degrees
		rotation = Quaternion.AngleAxis(45, Vector3.forward);
		direction = rotation*dir;
		Shoot(true,10,duration,direction,1);
	}

}
