using UnityEngine;
using System.Collections;

public class Pilot : MonoBehaviour {

	private ControlScheme controls;

	public bool isP1 = true; 
	public float maxSpeed = 3f; 			//Arbitrary speed value
	public bool facingRight = true;
	
	public float jumpForce = 250;			//Arbitrary jump value
	public bool enableControl = true;
	public bool inGolem = false;
	private GolemEntry golemEntry;
	public bool exitingTheGolem = false;
	public GameObject golem;
	
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

	// Use this for initialization
	void Start () {
	
	}
	
	void Update(){
		moveH = Input.GetAxis (controls.horizontal);
		moveV = Input.GetAxis (controls.vertical);
		jumpPress = Input.GetButtonDown (controls.jump);
		jumpRelease = Input.GetButtonUp (controls.jump);
		enterGolemPress = Input.GetButtonDown (controls.enter);
		enterGolemRelease = Input.GetButtonUp (controls.enter);
	}
}
