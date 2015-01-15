using UnityEngine;
using System.Collections;

public class Fireflies : MonoBehaviour {
	private float speed = 3.0f;
	public float xScale = 1.0f;
	public float yScale = 1.0f;
	public Vector2 pivot;
	private Vector2 pivotOffset;
	public Vector2 locFromParent = Vector2.zero;
	public float phase;
	private float xPos = 0;
	private float yPos = 0;
	private bool invert = false;

	public float counter = 0;
	public float droppingFlyTime = 2.0f;
	// Use this for initialization
	void Start () {
		pivot = transform.localPosition;  
		speed = Random.Range(2.0f, 4.0f);
		xScale = Random.Range(0.1f, 1.50f);
		yScale = Random.Range(0.1f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		pivotOffset = Vector2.up * 2 * yScale;
		phase += speed * Time.deltaTime;
		if (phase > Mathf.PI*2) {
			invert = !invert;
			phase -= Mathf.PI*2;
		}
		
		if(phase < 0) {
			phase += Mathf.PI*2;
		}

		transform.localPosition = pivot + (invert ? pivotOffset : Vector2.zero);
		xPos = locFromParent.x + transform.localPosition.x + Mathf.Sin(phase) * xScale;
		yPos = locFromParent.y + transform.localPosition.y + Mathf.Cos(phase) * yScale * (invert ? -1 : 1);
		transform.localPosition = new Vector2(xPos, yPos);
	}

	//Detaches from parent, this is needed because normally when the fly is removed from the swarm
	//it will return to the origin. This function allows it to stay where it is when it is removed
	public void removeFromSwarm() {
		locFromParent = transform.position;
		transform.parent = null;
		transform.localPosition = locFromParent;
	}

}
