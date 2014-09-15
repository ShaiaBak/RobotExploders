using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour {
	public float rangeSpeed;

	// Use this for initialization
	void Start () {
		transform.Translate(Vector3.forward * Input.GetAxis("Horizontal") * Time.deltaTime);
		rangeSpeed = Random.Range (1, 3);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.left * Time.deltaTime * rangeSpeed, Space.World);
	}
}
