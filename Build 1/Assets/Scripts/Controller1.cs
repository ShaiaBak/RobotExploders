using UnityEngine;
using System.Collections;

public class Controller1 : MonoBehaviour {


	public float maxSpeed = 3f; 			//Arbitrary speed value
	private bool facingRight = true;


	public float jumpForce = 250;			//Arbitrary jump value
	public bool enableControl = true;
	//private Animator anim;

	//--------Input Variables--------//
	private float moveH = 0f;
	private float moveV = 0f;
	public bool jumpPress;					//When the Jump button is pressed
	public bool jumpRelease;				//When the Jump button is released

	public bool grounded = false;			//checks if object on the ground
	public bool doubleJump = true;			//True = Doublejump is available
	public bool flyingMode = false;			//True = Flying is active
	public float flyingModeTimer = 0;		//The Timer for flying mode
	public float flyingModeDuration = 2;	//Total Duration of the flight
	public Transform groundCheck;
	private float groundRadius = 0.1f;
	public LayerMask whatIsGround;
	private int i;

	void Start () {
		//anim = GetComponent<Animator>();
	}
	
	void FixedUpdate () {

	}
	void Update () {
		//Player Inputs
		moveH = Input.GetAxis ("P1_Horizontal");
		moveV = Input.GetAxis ("P1_Vertical");
		jumpPress = Input.GetButtonDown ("P1_Jump");
		jumpRelease = Input.GetButtonUp ("P1_Jump");

		if (jumpPress) {
			Debug.Log("jump is pressed");
		}

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

		//enableControl is only used for potential ideas later. If true you have normal movement
		//if false, controls do nothing.
		if (enableControl) {
			//This checks the object "groundCheck", gives it a radius of "groundRadius"
			//If the "groundCheck" overlaps with anything that is tagged "whatIsGround"
			//the unit will be considered on the ground, grounded = true
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
			//anim.SetBool ("Ground", grounded);
			
			//When player is on the ground, second jump is available
			if (grounded) {
				Debug.Log("you are grounded");
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
			//When the vertical speed is not zero, change to the jumping/falling animation
			//anim.SetFloat ("speedV", rigidbody2D.velocity.y);
			
			//When the horizontal speed is not zero, change to the walking animation
			//anim.SetFloat ("speedH", Mathf.Abs (moveH));

			// If the player has activated "Flying Mode", the player can move freely in the X and Y
			// If not, the player can only move in the X
			if (flyingMode){
				rigidbody2D.velocity = new Vector2 (moveH * maxSpeed, moveV * maxSpeed);
			} else {
				rigidbody2D.velocity = new Vector2 (moveH * maxSpeed, rigidbody2D.velocity.y);
			}
			// Flip the image if it is moving to left while facing right or moving right while facing left
			if (moveH > 0 && !facingRight) {
				Flip ();
			} else if (moveH < 0 && facingRight) {
				Flip ();
			}
			
		} else {
			moveH = 0f; 
		}
	}


	void Flip () {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
