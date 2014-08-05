using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GUIText gameOverText;
	public GUIText restartText;

	private bool gameOver;
	private bool restart;

	private Component p1;
	private Component p2;	

	// Use this for initialization
	void Start () {
		gameOver = false;
		restart = false;

		gameOverText.text = "";
		restartText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		// if you press 'R', restart game
		if (Input.GetKeyDown (KeyCode.R) && gameOver) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}

	public void GameOver() {
		// @TODO: change game over screen to scenes and not just plain text

		p1 = GameObject.Find("P1").GetComponent<Component>();
		p2 = GameObject.Find("P2").GetComponent<Component>();

		// if player 1 wins
		if (p1.collider2D.enabled == true && p2.collider2D.enabled == false) {
			gameOverText.text = "Game Over Player 1 won... i guess";
		}
		// if player 2 wins
		if (p2.collider2D.enabled == true && p1.collider2D.enabled == false) {
			gameOverText.text = "Game Over - PLAYER 2 JUST RAN OUT OF BUBBLE GUM";
		}
		// if both players die
		if (p2.collider2D.enabled == false && p1.collider2D.enabled == false) {
			gameOverText.text = "Game Over - You just broke the game- congrats.";
		}

		// @TODO: add WHO wins to this
		gameOver = true;
		restartText.text = "Press 'R' to restart game";
		restart = true;
	}
}