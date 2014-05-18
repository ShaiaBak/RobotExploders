using UnityEngine;
using System.Collections;

public class createPlayer : MonoBehaviour {
	public GameObject Player;
	public GameController gameController;
	// Use this for initialization
	void Start () {

		Instantiate (Player, transform.position, Quaternion.identity);
	}

}
