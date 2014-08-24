using UnityEngine;
using System.Collections;

public class BirdReaction : MonoBehaviour {

	public float hitRadius = 0.5f;
	private RaycastHit2D hitInformationLeft;
	private RaycastHit2D hitInformationRight;
	private Transform test;
	public float speedH = 2.5f;
	public float speedV = 1.75f;
	public bool isShoo = false;
	public LayerMask scaresBirds;

	// Use this for initialization
	void Start () {

		//Debug.Log(LayerMask.NameToLayer("Pilots"));
		//Debug.Log(LayerMask.NameToLayer("Golems"));
		scaresBirds.value = (int)(Mathf.Pow(2f,(float)LayerMask.NameToLayer("Pilots")) + Mathf.Pow(2f,(float)LayerMask.NameToLayer("Golems")));
		Debug.Log (scaresBirds.value);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.DrawRay(test.position,-Vector2.right, Color.green);
		//Debug.DrawRay(transform.position, -Vector2.right, Color.red);

		Vector2 dir = new Vector2(transform.position.x - hitRadius + 0.2f ,transform.position.y+0.5f);


		Debug.DrawLine(transform.position, dir,Color.red);
		Debug.DrawLine(transform.position, new Vector2 (transform.position.x - hitRadius, transform.position.y),Color.red);
		Debug.DrawLine(transform.position, new Vector2 (transform.position.x + hitRadius, transform.position.y),Color.red);
		hitInformationLeft = Physics2D.Raycast(transform.position, -Vector2.right, hitRadius, scaresBirds);
		hitInformationRight = Physics2D.Raycast(transform.position, Vector2.right, hitRadius, scaresBirds);

		if (hitInformationLeft && !isShoo) {
			Debug.Log (hitInformationLeft.collider.gameObject.name);
			if (hitInformationLeft.collider.tag == "Player" || hitInformationLeft.collider.tag == "Golem") { 
				speedH =  Random.Range (2.5f,3.5f);
				rigidbody2D.velocity = new Vector2 (speedH, speedV);
				isShoo = true; 
			}
		}

		if (hitInformationRight && !isShoo) {
			Debug.Log (hitInformationRight.collider.gameObject.name);
			if (hitInformationRight.collider.tag == "Player" || hitInformationRight.collider.tag == "Golem") { 
				speedH =  Random.Range (2.5f,3.5f);
				rigidbody2D.velocity = new Vector2 (-speedH, speedV);
				isShoo = true; 
			}
		}


	}

	void OnTriggerEnter2D (Collider2D other) {




//		if ( (other.tag == "Player" || other.tag == "Golem")  && isShoo ) {
//			speed =  Random.Range (2f,3f);
//			rigidbody2D.velocity = new Vector2 (speed, speed);
//			isShoo = false; 
//		}
	}
}
