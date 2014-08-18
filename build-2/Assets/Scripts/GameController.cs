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
		// if you press 'R', restart game
		if (Input.GetKeyDown (KeyCode.R) && gameOver) {
			Application.LoadLevel ("01");
		}
		GameOver ();
	}

	public void GameOver() {
		// @TODO: change game over screen to scenes and not just plain text

		// @TODO: add WHO wins to this

		p1 = GameObject.Find("P1").GetComponent<Component>();
		p2 = GameObject.Find("P2").GetComponent<Component>();

		// if player 1 wins
		if (p1.collider2D.enabled == true && p2.collider2D.enabled == false) {
			Application.LoadLevel ("player1wins");
			gameOver = true;
		}
		// if player 2 wins
		else if (p2.collider2D.enabled == true && p1.collider2D.enabled == false) {
			Application.LoadLevel ("player2wins");
			gameOver = true;
		}
		// if both players die
		else if (p2.collider2D.enabled == false && p1.collider2D.enabled == false) {
			Application.LoadLevel ("no1wins");
			gameOver = true;
		}


	}
}

