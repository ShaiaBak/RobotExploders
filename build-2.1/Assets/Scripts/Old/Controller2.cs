using UnityEngine;
using System.Collections;

public class Controller2 : MonoBehaviour {


	public float maxSpeed = 3f; 	//Arbitrary speed value
	private bool facingRight = true;


	public float jumpForce = 250;	//Arbitrary jump value
	public bool enableControl = true;
	//private Animator anim;
	private float moveH = 0f;
//	private float moveV = 0f;
	public bool grounded = false;
	public bool jump;
	public bool doubleJump = true;
	public Transform groundCheck;
	private float groundRadius = 0.1f;
	public LayerMask whatIsGround;


	void Start () {
		//anim = GetComponent<Animator>();
	}
	
	void FixedUpdate () {
		//Only allow jumping when player is on the ground

		if (enableControl) {
			//This checks the object "groundCheck", gives it a radius of "groundRadius"
			//If the "groundCheck" overlaps with anything that is tagged "whatIsGround"
			//the unit will be considered on the ground, grounded = true
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
			//anim.SetBool ("Ground", grounded);

			if (grounded) {
				doubleJump = true;
				if (jump) {
					GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, jumpForce));
				}
				
			} else if (doubleJump && jump) {
				doubleJump = false;
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, jumpForce));
			}

			//When the vertical speed is not zero, change to the jumping/falling animation
			//anim.SetFloat ("speedV", rigidbody2D.velocity.y);

			//When the horizontal speed is not zero, change to the walking animation
			//anim.SetFloat ("speedH", Mathf.Abs (moveH));
			//Handles horizontal speed and orientation
			GetComponent<Rigidbody2D>().velocity = new Vector2 (moveH * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
			if (moveH > 0 && !facingRight) {
					Flip ();
			} else if (moveH < 0 && facingRight) {
					Flip ();
			}
		} else {
			moveH = 0f; 
		}


	}
	void Update () {
		//Player Inputs
		moveH = Input.GetAxis ("P2_Horizontal");
//		moveV = Input.GetAxis ("P2_Vertical");
		jump = Input.GetButtonDown ("P2_Jump");



	}


	void Flip () {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
