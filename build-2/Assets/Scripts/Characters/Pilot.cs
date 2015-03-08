using UnityEngine;
using System.Collections;

public class Pilot : MonoBehaviour {

	public ControlScheme controls;

	public bool isP1 = true; 
	public float maxSpeed = 3f; 			//Arbitrary speed value
	public bool facingRight = true;
	public float jumpForce = 350f;			//Arbitrary jump value
	public bool enableControl = true;
	public GameObject currentGolem;
	public CameraController cameraScript;	// The CameraControl script attached to the camera that's following the pilot. It's set in CameraController
	
	//private Animator anim;
	
	//--------Input Variables--------//
	private float moveH = 0f;
	private float moveV = 0f;
	private bool jumpPress;					//When the Jump button is pressed
	private bool jumpRelease;				//When the Jump button is released
	private bool jumpHeld;					//When the Jump button is held
	private bool enterGolemPress;			//Input for enter or exiting the golem is pressed
	private bool enterGolemRelease;			//Input for enter or exiting the golem is released
	
	public bool grounded = false;			//checks if object on the ground
	public bool doubleJump = true;			//True = Doublejump is available
	private bool flyingMode = false;		//True = Flying is active
	private float flyingModeTimer = 0;		//The Timer for flying mode
	private float flyingModeDuration = 2;	//Total Duration of the flight

	private float enterTimer = 0;
	public float timeToEnter = .5f;

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
			jumpHeld = Input.GetButton (controls.jump);
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
			CheckEntering();
			DirectionCheck();
			rigidbody2D.velocity = new Vector2 (moveH * maxSpeed, rigidbody2D.velocity.y);
		} else {
			moveH = 0f; 
		}

	}

	// START OF VERSION 2
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
			rigidbody2D.AddForce (new Vector2 (0, jumpForce/20f));
		}
		//When timer is equal to or less than zero or when the jump button is released
		//Disable flying mode and restore the gravity setting
		if (flyingModeTimer <= 0 || grounded) {
			flyingMode = false;
		}
	}

	//Checks whether the pilot is entering a golem and enter it if the button is held for .5 seconds
	public void CheckEntering(){
		//Find a nearby golem
		if(enterGolemPress){
			Collider2D nearbyGolem = Physics2D.OverlapCircle(transform.position, .25f, 1 << LayerMask.NameToLayer("Deactivated"));
			
			if(nearbyGolem != null && nearbyGolem.GetComponent<Golem>().currentPilot == null){
				//Check if button is held long enough
				enterTimer = enterTimer + Time.deltaTime;

				nearbyGolem.GetComponent<Golem>().enterTimerFromPilot = enterTimer;
				if(enterTimer >= timeToEnter){
					//print ("enter");
					enterTimer = 0;
					nearbyGolem.GetComponent<Golem>().enterTimerFromPilot = enterTimer;
					EnterGolem(nearbyGolem.gameObject);
				}
			}
		} else if (enterTimer >= 0.01f) {

			Collider2D nearbyGolem = Physics2D.OverlapCircle(transform.position, .25f, 1 << LayerMask.NameToLayer("Deactivated"));
			// enterTimer = 0;
			enterTimer = Mathf.Lerp(enterTimer, 0, Time.time);
			nearbyGolem.GetComponent<Golem>().enterTimerFromPilot = enterTimer;
		}
	}
	
	// Enters the golem and transfers control scheme to it
	private void EnterGolem(GameObject golem){
		currentGolem = golem;
		golem.layer = LayerMask.NameToLayer("Golems");
		// Sets golem variables to match pilot
		Golem gs = golem.GetComponent<Golem>();
		gs.controls = controls;
		gs.currentPilot = gameObject;
		transform.position = golem.transform.position;
		transform.parent = golem.transform;
//		currentGolem.rigidbody2D.isKinematic = false;
		// Sets pilot facing to golem's facing
		if(facingRight != gs.facingRight){
			Flip();

		}

		//Disable the pilot
		enabled = false;
		enableControl = false;
		moveH = 0f; 
		moveV = 0f; 
		rigidbody2D.isKinematic = true;
		flyingMode = false;
		collider2D.enabled = false;
//		GetComponent<TempShootingScript>().enabled = false;
	}
	
	private void Flip () {
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
		if(transform.localScale.x > 0){
			facingRight = true;
		}else{
			facingRight = false;
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
