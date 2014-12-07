using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	public Transform target;
	public Transform camera;

	void Update() {
//		Quaternion newRotation = Quaternion.LookRotation(transform.position - target.position, Vector3.forward);
//		newRotation.x = 0.0f;
//		newRotation.y = 0.0f;
//		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8);
		Quaternion newRotation = Quaternion.LookRotation(transform.position - target.position, Vector3.forward);

		newRotation.x = 0.0f;
		newRotation.y = 0.0f;
		transform.rotation = newRotation;
		print (newRotation.eulerAngles.z);
		transform.position = new Vector3(camera.position.x,camera.position.y,transform.position.z);
	}
}
