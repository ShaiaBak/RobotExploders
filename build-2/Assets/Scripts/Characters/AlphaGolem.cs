using UnityEngine;
using System.Collections;

public class AlphaGolem : Golem {

	public GameObject projectilePrefab;
	private float cooldownTimer = 0f;
	private float cooldownEnd = .5f;
	private bool diveEnabled = false;
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
	}

	protected override void HandleAttack(){
		// Non-piercing
		if(Input.GetButton(controls.fireA) && CheckAnimationCooldown()){
			//Shoot(false,3,2,GetFacingDirection(),1);
			//startFromGround = grounded;
			//diveEnabled = true;
			//enableControl= false;

			if (!grounded){
				StartCoroutine(DiveLostProj());
				diveEnabled = true;
			}  else {
				StartCoroutine(DiveLostProj2());
			}
		}
		// Piercing 
		if(Input.GetButton(controls.fireB) && CheckAnimationCooldown()){
			Shoot(true,3,2,GetFacingDirection(),1);
			SoundNotificationController.CreateSound(transform.position,0,2);
		}
		// Special 
		if(Input.GetButton(controls.fireC) && CheckAnimationCooldown()){
			StartCoroutine(Melee(GetFacingDirection()));
			SoundNotificationController.CreateSound(transform.position,0,2);
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
		//Vector2 pos = new Vector2(transform.position.x, transform.position.y+1f);
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


	private IEnumerator DiveLostProj(){

		rigidbody2D.AddForce(new Vector2 (0,-20f),ForceMode2D.Impulse);
		//Set position for creating the projectile
		Vector2 pos = new Vector2(transform.position.x, transform.position.y-1f);
		
		GameObject proj = (GameObject) Instantiate(projectilePrefab, pos, Quaternion.identity);
		Projectile p = proj.GetComponent<Projectile>();
		//Set the parent of the projectile to be the golem
		p.transform.parent = this.transform;
		// Make the projectile shoot to the direction of DIR for DUR sec with a movespeed of MS, damage of DMG
		p.SetParameters(false, 0.1f, 100f, new Vector2(0,0) ,1, 0, 0, 0);
		Physics2D.IgnoreCollision(collider2D, p.collider2D);
		yield return new WaitForSeconds(0.05f);
	}
	private IEnumerator DiveLostProj2(){
		diveEnabled = false;
		Shoot(true,25,1f,new Vector2 (1,0),1);
		Shoot(true,25,1f,new Vector2 (-1,0),1);

		yield return new WaitForSeconds(0.05f);
	}
	#region
	/* OLD DIVE ATTACK
	private void diveJump(Vector2 dir, bool onGround) {
		// TODO: Add in direction of Golem for the dive
		// TODO: golem needs to move straight down while in air: check the grounded variable of the golem similar to the jump function
		// TODO: needs to bounce off the object that it collides with, do something similar to diveJump

		//Jump in an arc first if the dive started from the ground
		if (diveEnabled && onGround) {
			//Quaternion rotation = Quaternion.AngleAxis(180, Vector3.forward);
			enableControl= false;
			counter += Time.deltaTime/3.0f;	
			//diveScaleX: modifies how far FORWARD the golem goes
			//diveScaleY: modifies how far VERTICAL the golem goes
			if (dir.x>=0f) {
				transform.position = new Vector2 (transform.position.x+diveScaleX, transform.position.y + Mathf.Cos(counter*diveDistanceScale)*diveScaleY);
			} else {
				transform.position = new Vector2 (transform.position.x-diveScaleX, transform.position.y + Mathf.Cos(counter*diveDistanceScale)*diveScaleY);
			}
		}

		//After dive arc, go down
		//If you hit the a roof before the arc is completed, go straight down
		if (counter >= Mathf.PI/(2*diveDistanceScale) && onGround || (roofHit && diveEnabled)) {
			counter = 0;
			divingCrash = true;
			StartCoroutine(DiveLostProj());
			//enableControl = true;
			diveEnabled = false;

		}
		//without dive arc
		if (diveEnabled && !onGround) {
			divingCrash = true;
			StartCoroutine(DiveLostProj());
			diveEnabled = false;
		}

		//Landing on the ground
		if (grounded && divingCrash ) {
			divingCrash = false;
			flyingMode = false;
			jumpRelease = false;
			jumpPress = false;
			enableControl = true;
		}
	}

	void FixedUpdate(){
		diveJump(GetFacingDirection(), startFromGround);
	}
	*/
	#endregion
	
	void FixedUpdate() {
		if (diveEnabled && grounded){
			StartCoroutine(DiveLostProj2());
		}
	}
}
