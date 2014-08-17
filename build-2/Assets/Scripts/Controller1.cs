using UnityEngine;
using System.Collections;

public class Controller1 : MonoBehaviour {

	public bool isP1 = true; 
	public float maxSpeed = 3f; 			//Arbitrary speed value
	public bool facingRight = true;

	public float jumpForce = 250;			//Arbitrary jump value
	public bool enableControl = true;
	private bool inGolem = false;
	private GolemEntry golemEntry;
	public bool exitingTheGolem = false;

	//private Animator anim;

	//--------Input Variables--------//
	public float moveH = 0f;
	public float moveV = 0f;
	public bool jumpPress;					//When the Jump button is pressed
	public bool jumpRelease;				//When the Jump button is released
	public bool enterGolemPress;			//Input for enter or exiting the golem is pressed
	public bool enterGolemRelease;			//Input for enter or exiting the golem is released

	public bool grounded = false;			//checks if object on the ground
	public bool doubleJump = true;			//True = Doublejump is available
	private bool flyingMode = false;		//True = Flying is active
	private float flyingModeTimer = 0;		//The Timer for flying mode
	private float flyingModeDuration = 2;	//Total Duration of the flight

	public bool enteringTheGolem = false;
	public float enteringTimer = 0;
	public float enteringDuration = 0.5f;

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
		if (isP1) {
			moveH = Input.GetAxis ("P1_Horizontal");
			moveV = Input.GetAxis ("P1_Vertical");
			jumpPress = Input.GetButtonDown ("P1_Jump");
			jumpRelease = Input.GetButtonUp ("P1_Jump");
			enterGolemPress = Input.GetButtonDown ("P1_Enter");
			enterGolemRelease = Input.GetButtonUp ("P1_Enter");
		}

		if (!isP1) {
			moveH = Input.GetAxis ("P2_Horizontal");
			moveV = Input.GetAxis ("P2_Vertical");
			jumpPress = Input.GetButtonDown ("P2_Jump");
			jumpRelease = Input.GetButtonUp ("P2_Jump");
			enterGolemPress = Input.GetButtonDown ("P2_Enter");
			enterGolemRelease = Input.GetButtonUp ("P2_Enter");
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

			directionCheck();

			if (enteringTheGolem) {
				enteringTimer -= Time.deltaTime;
			}
			if (!enteringTheGolem) {
				enteringTimer = enteringDuration;
			}



		} else {
			moveH = 0f; 
		}

		if(inGolem) {
			//Follow the golem
			transform.position = new Vector2(golemPosition.position.x, golemPosition.position.y);
		}

		if(exitingTheGolem) {
			pilotExit();
		}



	}

	//When the pilot enters the golems entry area
	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "GolemEnterZone" && enableControl) {

			Debug.Log("In Golem");
			if (enterGolemPress) {
				enteringTheGolem = true;
				enteringTimer = enteringDuration;
			}
			if (enterGolemRelease) {
				enteringTheGolem = false;
			}	
				
			if (enteringTimer <= 0 && enteringTheGolem) {
				//Disable player control and disable the image and collider
				Debug.Log("Entering....");
				enableControl = false;
				moveH = 0f; 
				moveV = 0f; 
				rigidbody2D.gravityScale = 0;
				spriteRenderer.enabled = false;
				boxCollider.enabled = false;
				flyingMode = false;
				//Finds the golems entry component and tags the pilot as inside the golem
				//and then sets the pilot to be a child of the Golem
				golemEntry = other.GetComponent<GolemEntry>();
				golemEntry.pilotInGolem = true;

				//If P1, set golems controls to Player 1
				if (isP1) {
					golemEntry.isP1entering = true;
				}
				if (!isP1) {
					golemEntry.isP1entering = false;
				}
				
				//Pilot object becomes a child of the golem and follows the golem object

				this.transform.parent = golemEntry.transform.parent;
				golemPosition = transform.parent.Find ("Golem");
				inGolem = true;
			} 
		} else {
			enteringTheGolem = false;
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "GolemEnterZone") {
			enteringTheGolem = false;
			enteringTimer = enteringDuration;
		}
	}

	void pilotExit() {
		Debug.Log("Exiting....");
		enableControl = true;
		moveH = 0f; 
		moveV = 0f; 
		rigidbody2D.gravityScale = 1;
		spriteRenderer.enabled = true;
		boxCollider.enabled = true;
		this.transform.parent = null;
		inGolem = false;
		exitingTheGolem = false;
		enteringTheGolem = false;
	}

	void Flip () {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	void directionCheck() {
		
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
