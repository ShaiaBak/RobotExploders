using UnityEngine;
using System.Collections;

public class Fireflies : MonoBehaviour {
	private float speed = 3.0f;
	public float xScale = 1.0f;
	public float yScale = 1.0f;
	public Vector2 pivot;
	private Vector2 pivotOffset;
	public float phase;

	private bool invert = false;


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
		transform.localPosition = new Vector2(transform.localPosition.x  + Mathf.Sin(phase) * xScale, transform.localPosition.y + Mathf.Cos(phase) * yScale * (invert ? -1 : 1));
		//transform.position.x += Mathf.Sin(phase) * xScale;
		//transform.position.y += Mathf.Sin(phase) * (invert ? -1 : 1)* yScale; 

	}

}
