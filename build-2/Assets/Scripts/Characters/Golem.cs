using UnityEngine;
using System.Collections;

public class Golem : MonoBehaviour {
	
	public ControlScheme controls;

	public float maxSpeed = 2f; 			//Arbitrary speed value
	public bool facingRight = true;
	public float jumpForce = 800f;			//Arbitrary jump value
	public bool enableControl = true;
	public GameObject currentPilot;

	//Directions
	[SerializeField]
	private Vector2 facingDirection;

	public Animator anim;
	
	//--------Input Variables--------//
	private float moveH = 0f;
	private float moveV = 0f;
	private bool jumpPress;					//When the Jump button is pressed
	private bool jumpRelease;				//When the Jump button is released
	private bool jumpHeld;					//When the Jump button is held
	public bool enterGolemPress;			//Input for enter or exiting the golem is pressed
	public bool enterGolemHeld;			//Input for enter or exiting the golem is pressed
	// public bool enterGolemRelease;			//Input for enter or exiting the golem is released
	
	public bool grounded = false;			//checks if object on the ground
	public bool doubleJump = true;			//True = Doublejump is available
	public bool flyingMode = false;			//True = Flying is active
	private float flyingModeTimer = 0;		//The Timer for flying mode
	private float flyingModeDuration = 2;	//Total Duration of the flight
	public float exitTimer = 0;			
	public float timeToExit = .5f;			//timeToExit must match the blendtree time
	private bool exiting = false;

	// public bool pilotNearby = false;		
	// public float enterTimerFromPilot = 0;


	private float colliderSize;
	public Transform groundCheck;
	//public Transform roofCheck;
	private Vector2 gCheck1;
	private Vector2 gCheck2;
	private int i;
	private Transform direction;
	//public bool roofHit = false;

	private Transform eUI;


	void Start () {
		anim = GetComponent<Animator>();
		eUI = this.transform.FindChild("Enter-UI");
		//Find the child, GroundCheck, of the object and assign it as the ground check
		groundCheck = this.transform.FindChild("GroundCheck");

		direction = this.transform.FindChild("Direction");

		//FOR BOX COLLIDER
		colliderSize = gameObject.GetComponent<BoxCollider2D>().size.x;

	}
	
	void Update(){
		//Debug.Log(enterTimerFromPilot);
		// Gets dynamic control scheme
		// TODO: don't grab controls every frame; grab them in control selection screen
		if(controls != null){
			moveH = Input.GetAxis (controls.horizontal);
			moveV = Input.GetAxis (controls.vertical);
			jumpPress = Input.GetButtonDown (controls.jump);
			jumpRelease = Input.GetButtonUp (controls.jump);
			jumpHeld = Input.GetButton (controls.jump);
			enterGolemPress = Input.GetButtonDown (controls.enter);
			enterGolemHeld = Input.GetButton (controls.enter);
			// Handles special attacks
			HandleAttack();
		}else{
			moveH = 0;
			moveV = 0;
			jumpPress = false;
			jumpRelease = false;
			enterGolemPress = false;
		}
		//enableControl is only used for potential ideas later. If true you have normal movement
		//if false, controls do nothing.
					
		//This checks the object "groundCheck", gives it a radius of "groundRadius"
		//If the "groundCheck" overlaps with anything that is tagged "whatIsGround"
		//the unit will be considered on the ground, grounded = true
		LayerMask whatIsGround = 1 << LayerMask.NameToLayer("Environment");
		//FOR BOX COLLIDER

		//Vector2 gCheck1 = new Vector2(groundCheck.position.x - colliderSize, groundCheck.position.y+0.05f);
		//Vector2 gCheck2 = new Vector2(groundCheck.position.x + colliderSize, groundCheck.position.y-0.05f);
		//grounded = Physics2D.OverlapArea (gCheck1, gCheck2, whatIsGround);

		//FOR CIRCLE COLLIDER
		float groundRadius = 0.1f;
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		anim.SetBool ("Grounded", grounded);
		/*
		roofHit = Physics2D.OverlapCircle (roofCheck.position, 0.2f, whatIsRoof);
		if (roofHit) {
			Debug.Log("HIT");
		}
		*/
		if (enableControl) {
		
			//When the vertical speed is not zero, change to the jumping/falling animation
			//anim.SetFloat ("speedV", rigidbody2D.velocity.y);
			
			//When the horizontal speed is not zero, change to the walking animation
			anim.SetFloat ("speedH", Mathf.Abs (moveH));

			CheckJumping();
			CheckFlying();

			DirectionCheck();
			rigidbody2D.velocity = new Vector2 (moveH * maxSpeed, rigidbody2D.velocity.y);

		} else {
			moveH = 0f; 
		}
		CheckExiting();
	}

	protected virtual void HandleAttack(){
		// Stub for subclass
	}
	/* OLD OLD OLD
	private void CheckJumping(){
		//When player is on the ground, second jump is available
		if (grounded) {
			doubleJump = true;
			//When jump button is pressed, add a force upwards
			if (jumpPress) {
				rigidbody2D.AddForce (new Vector2 (0, jumpForce));
			}
			//if second jump is available and the jump button is pressed
			//enter flying mode, turn off gravity and reset the timer
		
		} else if (doubleJump && jumpPress) {
			doubleJump = false;
			flyingMode = true;
			rigidbody2D.gravityScale = 0;
			flyingModeTimer = flyingModeDuration;
		}
	}
	private void CheckFlying(){
		//When flying, reduce timer by the time it took to complete the last frame
		if (flyingMode) {
			flyingModeTimer -= Time.deltaTime;
		}
		//When timer is equal to or less than zero or when the jump button is released
		//Disable flying mode and restore the gravity setting
		if (flyingModeTimer <= 0 || jumpRelease) {
			flyingMode = false;
			rigidbody2D.gravityScale = 1;
		}
		// If the player has activated "Flying Mode", the player can move freely in the X and Y
		// If not, the player can only move in the X
		if (flyingMode){
			rigidbody2D.velocity = new Vector2 (moveH * maxSpeed, moveV * maxSpeed);
		} else {
			rigidbody2D.velocity = new Vector2 (moveH * maxSpeed, rigidbody2D.velocity.y);
		}
	}
	// OLD OLD OLD 
	*/

	// START OF VERSION 2
	private void CheckJumping(){
		//When player is on the ground, second jump is available
		if (grounded) {
			anim.SetBool("Jumped", jumpPress);
			doubleJump = true;
			//When jump button is pressed, add a force upwards
			if (jumpPress) {
				rigidbody2D.AddForce (new Vector2 (0, jumpForce));
				
			}
			//if second jump is available and the jump button is pressed
			//enter flying mode, turn off gravity and reset the timer
		} else if (doubleJump) {
			doubleJump = false;
			flyingMode = true;
			flyingModeTimer = flyingModeDuration;
		}
	}

	private void CheckFlying(){
		//When flying, reduce timer by the time it took to complete the last frame
		if (flyingMode && jumpHeld) {
			flyingModeTimer -= Time.deltaTime;
			rigidbody2D.AddForce (new Vector2 (0, jumpForce/25f));
		}
		//When timer is equal to or less than zero or when the jump button is released
		//Disable flying mode and restore the gravity setting
		if (flyingModeTimer <= 0 || grounded) {
			flyingMode = false;
		}
		// If the player has activated "Flying Mode", the player can move freely in the X and Y
		// If not, the player can only move in the X
		/*
		if (flyingMode){
			rigidbody2D.AddForce (new Vector2 (0, jumpForce/25f));
		} else {
		}*/
	}
	// END OF VERSION 2
	//Exits the golem when held for .5 seconds
	private void CheckExiting() {
		//Find a nearby golem
		if( enterGolemPress || exiting ){
			//Check if button is held long enough
			if (enterGolemHeld) {
				exiting = true;
				exitTimer = exitTimer + Time.deltaTime;
				if(exitTimer >= timeToExit) {
					//print ("exit");
					exitTimer = 0;
					exiting = false;
					ExitGolem();
				}
			} else {
				exiting = false;
			}
		} else {
			// exitTimer = 0;
			exitTimer = Mathf.Lerp(exitTimer, 0, Time.time);
			exiting = false;
			// Debug.Log(exitTimer);
		}
	}
	
	private void ExitGolem(){
		// Reset pilot variables
		Pilot ps = currentPilot.GetComponent<Pilot>();
		ps.controls = controls;
		ps.enabled = true;
		ps.enableControl = true;
		ps.currentGolem = null;
//		ps.facingRight = facingRight;
		currentPilot.collider2D.enabled = true;
		currentPilot.rigidbody2D.isKinematic = false;
		currentPilot.transform.parent = null;
//		currentPilot.GetComponent<TempShootingScript>().enabled = true;


		// Vertically flips the entry UI, an asthetic change just for exiting
		eUI.GetComponent<enterUI>().flipVertical();



		// Reset golem variables
		gameObject.layer = LayerMask.NameToLayer("Deactivated");
		currentPilot = null;
		controls = null;
	}
	
	private void Flip () {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	

		//Flips the Enter UI (child) as well so the UI stays
		//consistently the same
		Vector3 childScale = eUI.transform.localScale;
        childScale.x  *= -1;
        eUI.transform.localScale= childScale;

	}
	
	private void DirectionCheck() {
		// Flip the image if it is moving to left while facing right or moving right while facing left
		if (moveH > 0 && !facingRight) {
			Flip ();
		} else if (moveH < 0 && facingRight) {
			Flip ();
		}
		
		//Top Right/Left, NE/NW
		if ((moveH >= 0 && moveV >= 0) || (moveH <= 0 && moveV >= 0)) {
			direction.localPosition = new Vector2(1, 1);
			if(facingRight){
				facingDirection = new Vector2(1,1);	//NE
			}else{
				facingDirection = new Vector2(-1,1);	//NW
			}
		}
		
		//Bottom Right/Left
		if ((moveH >= 0 && moveV <= 0) || (moveH <= 0 && moveV <= 0)) {
			direction.localPosition = new Vector2(1, -1);
			if(facingRight){
				facingDirection = new Vector2(1,-1); //SE
			}else{
				facingDirection = new Vector2(-1,-1); //SW
			}
		}
		
		//Top
		if (moveH == 0 && moveV >= 0) { 
			direction.localPosition = new Vector2(0, 1);
			facingDirection = new Vector2(0,1); //N
		}		
		
		//Bottom
		if (moveH == 0 && moveV <= 0) { 
			direction.localPosition = new Vector2(0, -1);
			facingDirection = new Vector2(0,-1); //S
		}	
		
		//Straight Right/Left
		if ((moveH >= 0 && moveV == 0) || (moveH <= 0 && moveV == 0)) {
			direction.localPosition = new Vector2(1, 0);
			if(facingRight){
				facingDirection = new Vector2(1,0); //E
			}else{
				facingDirection = new Vector2(-1,0); //W
			}
		}
	}

	public Vector2 GetFacingDirection(){
		return facingDirection;
	}
}
