using UnityEngine;
using System.Collections;

public class BirdReaction : MonoBehaviour {

	public float hitRadius = 0.5f;
	public float speedH =  2.5f;
	public float speedV = 2.0f;
	public bool isShoo = false;
	public bool isFlying = false;
	public float flyingTimerDuration = 2f;
	private float flyingTimer = 0f;
	private bool atPeak = false;
	private float counter = 0f;
	private float hoverScaleY = 1f;
	private float hoverScaleX = 1f;
	//public LayerMask scaresBirds;
	private Vector2 difference;
	private float atPeakX;
	
	void Start () {
		hoverScaleX = Random.Range(1.25f, 1.75f);
		hoverScaleY = Random.Range(0.01f, 0.02f);
		flyingTimer = flyingTimerDuration;
	}
	
	void Update () {

		if (isShoo && isFlying) {
			flyingTimer -= Time.deltaTime;
		}

		if (flyingTimer <= 0) {
			isFlying = false;
			rigidbody2D.velocity = new Vector2 (0f,0f);
			atPeak = true;
			atPeakX = transform.position.x;
			flyingTimer = flyingTimerDuration;
		}

		if (atPeak) {
			counter += Time.deltaTime/2;
//			Debug.Log(atPeakX);
			transform.position = new Vector2 ( atPeakX + Mathf.Sin(counter)*10f*hoverScaleX, transform.position.y + Mathf.Sin(counter)*hoverScaleY );
			//rigidbody2D.velocity = new Vector2 (0f, Mathf.Sin(Time.time)*hoverScale);
			//transform.localPosition.x += 1f;
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.collider2D.tag == "Player" || other.collider2D.tag == "Golem") {
		difference = other.transform.position - transform.position;
		if (difference.x < 0f && !isShoo) {
			speedH =  Random.Range (3.0f,3.5f);
			rigidbody2D.velocity = new Vector2 (speedH, speedV);
			isShoo = true; 	
			isFlying = true;
			flyingTimer = flyingTimerDuration;
		}

		if (difference.x > 0f && !isShoo) {
			speedH =  Random.Range (3.0f,3.5f);
			rigidbody2D.velocity = new Vector2 (-speedH, speedV);	
			isShoo = true; 
			isFlying = true;
			flyingTimer = flyingTimerDuration;
			}
		}
	}
}
