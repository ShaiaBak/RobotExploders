using UnityEngine;
using System.Collections;

public class BirdReaction : MonoBehaviour {

	public float hitRadius = 0.5f;
	//private RaycastHit2D hitInformationLeft;
	//private RaycastHit2D hitInformationAngledLeft;
	//private RaycastHit2D hitInformationRight;
	//private RaycastHit2D hitInformationAngledRight;
	public float speedH =  2.5f;
	public float speedV = 1.75f;
	public bool isShoo = false;
	public bool isFlying = false;
	public float flyingTimer = 2f;
	public bool atPeak = false;
	public float hoverScale = 1f;
	//public LayerMask scaresBirds;
	public Vector2 difference; 
	
	void Start () {

		//Debug.Log(LayerMask.NameToLayer("Pilots"));
		//Debug.Log(LayerMask.NameToLayer("Golems"));

		//Layer mask, only Pilots and Golems scare the birds
		//scaresBirds.value = (int)(Mathf.Pow(2f,(float)LayerMask.NameToLayer("Pilots")) + Mathf.Pow(2f,(float)LayerMask.NameToLayer("Golems")));
		//Debug.Log (scaresBirds.value);
		hoverScale = Random.Range(0.01f, 0.020f);

	}
	
	void Update () {

		if (isShoo && isFlying) {
			flyingTimer -= Time.deltaTime;
		}

		if (flyingTimer <= 0) {
			isFlying = false;
			rigidbody2D.velocity = new Vector2 (0f,0f);
			atPeak = true;

		}

		if (atPeak) {
			
			transform.position = new Vector2 ( transform.position.x + Mathf.Sin(Time.time)*0.25f, transform.position.y + Mathf.Sin(Time.time)*hoverScale );
			//	rigidbody2D.velocity = new Vector2 (0f, Mathf.Sin(Time.time)*hoverScale);
		//	transform.localPosition.x += 1f;
		}

//		//Debug.DrawRay(test.position,-Vector2.right, Color.green);
//		//Debug.DrawRay(transform.position, -Vector2.right, Color.red);
//
//		Vector2 angledLeft = new Vector2(transform.position.x - hitRadius + 0.2f ,transform.position.y+0.5f);
//		Vector2 angledRight = new Vector2(transform.position.x + hitRadius - 0.2f ,transform.position.y+0.5f);
//
//		//Debug Lines for Left side
//		Debug.DrawLine(transform.position, angledLeft, Color.green);
//		Debug.DrawLine(transform.position, new Vector2 (transform.position.x - hitRadius, transform.position.y),Color.green);
//
//		//Debug Lines for Right side
//		Debug.DrawLine(transform.position, angledRight, Color.red);
//		Debug.DrawLine(transform.position, new Vector2 (transform.position.x + hitRadius, transform.position.y),Color.red);
//
//		Physics2D.Raycast(
//		hitInformationLeft = Physics2D.Raycast(transform.position, -Vector2.right, hitRadius, scaresBirds);
//		hitInformationAngledLeft = Physics2D.Raycast(transform.position, test , hitRadius, scaresBirds);
//
//		hitInformationRight = Physics2D.Raycast(transform.position, Vector2.right, hitRadius, scaresBirds);
//		hitInformationAngledRight = Physics2D.Raycast(transform.position, angledRight, hitRadius, scaresBirds);
//
//		if ( (hitInformationLeft || hitInformationAngledLeft) && !isShoo) {
//			//Debug.Log (hitInformationLeft.collider.gameObject.name);
//			//if (hitInformationLeft.collider.tag == "Player" || hitInformationLeft.collider.tag == "Golem" ) { 
//				speedH =  Random.Range (2.5f,3.5f);
//				rigidbody2D.velocity = new Vector2 (speedH, speedV);
//				isShoo = true; 
//			//}
//		}
//
//		if ( (hitInformationRight || hitInformationAngledRight) && !isShoo) {
//			//Debug.Log (hitInformationRight.collider.gameObject.name);
//			//if (hitInformationRight.collider.tag == "Player" || hitInformationRight.collider.tag == "Golem") { 
//				speedH =  Random.Range (2.5f,3.5f);
//				rigidbody2D.velocity = new Vector2 (-speedH, speedV);
//				isShoo = true; 
//			//}
//		}
	}
	void OnTriggerEnter2D (Collider2D other) {
		if (other.collider2D.tag == "Player" || other.collider2D.tag == "Golem") {
		difference = other.transform.position - transform.position;
		if (difference.x < 0f && !isShoo) {
			speedH =  Random.Range (2.5f,3.5f);
			rigidbody2D.velocity = new Vector2 (speedH, speedV);
			isShoo = true; 	
			isFlying = true;
		}

		if (difference.x > 0f && !isShoo) {
			speedH =  Random.Range (2.5f,3.5f);
			rigidbody2D.velocity = new Vector2 (-speedH, speedV);	
			isShoo = true; 
			isFlying = true; 
			}
		}
	}
}
