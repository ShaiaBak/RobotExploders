using UnityEngine;
using System.Collections;

public class BirdReaction : MonoBehaviour {

	public float hitRadius = 0.5f;
	public float speedH =  2.5f;
	public float speedV = 1.75f;
	public bool isShoo = false;
	public bool isFlying = false;
	public float flyingTimerDuration = 2f;
	private float flyingTimer = 0f;
	public bool atPeak = false;
	public float hoverScale = 1f;
	//public LayerMask scaresBirds;
	public Vector2 difference;
	public float atPeakX;
	
	void Start () {
	
		hoverScale = Random.Range(0.01f, 0.020f);
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
//			Debug.Log(atPeakX);
			transform.position = new Vector2 ( atPeakX + Mathf.Sin(Time.time) , transform.position.y + Mathf.Sin(Time.time)*hoverScale );
			//rigidbody2D.velocity = new Vector2 (0f, Mathf.Sin(Time.time)*hoverScale);
			//transform.localPosition.x += 1f;
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.collider2D.tag == "Player" || other.collider2D.tag == "Golem") {
		difference = other.transform.position - transform.position;
		if (difference.x < 0f && !isShoo) {
			speedH =  Random.Range (2.5f,3.5f);
			rigidbody2D.velocity = new Vector2 (speedH, speedV);
			isShoo = true; 	
			isFlying = true;
			flyingTimer = flyingTimerDuration;
		}

		if (difference.x > 0f && !isShoo) {
			speedH =  Random.Range (2.5f,3.5f);
			rigidbody2D.velocity = new Vector2 (-speedH, speedV);	
			isShoo = true; 
			isFlying = true;
			flyingTimer = flyingTimerDuration;
			}
		}
	}
}
