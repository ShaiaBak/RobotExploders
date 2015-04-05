using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	//temp variables
	public bool inputSwitch_p1;				// when input switch for player 1 is pressed
	public bool inputSwitch_p2;				// when input switch for player 2 is pressed
	public Controller1 key1;
	public GolemController1 pad1;

	// Update is called once per frame
	void Update () {
		// when inputSwitch keys are pressed, switch the control scheme
		inputSwitch_p1 = Input.GetButtonDown("p1_control1");
		inputSwitch_p2 = Input.GetButtonDown("p1_control2");

		if (inputSwitch_p1) {
			// when one control scheme is on, turn other off
			Destroy(key1);
			Destroy(pad1);
			// add p1 control scheme
			key1 = gameObject.AddComponent<Controller1>() as Controller1;
			if (gameObject.name == "P2") {
				key1.isP1 = false;
			}
			enabled = false;
		}
		if (inputSwitch_p2) {
			// when one control scheme is on, turn other off
			Destroy(key1);
			Destroy(pad1);
			// add p2 control scheme
			pad1 = gameObject.AddComponent<GolemController1>() as GolemController1;
			enabled = false;
		}
	}
}
