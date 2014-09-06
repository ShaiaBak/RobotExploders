using UnityEngine;
using System.Collections;

// TODO: Recode this completely when we have a controls selection screen
public class ControlManager : MonoBehaviour {
	
	public ControlScheme P1Controls;
	public ControlScheme P2Controls;
	public GameObject P1;
	public GameObject P2;


	// Smelly spaghetti code done by a cowboy! Pew pew! (Helper methods pls)
	void Update () {
		// Give Player1 P1 keyboard controls & vice versa
		if(Input.GetButtonDown("p1_control1")){
			print ("Player1 now has P1 keyboard controls");
			print ("Player2 now has P2 keyboard controls");
			Destroy(P1.GetComponent<ControlScheme>());
			Destroy(P2.GetComponent<ControlScheme>());
			// Set the controls
			KeyboardScheme ks1 = P1.AddComponent<KeyboardScheme>();
			KeyboardScheme ks2 = P2.AddComponent<KeyboardScheme>();
			ks1.SetPlayerNumberControlScheme(1);
			ks2.SetPlayerNumberControlScheme(2);
			P1Controls = ks1;
			P2Controls = ks2;
			// Set the reference for pilot
			P1.GetComponent<Pilot>().controls = ks1;
			P2.GetComponent<Pilot>().controls = ks2;
		}
		// Give Player1 P2 keyboard controls & vice versa
		else if(Input.GetButtonDown("p1_control2")){
			print ("Player1 now has P2 keyboard controls");
			print ("Player2 now has P1 keyboard controls");
			Destroy(P1.GetComponent<ControlScheme>());
			Destroy(P2.GetComponent<ControlScheme>());
			// Set the controls
			KeyboardScheme ks1 = P1.AddComponent<KeyboardScheme>();
			KeyboardScheme ks2 = P2.AddComponent<KeyboardScheme>();
			ks1.SetPlayerNumberControlScheme(2);
			ks2.SetPlayerNumberControlScheme(1);
			P1Controls = ks1;
			P2Controls = ks2;
			// Set the reference for pilot
			P1.GetComponent<Pilot>().controls = ks1;
			P2.GetComponent<Pilot>().controls = ks2;
		}
	}
}
