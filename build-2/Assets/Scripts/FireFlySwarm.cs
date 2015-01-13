using UnityEngine;
using System.Collections;

public class FireFlySwarm : MonoBehaviour {

	public Transform player;
	public GameObject fireFlies;
	public Vector2 margin = new Vector2(0.5f,0.5f);
	public Vector2 smoothing = new Vector2(2.0f,2.0f);

	public bool isFollowing { get; set; }
	// Use this for initialization
	public void Start () {
		isFollowing = false;
		spawnFireflies();
	}
	
	// Update is called once per frame
	public void Update () {
		var x = transform.position.x;
		var y = transform.position.y;
		
		if (isFollowing) {
		
			if (Mathf.Abs( x - player.position.x ) > margin.x)
				x = Mathf.Lerp (x, player.position.x, smoothing.x * Time.deltaTime);

			if (Mathf.Abs( y - player.position.y ) > margin.y)
				y = Mathf.Lerp (y, player.position.y, smoothing.y * Time.deltaTime);
		}	
		transform.position = new Vector2(x, y);
	}

	private void spawnFireflies () {
		for (int i = 0; i < Random.Range(5,10); i++ ) {
			GameObject f = (GameObject) Instantiate(fireFlies, transform.position, Quaternion.identity);
			f.transform.parent = transform;
			f.transform.localPosition = new Vector2(Random.Range(-0.5f,0.5f), Random.Range(-0.5f,0.5f));
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {

		if (other.collider2D.tag == "Player" || other.collider2D.tag == "Golem") {
			player = other.transform;
			isFollowing = true;
		}
	}
}
