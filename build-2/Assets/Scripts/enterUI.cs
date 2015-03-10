using UnityEngine;
using System.Collections;

public class enterUI : MonoBehaviour {
	public Golem parentClass;
	public Animator anim;
	public float enterTimeFromPilot;
	private Pilot pilot;
	public bool pilotNearby = false;
	public bool pilotInside = false;
	// Use this for initialization

	void Start () {
		anim = GetComponent<Animator>();
		parentClass = transform.parent.GetComponent<Golem>();
	}
	
	// Update is called once per frame
	void Update () {
		// anim.SetFloat ("enterTime", parentClass.enterTimerFromPilot);
		
		// if ()
		// anim.SetFloat ("finishEnter", parentClass.enterGolemPress);
		// if (parentClass.enterGolemPress) {
			
		// } else {
		// 	parentClass.exitTimer = Mathf.Lerp(parentClass.exitTimer, 0, 0.15f);
		// }


		if (pilotNearby) {
			enterTimeFromPilot = pilot.enterTimer;
		} else {
			
			enterTimeFromPilot = Mathf.Lerp(enterTimeFromPilot, 0, 0.15f);
			// enterTimeFromPilot = 0;
		}

		anim.SetFloat ("exitTime", parentClass.exitTimer);
		anim.SetFloat ("enterTime", enterTimeFromPilot);
	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.collider2D.tag == "Player") {
			// Debug.Log("Entry UI enter");
			pilotNearby = true;
			pilot = other.collider2D.gameObject.GetComponent<Pilot>();
			// Debug.Log(enterTimeFromPilot);
			//

			anim.SetBool ("Pilot Nearby", pilotNearby);
		}
	}

	public void OnTriggerExit2D(Collider2D other) {
		if (other.collider2D.tag == "Player") {
			pilotNearby = false;
			// Debug.Log("Entry UI leave");
			anim.SetBool ("Pilot Nearby", pilotNearby);
		} 
	}


	//Changes the orientation of the the UI to differentiate it from entering and exiting
	public void flipVertical() {
		Vector2 theScale = transform.localScale;
		Vector2 thePosition = transform.localPosition;
		theScale.y *= -1;
		thePosition.y = -1*thePosition.y + 1.5f;
		transform.localScale = theScale;
		transform.localPosition = thePosition;
	}

}