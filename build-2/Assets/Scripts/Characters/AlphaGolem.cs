using UnityEngine;
using System.Collections;

public class AlphaGolem : Golem {

	public GameObject projectilePrefab;
	private float cooldownTimer = 0f;
	private float cooldownEnd = .5f;
	private bool diveEnabled = false;
	private float counter = 0f;
	public float diveScaleX = 0.1f;
	public float diveScaleY = 0.3f;
	public float diveDistanceScale = 10;


	void Awake(){
		projectilePrefab = Resources.golemProjectile;
	}

	protected override void HandleAttack(){
		// Non-piercing
		if(Input.GetButton(controls.fireA) && CheckAnimationCooldown()){
			//Shoot(false,3,2,GetFacingDirection(),1);

			diveEnabled = true;

		}
		// Piercing 
		if(Input.GetButton(controls.fireB) && CheckAnimationCooldown()){
			Shoot(true,3,2,GetFacingDirection(),1);
			SoundNotificationController.CreateSound(transform.position,0,1);
		}
		// Special 
		if(Input.GetButton(controls.fireC) && CheckAnimationCooldown()){
			StartCoroutine(Melee(GetFacingDirection()));
			SoundNotificationController.CreateSound(transform.position,0,1);
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


	private IEnumerator DiveLost(){

		//Pushes the golem straight down
		rigidbody2D.AddForce(new Vector2 (0,-10),ForceMode2D.Impulse);
		//Shoots projectile to simulate melee attack
		Shoot(false,10,1f,new Vector2 (0,-1),1);

		yield return new WaitForSeconds(0.05f);

	}

	private void diveJump() {
		// TODO: Add in direction of Golem for the dive
		// TODO: golem needs to move straight down while in air: check the grounded variable of the golem similar to the jump function
		// TODO: needs to bounce off the object that it collides with, do something similar to diveJump
		//

		if (diveEnabled){
			//Quaternion rotation = Quaternion.AngleAxis(180, Vector3.forward);




			counter += Time.deltaTime/3f;
			transform.position = new Vector2 (transform.position.x+diveScaleX, transform.position.y + Mathf.Cos(counter*diveDistanceScale)*diveScaleY);
		}
		if (counter >= Mathf.PI/(2*diveDistanceScale)) {
			diveEnabled = false;
			counter = 0;
			StartCoroutine(DiveLost());
		}
	}

	void FixedUpdate(){
		diveJump();
	}
}
