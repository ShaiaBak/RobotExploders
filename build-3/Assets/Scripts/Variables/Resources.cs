using UnityEngine;
using System.Collections;

// static variables so you can grab them anywhere without needing to find them
public class Resources : MonoBehaviour {

	public static GameObject player1;
	public static GameObject player2;
	public static Pilot pilotScript1;
	public static Pilot pilotScript2;
	public static GameObject projectilePrefab;
	public static GameObject golemProjectile;

	void Awake(){
		ResourcesManager rs = GetComponent<ResourcesManager>();
		player1 = rs.player1;
		player2 = rs.player2;
		pilotScript1 = rs.player1.GetComponent<Pilot>();
		pilotScript2 = rs.player2.GetComponent<Pilot>();
		projectilePrefab = rs.projectilePrefab;
		golemProjectile = rs.golemProjectile;
	}

}
