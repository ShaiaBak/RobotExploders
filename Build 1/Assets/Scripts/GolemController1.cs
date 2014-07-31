using UnityEngine;
using System.Collections;

public class GolemController1 : MonoBehaviour {
	
	
	public float maxSpeed = 2f; 			//Arbitrary speed value
	public bool facingRight = true;
	
	
	public float jumpForce = 150;			//Arbitrary jump value
	public bool enableControl = false;
	private bool inGolem = false;
	private GolemEntry golemEntry;
	private Controller1 pilot;				//Change Controller1 when pilot script changes name
	//private Animator anim;
	
	//--------Input Variables--------//
	private float moveH = 0f;
	private float moveV = 0f;
	public bool jumpPress;					//When the Jump button is pressed
	public bool jumpRelease;				//When the Jump button is released
	public bool enterGolemPress;			//Input for enter or exiting the golem is pressed
	public bool enterGolemRelease;			//Input for enter or exiting the golem is released

	
	public bool grounded = false;			//checks if object on the ground
	public bool doubleJump = true;			//True = Doublejump is available
	private bool flyingMode = false;			//True = Flying is active
	private float flyingModeTimer = 0;		//The Timer for flying mode
	private float flyingModeDuration = 2;	//Total Duration of the flight

	public bool exitingTheGolem = false;
	public float exitingTimer = 0;
	public float exitingDuration = 0.5f;


	public Transform groundCheck;
	private float groundRadius = 0.1f;
	public LayerMask whatIsGround;
	private int i;
	private SpriteRenderer spriteRenderer;
	private BoxCollider2D boxCollider;
	private Transform golemPosition;		//Used for following the golem position
	private Transform direction;
	
	
	void Start () {
		//anim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		boxCollider = GetComponent<BoxCollider2D>();
		
		//Find the child, GroundCheck, of the object and assign it as the ground check
		groundCheck = this.transform.FindChild("GroundCheck");
		
		//used to check what value the environment layer is, which is 8
		//layermasks is a bitwise operator 8 = 2^8 = 256
		//Debug.Log(LayerMask.NameToLayer("Environment"));
		whatIsGround.value = 256;

		direction = this.transform.FindChild("Direction");

	}
	
	void FixedUpdate () {
		
	}
	
	void Update () {
		//Player Inputs
		moveH = Input.GetAxis ("P1_Horizontal");
		moveV = Input.GetAxis ("P1_Vertical");
		jumpPress = Input.GetButtonDown ("P1_Jump");
		jumpRelease = Input.GetButtonUp ("P1_Jump");
		enterGolemPress = Input.GetButtonDown ("P1_Enter");
		enterGolemRelease = Input.GetButtonUp ("P1_Enter");

		if (jumpPress) {
			Debug.Log("jump is pressed");
		}
		
		//enableControl is only used for potential ideas later. If true you have normal movement
		//if false, controls do nothing.
		if (enableControl) {
			
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
			// When the enter/exit golem button is pressed, start the timer
			if (enterGolemPress) {
				exitingTheGolem = true;
				exitingTimer = exitingDuration;
			}
			// If enter/exit golem button cancel timer
			if (enterGolemRelease) {
				exitingTheGolem = false;
			}
			
			if (exitingTheGolem) {
				exitingTimer -= Time.deltaTime;
			}

			// when the timer reaches 0, remove the pilot 
			if (exitingTimer <= 0 && exitingTheGolem) {
	

				//Search for the pilot sibling, uses Tag
				foreach (Transform child in transform.parent) {
					if (child.tag == "Player") {
						pilot = child.GetComponent<Controller1>();
					}
				}
				flyingMode = false;
				rigidbody2D.gravityScale = 1;
				enableControl = false;
				exitingTheGolem = false;
				golemEntry = transform.parent.Find ("Golem_Entry").GetComponent<GolemEntry>();
				golemEntry.pilotInGolem = false;
				pilot.exitingTheGolem = true;
				moveH = 0f;
				moveV = 0f;	
			}

		} else {
			moveH = 0f; 
		}
		//Reset Exiting golem timer
		if (!exitingTheGolem) {
			exitingTimer = exitingDuration;
		}



	}

	void Flip () {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


}
