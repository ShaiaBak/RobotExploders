using UnityEngine;
using System.Collections;

public class enterUI : MonoBehaviour {
	public Golem parentClass;
	public Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		parentClass = transform.parent.GetComponent<Golem>();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool ("enterPress", parentClass.enterGolemPress);
		// anim.SetFloat ("finishEnter", parentClass.enterGolemPress);
	}
}