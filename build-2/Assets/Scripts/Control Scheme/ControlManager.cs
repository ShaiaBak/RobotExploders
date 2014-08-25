using UnityEngine;
using System.Collections;

public class ControlManager : MonoBehaviour {
	
	public ControlScheme currentControls;
	
	// Update is called once per frame
	void Update () {
		// Give Player1 P1 keyboard controls
		if(Input.GetButtonDown("p1_control1")){
			Destroy(GetComponent<ControlScheme>());
			// Set the controls
			KeyboardScheme ks = gameObject.AddComponent<KeyboardScheme>();
			ks.SetPlayerNumberControlScheme(1);
			currentControls = ks;
			// Set the reference for pilot
			GetComponent<Pilot>().controls = ks;
		}
		// Give Player1 P2 keyboard controls
		else if(Input.GetButtonDown("p1_control2")){
			Destroy(GetComponent<ControlScheme>());
			// Set the controls
			KeyboardScheme ks = gameObject.AddComponent<KeyboardScheme>();
			ks.SetPlayerNumberControlScheme(2);
			currentControls = ks;
			// Set the reference for pilot
			GetComponent<Pilot>().controls = ks;
		}
	}
}
