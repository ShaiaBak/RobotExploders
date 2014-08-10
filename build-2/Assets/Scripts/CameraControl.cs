using UnityEngine;
using System.Collections;

// This is basically how the Super Metroid camera worked. Whichever direction you moved, the camera would move in the
// same direction a multiple of the player's speed. Once the center of the camera moved a certain distance from the
// player, the camera would lock on the player and move the same speed.

// Now, Super Metroid also limited/locked certain axes based on where you were. For instance, if you were in a vertical-only
// location, it would not care about whether the player crossed the left or right boundaries, but functioned normally on
// the top/bottom boundaries. To do this, I have provided the Limit Movement bools and variables to set limits on 
// camera movement. 

// Just set your vertical and horizontal camera limits with the yLimit and xLimit variables respectively, and activate 
// the limit with the limitYMovment and limitXMovement bools. Setting both limits on an axis to the same number will lock
// it on that location on the axis. That way you can have your vertical-only level by setting xLimitLeft and xLimitRight 
// to the same numbers while giving the yLimit variables a range. If you want to take over the camera, change 
// 'activeTracking' to false, then do your thing.

// Get in touch with me, @Jellybit on twitter if you like this, have questions, or find it lacking or weird somehow.
// Use this however you wish. I'd appreciate some sort of thanks, but nothing's required.

public class CameraControl : MonoBehaviour {

	[Tooltip("Drag your player, or whatever object you want the camera to track here. If you need to get the player by name, there's a line in the code you can uncomment.")]
	public GameObject player;

	[Tooltip("This is the area that the player can move within. It will create a window the size of the given units centered on the screen. If you have trouble visualizing it, click on the 'Debug Box' box below and press play to see it.")]
	public Vector2 movementWindowSize = new Vector2(8, 8);
	
	[Tooltip("Should be at least 1.1, as for this to work, the camera has to move faster than the player. Otherwise, it behaves as if the camera is locked to the player.")]
	[Range (1, 10)]
	public float scrollMultiplier = 3.0f;
	
	[Tooltip("If the root of your character is at the feet, you can set this offset to half the player's height to compensate. You can also just use it to keep the box low to the ground or whatever you like.")]
	public Vector2 windowOffset;
	
	[Tooltip("Draws a debug box on the screen while the game is running so you can see the boundaries against the player. Red boundaries mean that they are being ignored due to the following options.")]
	public bool debugBox = false;

	[Tooltip("Use these if you want to limit movement along the corresponding axis. For example, if you want a vertical scrolling level, you can clamp the left and right to the same position so it will not move left or right, and set the vertical top to as high as you want the camera to go, and the vertical bottom to as low as you want the camera to go. The result is that the camera will only move up and down.")]
	public bool limitYMovement = false;
	[Tooltip("Set the highest position you want the camera to be able to go.")]
	public float yLimitTop;
	[Tooltip("Set the lowest position you want the camera to be able to drop.")]
	public float yLimitBottom;

	[Tooltip("Use these if you want to limit movement along the corresponding axis. For example, if you want a horizontal scrolling level, you can clamp the top and bottom to the same position so it will not move up or down, and set the horizontal left to as far left as you want the camera to go, and the horizontal right as far right as you want the camera to go. The result is that the camera will only move left and right.")]
	public bool limitXMovement = false;
	[Tooltip("Set the leftmost position you want the camera to be able to go.")]
	public float xLimitLeft;
	[Tooltip("Set the rightmost position you want the camera to be able to go.")]
	public float xLimitRight;
	
	[HideInInspector] 
	public bool activeTracking = true;

	private Vector3 cameraPosition;
	private Vector3 playerPosition;
	private Vector3 previousPlayerPosition;
	private Rect windowRect;
	
	void Start () {
		
		cameraPosition = transform.position;
		
		//Uncomment the following if you need to get the player by name.
		//player = GameObject.Find ( "Player Name" );
		
		if(player == null)
			Debug.LogError( "You have to let the CameraControl script know which object is your player.");
		
		previousPlayerPosition = player.transform.position;
		
		//These are the root x/y coordinates that we will use to create our boundary rectangle.
		//Starts at the lower left, and takes the offset into account.
		float windowAnchorX = cameraPosition.x - movementWindowSize.x/2 + windowOffset.x;
		float windowAnchorY = cameraPosition.y - movementWindowSize.y/2 + windowOffset.y;
		
		//From our anchor point, we set the size of the window based on the public variable above.
		windowRect = new Rect(windowAnchorX, windowAnchorY, movementWindowSize.x, movementWindowSize.y);
		
		
		//Debug.Log( "Window Rect is " + windowRect );
		
	}
	
	
	void LateUpdate()
	{
		//Updates the camera position based on player location
		CameraUpdate();
		
		// This draws the camera boundary rectangle
		if(debugBox) DrawDebugBox();
	}
	
	void CameraUpdate ()
	{
		playerPosition = player.transform.position;
		
		//Only worry about updating the camera based on player position if the player has actually moved.
		//This is how Super Metroid works except in the case of scene changes.
		//If both axes are to be ignored, we don't bother with any of this crap.
		if ( activeTracking && playerPosition != previousPlayerPosition )
		{
			
			cameraPosition = transform.position;
			
			//Get the distance of the player from the camera.
			Vector3 playerPositionDifference = playerPosition - previousPlayerPosition;
			
			//Move the camera this direction, but faster than the player moves.
			Vector3 multipliedDifference = playerPositionDifference * scrollMultiplier;
			
			cameraPosition += multipliedDifference;

			//updating our movement window root location based on the current camera position
			windowRect.x = cameraPosition.x - movementWindowSize.x/2 + windowOffset.x;
			windowRect.y = cameraPosition.y - movementWindowSize.y/2 + windowOffset.y;
			
			//We may have overshot the boundaries, or the player just may have been moving too fast/popped into another place.
			//This corrects for those cases, and snaps the boundary to the player.
			if(!windowRect.Contains(playerPosition))
			{
				Vector3 positionDifference = playerPosition - cameraPosition;
				positionDifference.x -= windowOffset.x;
				positionDifference.y -= windowOffset.y;
				
				//I made a function to figure out how much to move in order to snap the boundary to the player.
				cameraPosition.x += DifferenceOutOfBounds( positionDifference.x, movementWindowSize.x );

				
				cameraPosition.y += DifferenceOutOfBounds( positionDifference.y, movementWindowSize.y );
				
			}

			if( limitYMovement )
			{
				cameraPosition.y = Mathf.Clamp ( cameraPosition.y, yLimitBottom, yLimitTop );
			}

			if( limitXMovement )
			{
				cameraPosition.x = Mathf.Clamp ( cameraPosition.x, xLimitLeft, xLimitRight );
			}

			transform.position = cameraPosition;
			
		}
		
		previousPlayerPosition = playerPosition;
	}
	
	
	//This takes the player distance from the camera, and subtracks the boundary distance to find how far the
	//player has overshot things.
	static float DifferenceOutOfBounds ( float differenceAxis, float windowAxis )
	{
		float difference;
		
		//We're seeing here if the player has overshot it at all on this axis. If not, we just set the difference to zero.
		//This is because if we subtract the boundary distance when the player isn't far from the camera, we'll needlessly
		//compensate, and screw up the camera.
		if (Mathf.Abs (differenceAxis) <= windowAxis/2)
			difference = 0f;
		//And if the player legit overshot the boundary, we subtract the boundary from the distance.
		else
			difference = differenceAxis - (windowAxis/2) * Mathf.Sign (differenceAxis);
		
		
		//Returns something if the overshot was legit, and zero if it wasn't.
		return difference;
		
	}
	
	void DrawDebugBox()
	{
		//This will draw the boundaries you are tracking in green, and boundaries you are ignoring in red.
		windowRect.x = cameraPosition.x - movementWindowSize.x/2 + windowOffset.x;
		windowRect.y = cameraPosition.y - movementWindowSize.y/2 + windowOffset.y;

		Vector3 cameraPos = transform.position;

		Color xColorA;
		Color xColorB;
		Color yColorA;
		Color yColorB;
		
		if(!activeTracking || limitXMovement && cameraPos.x <= xLimitLeft )
			xColorA = Color.red;
		else
			xColorA = Color.green;

		if(!activeTracking || limitXMovement && cameraPos.x >= xLimitRight )
			xColorB = Color.red;
		else
			xColorB = Color.green;

		if(!activeTracking || limitYMovement && cameraPos.y <= yLimitBottom )
			yColorA = Color.red;
		else
			yColorA = Color.green;

		if(!activeTracking || limitYMovement && cameraPos.y >= yLimitTop )
			yColorB = Color.red;
		else
			yColorB = Color.green;
		
		Vector3 actualWindowCorner1 = new Vector3( windowRect.xMin, windowRect.yMin, 0 );
		Vector3 actualWindowCorner2 = new Vector3( windowRect.xMax, windowRect.yMin, 0 );
		Vector3 actualWindowCorner3 = new Vector3( windowRect.xMax, windowRect.yMax, 0 );
		Vector3 actualWindowCorner4 = new Vector3( windowRect.xMin, windowRect.yMax, 0 );
		
		Debug.DrawLine (actualWindowCorner1, actualWindowCorner2, yColorB);
		Debug.DrawLine (actualWindowCorner2, actualWindowCorner3, xColorB);
		Debug.DrawLine (actualWindowCorner3, actualWindowCorner4, yColorA);
		Debug.DrawLine (actualWindowCorner4, actualWindowCorner1, xColorA);
	}

	//Use these from other scripts/objects that want to communicate level limits to the camera.
	public void ActivateYLimits (float bottomLimit, float topLimit )
	{
		yLimitBottom = bottomLimit;
		yLimitTop = topLimit;
		limitYMovement = true;
	}

	public void ActivateXLimits (float leftLimit, float rightLimit )
	{
		xLimitLeft = leftLimit;
		xLimitRight = rightLimit;
		limitXMovement = true;
	}

	public void DeactivateYLimits ()
	{
		limitYMovement = false;
	}
	
	public void DeactivateXLimits ()
	{
		limitXMovement = false;
	}

	public void DeactivateLimits ()
	{
		limitYMovement = false;
		limitXMovement = false;
	}
	
}