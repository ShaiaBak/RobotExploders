using UnityEngine;
using System.Collections;

public class BetaGolem: Golem {

	public GameObject projectilePrefab;
	private float cooldownTimer = 0f;
	private float cooldownEnd = .5f;
	private bool diveEnabled = false;
	public enum listOfMoves {nothing, Attack1, Attack2, Attack3};
	public listOfMoves lastAttack;
	/* OLD DIVE ATTACK VARIABLES
	private float counter = 0f;
	public float diveScaleX = 0.1f;
	public float diveScaleY = 0.3f;
	public float diveDistanceScale = 10;
	private bool startFromGround = false;
	public bool divingCrash = false;
	*/
	void Awake(){
		projectilePrefab = Resources.golemProjectile;
		lastAttack = listOfMoves.nothing;
	}

	protected override void HandleAttack(){
		// Machine Gun
		if(Input.GetButton(controls.fireA) && CheckAnimationCooldown(lastAttack)){
			Shoot(true,10,2,GetFacingDirection(),1,0);
			lastAttack = listOfMoves.Attack1;
		}
		// Hammer Fist Combo 
		if(Input.GetButton(controls.fireB) && CheckAnimationCooldown(lastAttack)){

			//Shoot(true,10,0.02f,GetFacingDirection(),1,0);
			
			HammerAttack(true,15,0.1f,GetFacingDirection(),1,4);

			//SoundNotificationController.CreateSound(transform.position,0,2);
			lastAttack = listOfMoves.Attack2;
		}
		// Special 
		if(Input.GetButton(controls.fireC) && CheckAnimationCooldown(lastAttack)){
			//StartCoroutine(Melee(GetFacingDirection()));
			//SoundNotificationController.CreateSound(transform.position,0,2);
			lastAttack = listOfMoves.Attack3;
		}
		cooldownTimer += Time.deltaTime;
	}

	// Short cooldown in between attacks/different attacks
	private bool CheckAnimationCooldown(listOfMoves prevAttack){
		// Check cooldown period

		// Different attacks have different cooldowns 
		switch (prevAttack) {
			case listOfMoves.Attack1:
				cooldownEnd = 0.1f;
				break;
			case listOfMoves.Attack2:
				cooldownEnd = 0.2f;
				break;
			default:
				cooldownEnd = 0.5f;
				break;
			}
		if(cooldownTimer >= cooldownEnd){
			cooldownTimer = 0;
			return true;
		}
		return false;
	}

	private void Shoot(bool isPiercing, float ms, float dur, Vector2 dir, int dmg, float knock) {

		//Shoots infront of the golem
		Vector2 pos = new Vector2( transform.position.x + dir.x + 0.5f, transform.position.y + dir.y);
		
		GameObject proj = (GameObject) Instantiate(projectilePrefab, pos, Quaternion.identity);
		Projectile p = proj.GetComponent<Projectile>();
		
		// Make the projectile shoot to the direction of DIR for DUR sec with a movespeed of MS, damage of DMG
		p.SetParameters(isPiercing, ms, dur, dir, dmg, 0, 0, knock);
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), p.GetComponent<Collider2D>());
	}

	private void HammerAttack(bool isPiercing, float ms, float dur, Vector2 dir, int dmg, float knock) {
		Vector2 pos = new Vector2( transform.position.x + dir.x, transform.position.y + 1);
		
		GameObject proj = (GameObject) Instantiate(projectilePrefab, pos, Quaternion.identity);
		Projectile p = proj.GetComponent<Projectile>();
		p.transform.parent = this.transform;

		p.SetParameters(isPiercing, ms, dur, new Vector2(0,-1), dmg, 0, 0, knock);
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), p.GetComponent<Collider2D>());	
	}

	
	// Fires 3 projectiles NE, E, SE in quick succession to imitate a downward slash (if facing right)
	private IEnumerator Melee(Vector2 dir){
		//(time*deltatime)*speed=dist
		//(time*deltatime)=dist/speed
		float duration = .1f;

		// Shoot at dir - 45 degrees
		Quaternion rotation = Quaternion.AngleAxis(-45, Vector3.forward);
		Vector2 direction = rotation*dir;
		Shoot(true,10,duration,direction,1,0);
		yield return new WaitForSeconds(.05f);
		
		// Shoot at dir
		Shoot(true,10,duration,dir,1,0);
		yield return new WaitForSeconds(.05f);

		// Shoot at dir + 45 degrees
		rotation = Quaternion.AngleAxis(45, Vector3.forward);
		direction = rotation*dir;
		Shoot(true,10,duration,direction,1,0);
	}


	private IEnumerator AlphaDiveProj() {

		GetComponent<Rigidbody2D>().AddForce(new Vector2 (0,-20f),ForceMode2D.Impulse);
		//Set position for creating the projectile
		Vector2 pos = new Vector2(transform.position.x, transform.position.y-1f);
		
		GameObject proj = (GameObject) Instantiate(projectilePrefab, pos, Quaternion.identity);
		Projectile p = proj.GetComponent<Projectile>();
		//Set the parent of the projectile to be the golem
		p.transform.parent = this.transform;
		// Make the projectile shoot to the direction of DIR for DUR sec with a movespeed of MS, damage of DMG
		p.SetParameters(false, 0.1f, 2f, new Vector2(0,0) ,1, 0, 0, 0);
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), p.GetComponent<Collider2D>());
		yield return new WaitForSeconds(0.05f);
	}
}
