﻿using UnityEngine;
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


	public void Start()
	{
		min = bounds.bounds.min;
		max = bounds.bounds.max;
		isFollowing = true;
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
		var cameraHalfWdith = camera.orthographicSize * ((float) Screen.width / Screen.height);

		x = Mathf.Clamp(x, min.x + cameraHalfWdith/2, max.x - cameraHalfWdith/2);
		y = Mathf.Clamp(y, min.y + camera.orthographicSize, max.y - camera.orthographicSize);
		transform.position = new Vector3(x, y, transform.position.z);

	}
}