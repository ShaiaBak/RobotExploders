using UnityEngine;
using System.Collections;

// TODO: Recode this completely when we have a controls selection screen
// Change a player's control scheme by adding the corresponding component
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
			Pilot ps1 = P1.GetComponent<Pilot>();
			ps1.controls = ks1;
			Pilot ps2 = P2.GetComponent<Pilot>();
			ps2.controls = ks2;
			// Set the reference for golem
			if(ps1.currentGolem != null){
				ps1.currentGolem.GetComponent<Golem>().controls = ks1;
			}
			if(ps2.currentGolem != null){
				ps2.currentGolem.GetComponent<Golem>().controls = ks2;
			}
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
			Pilot ps1 = P1.GetComponent<Pilot>();
			ps1.controls = ks1;
			Pilot ps2 = P2.GetComponent<Pilot>();
			ps2.controls = ks2;
			// Set the reference for golem
			if(ps1.currentGolem != null){
				ps1.currentGolem.GetComponent<Golem>().controls = ks1;
			}
			if(ps2.currentGolem != null){
				ps2.currentGolem.GetComponent<Golem>().controls = ks2;
			}
		}
		else if(Input.GetButtonDown("p1_joy1")){
			print ("Player1 now has P1 Joypad controls");
			print ("Player2 now has P2 Joypad controls");
			Destroy(P1.GetComponent<ControlScheme>());
			Destroy(P2.GetComponent<ControlScheme>());
			// Set the controls
			JoypadScheme js1 = P1.AddComponent<JoypadScheme>();
			JoypadScheme js2 = P2.AddComponent<JoypadScheme>();
			js1.SetPlayerNumberControlScheme(1);
			js2.SetPlayerNumberControlScheme(2);
			P1Controls = js1;
			P2Controls = js2;
			// Set the reference for pilot
			Pilot ps1 = P1.GetComponent<Pilot>();
			ps1.controls = js1;
			Pilot ps2 = P2.GetComponent<Pilot>();
			ps2.controls = js2;
			// Set the reference for golem
			if(ps1.currentGolem != null){
				ps1.currentGolem.GetComponent<Golem>().controls = js1;
			}
			if(ps2.currentGolem != null){
				ps2.currentGolem.GetComponent<Golem>().controls = js2;
			}
		}
	}
}
