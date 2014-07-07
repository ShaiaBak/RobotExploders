using UnityEngine;
using System.Collections;

public class inputManager : MonoBehaviour {

	//temp variables
	public bool inputSwitch_p1;				// when input swithc for player 1 is pressed
	public bool inputSwitch_p2;				// when input swithc for player 2 is pressed
	public bool control1_p1 = true;			// player 1 has control scheme 1
	public bool control2_p1 = false;		// player 1 has control scheme 2

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// when inputSwitch keys are pressed, switch the control scheme
		inputSwitch_p1 = Input.GetButtonDown("p1_control1");
		inputSwitch_p2 = Input.GetButtonDown("p1_control2");

		if (inputSwitch_p1) {
			// when one control scheme is on, turn other off
			control1_p1 = false;
		}
		if (inputSwitch_p2) {
			// when one control scheme is on, turn other off
			control2_p1 = false;
		}
	}
}
