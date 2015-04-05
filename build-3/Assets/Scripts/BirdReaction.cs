using UnityEngine;
using System.Collections;

public class BirdReaction : MonoBehaviour {

	public float hitRadius = 0.5f;
	public float speedH =  2.5f;
	public float speedV = 2.0f;
	public bool isShoo = false;
	public bool isFlying = false;
	public float flyingTimer1Duration = 2f;
	private float flyingTimer1 = 0f;
	public float flyingTimer2Duration = 5f;
	private float flyingTimer2 = 0f;
	private bool atPeak = false;
	private float counter = 0f;
	public float hoverScaleY = 1f;
	public float hoverScaleX = 1f;
	//public LayerMask scaresBirds;
	private Vector2 difference;
	private float atPeakX;
	private float atPeakY;

	public bool isLanding;
	public float landingScaleY = 0.01f;

	void Start () {
		init ();

	}
	
	void Update () {

		if (isShoo && isFlying) {
			flyingTimer1 -= Time.deltaTime;
		}

		if (flyingTimer1 <= 0) {
			isFlying = false;
			GetComponent<Rigidbody2D>().velocity = new Vector2 (0f,0f);
			atPeak = true;
			atPeakX = transform.position.x;
			atPeakY = transform.position.y;
			flyingTimer1 = flyingTimer1Duration;
		}

		if (atPeak) {
			counter += Time.deltaTime/2;
			transform.position = new Vector2 ( atPeakX + Mathf.Sin(counter)*hoverScaleX, atPeakY + Mathf.Sin(counter*hoverScaleY*3)*0.75f );
			flyingTimer2 -= Time.deltaTime;
		}

		if (flyingTimer2 <= 0) {
			atPeak = false;
			isLanding = true;
			transform.position = new Vector2 (transform.position.x, transform.position.y - landingScaleY);
		}

	}

	private void init() {
		hoverScaleX = Random.Range(7.5f, 10.0f);
		hoverScaleY = Random.Range(1f, 1.5f);
		flyingTimer1 = flyingTimer1Duration;
		flyingTimer2 = flyingTimer2Duration;
		isShoo = false;
		isFlying = false;
		atPeak = false;
		counter = 0f;
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.GetComponent<Collider2D>().tag == "Player" || other.GetComponent<Collider2D>().tag == "Golem") {
		difference = other.transform.position - transform.position;
		if (difference.x < 0f && !isShoo) {
			speedH =  Random.Range (3.0f,3.5f);
			GetComponent<Rigidbody2D>().velocity = new Vector2 (speedH, speedV);
			isShoo = true; 	
			isFlying = true;
			flyingTimer1 = flyingTimer1Duration;
		}

		if (difference.x > 0f && !isShoo) {
			speedH =  Random.Range (3.0f,3.5f);
			GetComponent<Rigidbody2D>().velocity = new Vector2 (-speedH, speedV);	
			isShoo = true; 
			isFlying = true;
			flyingTimer1 = flyingTimer1Duration;
			}
		}
	}
}
