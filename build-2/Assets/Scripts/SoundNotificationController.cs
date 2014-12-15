using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundNotificationController : MonoBehaviour {

	private Pilot ps;
	
	private float rotAngle = 0;
	public Texture2D[] icons;
	private static List<Sound> sounds;
	// Test:
	private float timer = 0;
	private Vector2 pivot;

	public class Sound{
		public Vector2 position;
		public int magnitude;
		public float duration;

		public Sound(Vector2 pos, int mag, float dur){
			position = pos;
			magnitude = mag;
			duration = dur + Time.time;
		}
	}

	void Start(){
		// Grab the Pilot script defined in the Camera Controller component
		ps = GetComponent<CameraController>().player.GetComponent<Pilot>();
		sounds = new List<Sound>();
		if(ps.isP1){
			pivot = new Vector2(Screen.width/4,Screen.height/2);
		}else{
			pivot = new Vector2(Screen.width-(Screen.width/4),Screen.height/2);
		}
	}

	void Update(){
		// Remove expired sounds
		timer += Time.deltaTime;
		for(int i=0; i<sounds.Count; i++){
			if(Time.time >= sounds[i].duration){
				sounds.RemoveAt(i);
			}
		}
	}

	void OnGUI(){
		// Show and rotate sound notifications far away enough from the player
		for(int i=0; i<sounds.Count; i++){
			if(Vector2.Distance(transform.position,sounds[i].position) > camera.orthographicSize+.75f){
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
