using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour {
	public bool left;
	public bool right;

	// Use this for initialization
	void Start () {
		transform.Translate(Vector3.forward * Input.GetAxis("Horizontal") * Time.deltaTime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.left * Time.deltaTime, Space.World);
	}
}
