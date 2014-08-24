using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private bool gameOver;

	private Component p1;
	private Component p2;	

	// Use this for initialization
	void Start () {
		gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		GameOver ();
	}

	public void GameOver() {
		p1 = GameObject.Find("P1").GetComponent<Component>();
		p2 = GameObject.Find("P2").GetComponent<Component>();
		
		// player 1 wins
		if (p1.tag == "Player" && p2.tag == "Dead") {
			Application.LoadLevel ("player1wins");
			gameOver = true;
		}
		// player 2 wins
		if (p2.tag == "Player" && p1.tag == "Dead") {
			Application.LoadLevel ("player2wins");
			gameOver = true;
		}
		// no 1 wins. game broke		
		if (p1.tag == "Dead" && p2.tag == "Dead") {
			Application.LoadLevel ("no1wins");
			gameOver = true;
		}
	}
}

