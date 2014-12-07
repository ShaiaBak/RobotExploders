using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundNotificationController : MonoBehaviour {

	private Pilot ps;
	
	private float rotAngle = 0;
	public Texture2D[] icons;
	private static List<Sound> sounds;
	// Test:
	public Transform target1;
	public Transform target2;
	public float timer = 0;

	public class Sound{
		public Vector2 position;
		public int magnitude;
		public float duration;

		public Sound(Vector2 pos, int mag, float dur){
			position = pos;
			magnitude = mag;
			duration = dur + Time.time;
//			Destroy(this,dur);
		}
	}

	void Start(){
		// Grab the Pilot script defined in the Camera Controller component
		ps = GetComponent<CameraController>().player.GetComponent<Pilot>();
		sounds = new List<Sound>();
	}

	void Update(){
		timer += Time.deltaTime;
		for(int i=0; i<sounds.Count; i++){
			if(Time.time >= sounds[i].duration){
				sounds.RemoveAt(i);
			}
		}
	}
	
	void OnGUI(){
//		GUI.Label(new Rect(0,0,1000,1000), Time.time.ToString());
//		for(int i=0; i<sounds.Count; i++){
//			GUI.Label(new Rect(0,Screen.height-20*(i+1),1000,1000), sounds[i].duration.ToString());
//		}
		// Player 1
		if(ps.isP1){
			// Rotate sound notification
			Vector2 pivot = new Vector2(Screen.width/4,Screen.height/2);
			for(int i=0; i<sounds.Count; i++){
				RotateSoundNotification(pivot,sounds[i].position,icons[3]);
			}
		}
		// Player 2
		else{
			// Rotate sound notification
			Vector2 pivot = new Vector2(Screen.width-(Screen.width/4),Screen.height/2);
			for(int i=0; i<sounds.Count; i++){
				RotateSoundNotification(pivot,sounds[i].position,icons[3]);
			}
		}
		
	}

	/// <summary>
	/// Creates the sound.
	/// </summary>
	/// <param name="position">Position.</param>
	/// <param name="magnitude">Magnitude.</param>
	/// <param name="duration">Duration.</param>
	public static void CreateSound(Vector2 position, int magnitude, float duration){
		sounds.Add(new Sound(position,magnitude,duration));
	}

	// Rotate sound notification
	// GUIUtility.RotateAroundPivot rotates any GUI created after that line
	private void RotateSoundNotification(Vector2 pivot, Vector2 target, Texture2D icon){
		GUIUtility.RotateAroundPivot(rotAngle, pivot);
		Vector3 cameraScreenPoint = ps.cameraScript.camera.WorldToScreenPoint(transform.position);
		GUI.DrawTexture(new Rect(cameraScreenPoint.x - icon.width/2,
		                         cameraScreenPoint.y - Screen.height/2,
		                         icon.width,icon.height),icon);
		// Get the direction from player to target
		// No idea why we can't use player's position - target's position
		Vector3 lookToTargetDirection = new Vector3(ps.transform.position.x,ps.transform.position.y,transform.position.z) - (Vector3)target;
		rotAngle = 180+Quaternion.LookRotation(lookToTargetDirection, Vector3.forward).eulerAngles.z;
		// Reset the GUI.matrix to stop rotating other GUI elements
		GUI.matrix = Matrix4x4.identity;
	}
}
