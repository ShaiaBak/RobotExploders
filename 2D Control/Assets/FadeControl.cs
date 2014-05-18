using UnityEngine;
using System.Collections;

public class FadeControl : MonoBehaviour {
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	public void fadeToBlackStart() {
		anim.SetBool ("Fade", true);
	}

	public void fadeToBlackEnd() {
		anim.SetBool ("Fade", true);
	}
	public void fadeToGameStart() {
		anim.SetBool ("Fade", true);
	}
	public void fadeToGameEnd() {
		anim.SetBool ("Fade", true);
	}
}
