using UnityEngine;
using System.Collections;

public class BirdReaction : MonoBehaviour {

	public float speed = 1f;
	public bool isShoo = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {


		if ( (other.tag == "Player" || other.tag == "Golem")  && isShoo ) {
			speed =  Random.Range (2f,3f);
			rigidbody2D.velocity = new Vector2 (speed, speed);
			isShoo = false; 
		}
	}
}
