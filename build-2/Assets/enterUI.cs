using UnityEngine;
using System.Collections;

public class enterUI : MonoBehaviour {
	public Golem parentClass;
	public Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		parentClass = transform.parent.GetComponent<Golem>();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetFloat ("enterTime", parentClass.enterTimerFromPilot);
		// if ()
		// anim.SetFloat ("finishEnter", parentClass.enterGolemPress);
	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.collider2D.tag == "Player") {
			// Debug.Log("Entry UI enter");
			anim.SetBool ("Pilot Nearby", true);
		}
	}

	public void OnTriggerExit2D(Collider2D other) {
		if (other.collider2D.tag == "Player") {
			Debug.Log("Entry UI leave");
			anim.SetBool ("Pilot Nearby", false);
		} 
	}

}