using UnityEngine;
using System.Collections;
// This should be attached to the camera
public class GUIManager : MonoBehaviour {

	public PlayerHealthSystem ph;
	private Pilot ps;
	private Transform player;

	private float rotAngle = 0;
	public Texture2D icon;
	public Transform target;

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
			// Rotate sound notification
			Vector2 pivot = new Vector2(Screen.width/4,Screen.height/2);
			RotateSoundNotification(pivot);
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
			// Rotate sound notification
			Vector2 pivot = new Vector2(Screen.width-(Screen.width/4),Screen.height/2);
			RotateSoundNotification(pivot);
		}

	}

	// Rotate sound notification
	// GUIUtility.RotateAroundPivot rotates any GUI created after that line
	private void RotateSoundNotification(Vector2 pivot){
		GUIUtility.RotateAroundPivot(rotAngle , pivot);
		Vector3 cameraScreenPoint = ps.cameraScript.camera.WorldToScreenPoint(transform.position);
		GUI.DrawTexture(new Rect(cameraScreenPoint.x - icon.width/2,
		                         cameraScreenPoint.y - Screen.height/2,
		                         icon.width,icon.height),icon);
		// Get the direction from player to target
		// No idea why we can't use player's position - target's position
		Vector3 lookToTargetDirection = new Vector3(ps.transform.position.x,ps.transform.position.y,transform.position.z) - target.position;
		rotAngle = 180+Quaternion.LookRotation(lookToTargetDirection, Vector3.forward).eulerAngles.z;
	}
}