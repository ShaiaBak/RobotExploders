using UnityEngine;
using System.Collections;

// static variables so you can grab them anywhere without needing to find them
public class Resources : MonoBehaviour {

	public static GameObject player1;
	public static GameObject player2;

	void Awake(){
		ResourcesManager rs = GetComponent<ResourcesManager>();
		player1 = rs.player1;
	}

}
