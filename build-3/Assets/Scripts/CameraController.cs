using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	//This script is added to a Camera
	//The camera will follow whatever is chosen as "player"
	//Smoothing = How fast the camera follows
	//Margin = The amount the "player" can move before the camera reacts 
	public Transform player;

	//Margin and Smoothing values are arbitrary, they can be tweaked
	public Vector2 margin = new Vector2(0.5f,0.5f);
	public Vector2 smoothing = new Vector2(4.0f,4.0f);

	public BoxCollider2D bounds;
	private Vector3 min, max;
	public bool isFollowing { get; set; } 

	//Camera offset for facing directions
	public float xOffset = 0.0f, yOffset = 0.0f;

	// camera zoom variables
	public float smooth = 10;
	public float zoomIn = 5;
	public float zoomOut = 2.25f;

	// Camera shake variables
	private float cameraShakeMagnitude = 0;

	public void Start()
	{
		min = bounds.bounds.min;
		max = bounds.bounds.max;
		isFollowing = true;
		// Attach the camera to the pilot
		player.GetComponent<Pilot>().cameraScript = this;
	}
	public void Update()
	{
		// x and y are the current cameras position
//		if () {
//			xOffset = -xOffset;
//		}
		var x = transform.position.x + xOffset;
		var y = transform.position.y;

		// If the camera is following the "player"
		if (isFollowing)
		{
			if (Mathf.Abs( x - player.position.x ) > margin.x)
				x = Mathf.Lerp (x, player.position.x, smoothing.x * Time.deltaTime);

			if (Mathf.Abs( y - player.position.y ) > margin.y)
				y = Mathf.Lerp (y, player.position.y, smoothing.y * Time.deltaTime);
		}

		//If the camera hits the bounds of the stage
		var cameraHalfWdith = GetComponent<Camera>().orthographicSize * ((float) Screen.width / Screen.height);

		x = Mathf.Clamp(x, min.x + cameraHalfWdith/2, max.x - cameraHalfWdith/2);
		y = Mathf.Clamp(y, min.y + GetComponent<Camera>().orthographicSize, max.y - GetComponent<Camera>().orthographicSize);
		transform.position = new Vector3(x, y, transform.position.z);

		if (player.GetComponent<Pilot>().currentGolem != null) {
			GetComponent<Camera>().orthographicSize = Mathf.Lerp (GetComponent<Camera>().orthographicSize, zoomIn, Time.deltaTime*smooth);
		} else {
			GetComponent<Camera>().orthographicSize = Mathf.Lerp (GetComponent<Camera>().orthographicSize, zoomOut, Time.deltaTime*smooth);
		}
		if(cameraShakeMagnitude > 0){
			HandleCameraShake(x,y);
		}
	}

	private void HandleCameraShake(float x, float y){
		float xShake = Random.Range(-cameraShakeMagnitude,cameraShakeMagnitude);
		float yShake = Random.Range(-cameraShakeMagnitude,cameraShakeMagnitude);
		transform.position = new Vector3(x+xShake, y+yShake, transform.position.z);
	}

	public void ShakeCamera(float magnitude, float length){
		CancelInvoke("StopCameraShake");
		Invoke ("StopCameraShake", length);
		cameraShakeMagnitude = magnitude;
	}

	public void StopCameraShake(){
		cameraShakeMagnitude = 0;
	}
}