using UnityEngine;
using System.Collections;
// This should be attached to the camera
public class GUIManager : MonoBehaviour {

	public PlayerHealthSystem ph;
	private Pilot ps;
	private Transform player;

//	private float rotAngle = 0;
//	public Texture2D[] icons;
//	// Test:
//	public Transform target1;
//	public Transform target2;

	void Start(){
		// Grab the player defined in the Camera Controller component
		player = GetComponent<CameraController>().player;
		ph = player.GetComponent<PlayerHealthSystem>();
		ps = player.GetComponent<Pilot>();
	}

	void OnGUI(){
		// Player 1
		if(ps.isP1){
			// Show pilot hp
			GUI.Label(new Rect(10,10,100,20), ph.curHP.ToString() + "/" + ph.maxHP.ToString());

			// Check whether to show golem hp
			if(ps.currentGolem != null){
				GolemHealthSystem gh = ps.currentGolem.GetComponent<GolemHealthSystem>();
				GUI.Label(new Rect(10,30,100,20), gh.curHP.ToString() + "/" + gh.maxHP.ToString());
			}
		}
		// Player 2
		else{
			// Show pilot hp
			GUI.Label(new Rect(Screen.width/2+20,10,100,20), ph.curHP.ToString() + "/" + ph.maxHP.ToString());

			// Check whether to show golem hp
			if(ps.currentGolem != null){
				GolemHealthSystem gh = ps.currentGolem.GetComponent<GolemHealthSystem>();
				GUI.Label(new Rect(Screen.width/2+20,30,100,20), gh.curHP.ToString() + "/" + gh.maxHP.ToString());
			}
		}

	}
}