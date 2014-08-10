using UnityEngine;
using System.Collections;
// This should be attached to the camera
public class GUIManager : MonoBehaviour {

	public PlayerHealthSystem ph;
	private Transform player;

	void Start(){
		// Grab the player defined in the Camera Controller component
		player = GetComponent<CameraController>().player;
		ph = player.GetComponent<PlayerHealthSystem>();
	}

	void OnGUI(){
		// TODO: We shouldn't check for player 1 by checking its name
		if(player.name == "P1"){
			GUI.Label(new Rect(10,10,100,20), ph.curHP.ToString() + "/" + ph.maxHP.ToString());
		}else{
			GUI.Label(new Rect(Screen.width/2+20,10,100,20), ph.curHP.ToString() + "/" + ph.maxHP.ToString());
		}
	}
}
