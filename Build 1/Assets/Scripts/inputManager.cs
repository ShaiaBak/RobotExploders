using UnityEngine;
using System.Collections;

public class inputManager : MonoBehaviour {

	//temp variables
	public bool inputSwitch_p1;				// when input switch for player 1 is pressed
	public bool inputSwitch_p2;				// when input switch for player 2 is pressed
	public Controller1 p1;
	public Controller2 p2;

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
			Destroy(p2);
			// add p1 control scheme
			p1 = gameObject.AddComponent("Controller1") as Controller1;
		}
		if (inputSwitch_p2) {
			// when one control scheme is on, turn other off
			Destroy(p1);
			// add p2 control scheme
			p2 = gameObject.AddComponent("Controller2") as Controller2;

		}
	}
}
