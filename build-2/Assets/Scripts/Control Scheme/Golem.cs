using UnityEngine;
using System.Collections;

public class Golem : MonoBehaviour {
	
	public ControlScheme controls;

	public float maxSpeed = 2f; 			//Arbitrary speed value
	public bool facingRight = true;
	public float jumpForce = 150;			//Arbitrary jump value
	public bool enableControl = true;
	public GameObject currentPilot;
	
	//private Animator anim;
	
	//--------Input Variables--------//
	private float moveH = 0f;
	private float moveV = 0f;
	private bool jumpPress;					//When the Jump button is pressed
	private bool jumpRelease;				//When the Jump button is released
	private bool enterGolemPress;			//Input for enter or exiting the golem is pressed
	private bool enterGolemRelease;			//Input for enter or exiting the golem is released
	
	public bool grounded = false;			//checks if object on the ground
	public bool doubleJump = true;			//True = Doublejump is available
	private bool flyingMode = false;		//True = Flying is active
	private float flyingModeTimer = 0;		//The Timer for flying mode
	private float flyingModeDuration = 2;	//Total Duration of the flight
	
	private float exitTimer = -1;
	public float timeToExit = .5f;
	
	public Transform groundCheck;
	private int i;
	private Transform direction;
	
	void Start () {
		//anim = GetComponent<Animator>();

		//Find the child, GroundCheck, of the object and assign it as the ground check
		groundCheck = this.transform.FindChild("GroundCheck");
		direction = this.transform.FindChild("Direction");
	}
	
	void Update(){
		// Gets dynamic control scheme
		// TODO: don't grab controls every frame; grab them in control selection screen
		if(controls != null){
			moveH = Input.GetAxis (controls.horizontal);
			moveV = Input.GetAxis (controls.vertical);
			jumpPress = Input.GetButtonDown (controls.jump);
			jumpRelease = Input.GetButtonUp (controls.jump);
			enterGolemPress = Input.GetButton (controls.enter);
		}
		
		//enableControl is only used for potential ideas later. If true you have normal movement
		//if false, controls do nothing.
		if (enableControl) {
			
			//This checks the object "groundCheck", gives it a radius of "groundRadius"
			//If the "groundCheck" overlaps with anything that is tagged "whatIsGround"
			//the unit will be considered on the ground, grounded = true
			LayerMask whatIsGround = 1 << LayerMask.NameToLayer("Environment");
			float groundRadius = 0.1f;
			
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
			//anim.SetBool ("Ground", grounded);
			
			//When the vertical speed is not zero, change to the jumping/falling animation
			//anim.SetFloat ("speedV", rigidbody2D.velocity.y);
			
			//When the horizontal speed is not zero, change to the walking animation
			//anim.SetFloat ("speedH", Mathf.Abs (moveH));
			
			CheckJumping();
			CheckFlying();
			CheckExiting();
			DirectionCheck();
			
		} else {
			moveH = 0f; 
		}
		
	}
	
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
	
	//Exits the golem when held for .5 seconds
	private void CheckExiting(){
		//Find a nearby golem
		if(enterGolemPress){
			//Check if button is held long enough
			exitTimer = exitTimer + Time.deltaTime;
			if(exitTimer >= timeToExit){
				print ("exit");
				exitTimer = 0;
				ExitGolem();
			}
			
		}else{
			exitTimer = 0;
		}
	}
	
	private void ExitGolem(){
		// Reset pilot variables
		Pilot ps = currentPilot.GetComponent<Pilot>();
		ps.controls = controls;
		ps.enabled = true;
		ps.enableControl = true;
		ps.currentGolem = null;
		ps.facingRight = facingRight;
		currentPilot.collider2D.enabled = true;
		currentPilot.rigidbody2D.isKinematic = false;
		currentPilot.transform.parent = null;

		// Reset golem variables
		gameObject.AddComponent<Golem>();
		Destroy(this);
	}
	
	private void Flip () {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	private void DirectionCheck() {
		// Flip the image if it is moving to left while facing right or moving right while facing left
		if (moveH > 0 && !facingRight) {
			Flip ();
		} else if (moveH < 0 && facingRight) {
			Flip ();
		}
		
		//Top Right/Left
		if ((moveH >= 0 && moveV >= 0) || (moveH <= 0 && moveV >= 0)) {
			direction.localPosition = new Vector2(1, 1);
		}
		
		//Bottom Right/Left
		if ((moveH >= 0 && moveV <= 0) || (moveH <= 0 && moveV <= 0)) {
			direction.localPosition = new Vector2(1, -1);
		}
		
		//Top
		if (moveH == 0 && moveV >= 0) { 
			direction.localPosition = new Vector2(0, 1);
		}		
		
		//Bottom
		if (moveH == 0 && moveV <= 0) { 
			direction.localPosition = new Vector2(0, -1);
		}	
		
		//Straight Right/Left
		if ((moveH >= 0 && moveV == 0) || (moveH <= 0 && moveV == 0)) {
			direction.localPosition = new Vector2(1, 0);
		}
	}
}
