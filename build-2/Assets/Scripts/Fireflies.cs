using UnityEngine;
using System.Collections;

public class Fireflies : MonoBehaviour {

	private float mSpeed = 3.0f;
	public float mXScale = 1.0f;
	public float mYScale = 1.0f;
	public float rotatex = 0f;
	public float rotatey = 0f;
	public Vector2 pivot;
	private Vector2 pivotOffset;
	public float phase;

	private bool invert = false;


	// Use this for initialization
	void Start () {
		pivot = transform.position;  
		mSpeed = Random.Range(2.0f, 4.0f);
		mXScale = Random.Range(0.5f, 2.0f);
		mYScale = Random.Range(0.5f, 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
		pivotOffset = Vector2.up * 2 * mYScale;
		phase += mSpeed * Time.deltaTime;
		if (phase > Mathf.PI*2) {
			invert = !invert;
			phase -= Mathf.PI*2;
			Debug.Log ("switch");
		}
		if(phase < 0) {
			phase += Mathf.PI*2;
		}
		transform.rotation = Quaternion.Euler(rotatex,rotatey,0);
		transform.position = pivot + (invert ? pivotOffset : Vector2.zero);
		transform.position = new Vector2(transform.position.x  + Mathf.Sin(phase) * mXScale, transform.position.y + Mathf.Cos(phase) * mYScale * (invert ? -1 : 1));
		//transform.position.x += Mathf.Sin(phase) * mXScale;
		//transform.position.y += Mathf.Sin(phase) * (invert ? -1 : 1)* mYScale; 

	}

}
