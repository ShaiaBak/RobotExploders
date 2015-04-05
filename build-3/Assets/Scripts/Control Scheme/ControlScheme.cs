using UnityEngine;
using System.Collections;

public abstract class ControlScheme : MonoBehaviour {

	public string horizontal;
	public string vertical;
	public string fireA;
	public string fireB;
	public string fireC;
	public string jump;
	public string enter;
	public int playerNumberControlScheme = 0;

	public abstract void SetPlayerNumberControlScheme(int num);

}
