using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	[SerializeField]
	private bool gameIsPaused = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("p")) {
			if (gameIsPaused) {
				Time.timeScale = 1;
				gameIsPaused = false;
			} else {
				Time.timeScale = 0;
				gameIsPaused = true;
			}
        }
	}
}
