using UnityEngine;
using System.Collections;
// This should be attached to the camera
public class GUIManager : MonoBehaviour {

	public PlayerHealthSystem ph;
	public Controller1 pc;
	private Transform player;

	void Start(){
		// Grab the player defined in the Camera Controller component
		player = GetComponent<CameraController>().player;
		ph = player.GetComponent<PlayerHealthSystem>();
	}

	void OnGUI(){
		// TODO: We shouldn't check for player 1 by checking its name
		if(player.name == "P1"){

			// Show pilot hp
			GUI.Label(new Rect(10,10,100,20), ph.curHP.ToString() + "/" + ph.maxHP.ToString());

			// Check whether to show golem hp
			// TODO: We shouldn't check whether the player has controller1 in OnGUI
			pc = player.GetComponent<Controller1>();
			if(pc != null && pc.inGolem){
				GolemHealthSystem gh = pc.golem.GetComponent<GolemHealthSystem>();
				GUI.Label(new Rect(10,30,100,20), gh.curHP.ToString() + "/" + gh.maxHP.ToString());
			}

		}else{
			GUI.Label(new Rect(Screen.width/2+20,10,100,20), ph.curHP.ToString() + "/" + ph.maxHP.ToString());
		}
	}
}