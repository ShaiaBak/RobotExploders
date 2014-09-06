﻿using UnityEngine;
using System.Collections;

public class ControlManager : MonoBehaviour {
	
	public ControlScheme P1Controls;
	public ControlScheme P2Controls;
	public GameObject P1;
	public GameObject P2;


	// Update is called once per frame
	void Update () {
		// Give Player1 P1 keyboard controls
		if(Input.GetButtonDown("p1_control1")){
			print ("Player1 now has P1 keyboard controls");
			Destroy(P1.GetComponent<ControlScheme>());
			// Set the controls
			KeyboardScheme ks = P1.AddComponent<KeyboardScheme>();
			ks.SetPlayerNumberControlScheme(1);
			P1Controls = ks;
			// Set the reference for pilot
			P1.GetComponent<Pilot>().controls = ks;
		}
		// Give Player1 P2 keyboard controls
		else if(Input.GetButtonDown("p1_control2")){
			print ("Player1 now has P2 keyboard controls");
			Destroy(P1.GetComponent<ControlScheme>());
			// Set the controls
			KeyboardScheme ks = P1.AddComponent<KeyboardScheme>();
			ks.SetPlayerNumberControlScheme(2);
			P1Controls = ks;
			// Set the reference for pilot
			P1.GetComponent<Pilot>().controls = ks;
		}
	}
}
