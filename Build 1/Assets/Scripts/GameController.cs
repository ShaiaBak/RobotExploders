using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GUIText gameOverText;
	public GUIText restartText;

	private bool gameOver;
	private bool restart;

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
		gameOverText.text = "Game Over";		// @TODO: add WHO wins to this
		gameOver = true;
		restartText.text = "Press 'R' to restart game?";
		restart = true;
	}

}