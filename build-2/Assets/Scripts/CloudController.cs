using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour {
	public float rangeSpeed;
	
	public int secWait;
	public int totalSec = 0;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("timeToKill", 0, 1);
		transform.Translate(Vector3.forward * Input.GetAxis("Horizontal") * Time.deltaTime);
		rangeSpeed = Random.Range (1, 3);
	}

	public void timeToKill() {
		if (totalSec < secWait){
			totalSec++;
		} else {
			CancelInvoke("timeToKill");
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.left * Time.deltaTime * rangeSpeed, Space.World);
	}
}