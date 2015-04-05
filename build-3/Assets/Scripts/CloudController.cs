using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour {
	public float rangeSpeed;
	
	public float secWait;
	public float totalSec = 35;

	public SpriteRenderer CloudSprite;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("timeToKill", 0, Time.deltaTime);
		transform.Translate(Vector3.forward * Input.GetAxis("Horizontal") * Time.deltaTime);
		rangeSpeed = Random.Range (1, 3);

		CloudSprite = gameObject.GetComponent<SpriteRenderer>();
	}

	public void timeToKill() {
		if (totalSec*Time.deltaTime < secWait*Time.deltaTime){
			totalSec+= Time.deltaTime;
		} else {
			CloudSprite.color = new Color (CloudSprite.color.r, CloudSprite.color.g, CloudSprite.color.b, Mathf.Lerp(CloudSprite.color.a, 0f, Time.deltaTime * 2));
		}
		if(CloudSprite.color.a <= 0.01f) {

			CancelInvoke("timeToKill");
			killCloud();
		}
	}

	public void killCloud() {
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.left * Time.deltaTime * rangeSpeed, Space.World);
	}
}