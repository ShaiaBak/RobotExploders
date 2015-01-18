using UnityEngine;
using System.Collections;

public class FireFlySwarm : MonoBehaviour {

	public bool isFollowing = false;					//if its following something
	public Transform player;							//the object to follow
	public GameObject fireFlies;						//Little fireflies prefabs
	public Vector2 margin = new Vector2(0.5f,0.5f);
	public Vector2 smoothTo = new Vector2(1.5f,1.5f);
	private Vector2 smoothing = new Vector2(0.0f,0.0f);
	private Vector2 originalLocation = new Vector2(0.0f, 0.0f);
	private float smoothTime = 0.0f;
	public float counter = 0.0f;
	public float droppingFlyTime = 10.0f;
	
	public float returnDurationTime = 0.0f;
	public float returnTime = 3.0f;

	public int spawnNum;

	public void Start () {
		spawnNum = Random.Range(5,10);
		spawnFireflies();
		originalLocation = transform.position;
	}
	
	// Update is called once per frame
	public void Update () {
		var x = transform.position.x;
		var y = transform.position.y;

		if (isFollowing) {
			if (smoothTime < 1) {
				smoothing = Vector2.Lerp(smoothing, smoothTo, smoothTime);
				smoothTime += Time.deltaTime/10;
			}

			if (Mathf.Abs( x - player.position.x ) > margin.x) {
				x = Mathf.Lerp (x, player.position.x, smoothing.x * Time.deltaTime);
			}

			if (Mathf.Abs( y - player.position.y ) > margin.y) {
				y = Mathf.Lerp (y, player.position.y, smoothing.y * Time.deltaTime);
			}
			
			returnDurationTime += Time.deltaTime;

			if (returnDurationTime >= returnTime) {
				isFollowing = false;
				transform.parent = null;
			}



		} else {
			x = Mathf.Lerp (x, originalLocation.x, 0.2f * Time.deltaTime);
			y = Mathf.Lerp (y, originalLocation.y, 0.2f * Time.deltaTime);
		}
		transform.position = new Vector3(x, y,-0.5f);
	}

	//Spawn a random number of fireflies in random positions as a child of the swarm
	private void spawnFireflies () {
		for (int i = 0; i < spawnNum; i++ ) {
			GameObject f = (GameObject) Instantiate(fireFlies, transform.position, Quaternion.identity);
			f.transform.parent = transform;
			f.transform.localPosition = new Vector2(Random.Range(-0.3f,0.3f), Random.Range(-0.3f,0.3f));
		}
	}

	//when player touches, swarm, follow the player
	private void OnTriggerEnter2D(Collider2D other) {
		if (transform.parent == null) {
			if (other.collider2D.tag == "Player" || other.collider2D.tag == "Golem") {
				player = other.transform;
				isFollowing = true;
				//for (int i = 0; i < spawnNum/2; i++) {
				//	transform.GetChild(0).gameObject.GetComponent<Fireflies>().removeFromSwarm();
				//}
			}
		}
	}


}
