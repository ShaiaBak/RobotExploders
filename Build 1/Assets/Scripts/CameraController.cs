using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public Transform player;
	public Vector2 margin, smoothing;
	public BoxCollider2D bounds;
	private Vector3 min, max;
	public bool isFollowing { get; set; } 

	public void Start()
	{
		min = bounds.bounds.min;
		max = bounds.bounds.max;
		isFollowing = true;
	}
	public void Update()
	{
		var x = transform.position.x;
		var y = transform.position.y;

		if (isFollowing)
		{
			if (Mathf.Abs( x - player.position.x ) > margin.x)
				x = Mathf.Lerp (x, player.position.x, smoothing.x * Time.deltaTime);

			if (Mathf.Abs( y - player.position.y ) > margin.y)
				y = Mathf.Lerp (y, player.position.y, smoothing.y * Time.deltaTime);
		}

		var cameraHalfWdith = camera.orthographicSize * ((float) Screen.width / Screen.height);

		x = Mathf.Clamp(x, min.x + cameraHalfWdith/2, max.x - cameraHalfWdith/2);
		y = Mathf.Clamp(y, min.y + camera.orthographicSize, max.y - camera.orthographicSize);
		transform.position = new Vector3(x, y, transform.position.z);

	}
}