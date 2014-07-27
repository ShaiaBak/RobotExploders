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
		
	}

	public void GameOver() {
		gameOverText.text = "Game Over";		// @TODO: add WHO wins to this
		gameOver = true;
		restartText.text = "Restart game?";
		restart = true;

		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}

}