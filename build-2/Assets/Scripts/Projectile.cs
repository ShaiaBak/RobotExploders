using UnityEngine;
using System.Collections;

// NOTE: Constructers shouldn't be used with Monobehaviours, so use SetParameters to set the initial values.
public class Projectile : MonoBehaviour {
	
	private float timeSpentAlive = 0f;
	private float moveSpeed = 0f;
	private float timeUntilDeath = 1f;
	private Vector2 direction;
	private int directDamage = 1;

//	private int aoeDamage = 1;
//	private float explosionAoE = 0f;
	private float knockback = 0f;

	// Use this for initialization
	void Start () {

	}
	
	// FixedUpdate is called once per fixed framerate frame
	void FixedUpdate () {

		//Used for Alpha dive, if the projectile becomes a child of the golem,
		//follow the golems feet
		if (transform.parent == null) {
			rigidbody2D.velocity = direction * moveSpeed; 
		} else if (transform.parent.gameObject.name == "Golem") { 
			transform.position = new Vector2 (transform.parent.position.x,transform.parent.position.y-1.2f);
		} else if (transform.parent.gameObject.name == "BetaGolem"){
			rigidbody2D.velocity = new Vector2 (direction.x * moveSpeed + transform.parent.rigidbody2D.velocity.x, direction.y * moveSpeed );
			//rigidbody2D.velocity.x = direction.x * moveSpeed + transform.parent.rigidbody2D.velocity.x;
			//rigidbody2D.velocity.y = direction.y * moveSpeed;
		} 

		timeSpentAlive += Time.deltaTime;
		if(timeSpentAlive > timeUntilDeath){
			RemoveMe();
		}
	}

	/// <summary>
	/// Sets the initial parameters for the projectile (constructors can't be used with Monobehaviours).
	/// </summary>
	/// <param name="ms">Move speed.</param>
	/// <param name="dur">Duration the projectile will last until its death.</param>
	/// <param name="dir">Direction the projectile will travel.</param>
	/// <param name="dmg">Damage done on contact.</param>
	/// <param name="aoeDmg">Damage done within the explosion blast radius.</param>
	/// <param name="aoe">Explosion blast radius.</param>
	/// <param name="knock">Knockback magnitude.</param>
	public void SetParameters(bool pierce, float ms, float dur, Vector2 dir, int dmg, int aoeDmg, float aoe, float knock){
		// Allows the projectile to go through things if the collider is a trigger
		collider2D.isTrigger = pierce;
		moveSpeed = ms;
		timeUntilDeath = dur;
		direction = dir;
		directDamage = dmg;
//		aoeDamage = aoeDmg;
//		explosionAoE = aoe;
		knockback = knock;
	}

	// For non-piercing projectiles
	void OnCollisionEnter2D(Collision2D col) {
		// TODO: AoE? Knockback?
		HealthSystem hs = col.gameObject.GetComponent<HealthSystem>();
		if(hs != null){
			hs.HurtHealth(directDamage, collider);
		}
		RemoveMe();
	}

	// For piercing projectiles
	void OnTriggerEnter2D(Collider2D col){
		// TODO: AoE? Knockback?
		HealthSystem hs = col.gameObject.GetComponent<HealthSystem>();
		Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
		if(hs != null){
			hs.HurtHealth(directDamage, collider);
		}

		if(rb != null) {
			rb.AddForce (new Vector2 (direction.x*1000f*knockback, direction.y*100f*knockback));
		}
	}

	private void RemoveMe(){
		Destroy(gameObject);
	}
}
