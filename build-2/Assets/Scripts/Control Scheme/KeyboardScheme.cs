using UnityEngine;
using System.Collections;

public class KeyboardScheme : ControlScheme {
	
	public override void SetPlayerNumberControlScheme(int num){
		playerNumberControlScheme = num;
		switch(num){
			case 1:
				// P1 keyboard controls
				horizontal = "P1_Horizontal";
				vertical = "P1_Vertical";
				fireA = "Fire1a";
				fireB = "Fire1b";
				fireC = "Fire1c";
				jump = "P1_Jump";
				enter = "P1_Enter";
				break;

			case 2:
				// P2 keyboard controls
				horizontal = "P2_Horizontal";
				vertical = "P2_Vertical";
				fireA = "Fire2a";
				fireB = "Fire2b";
				fireC = "Fire2c";
				jump = "P2_Jump";
				enter = "P2_Enter";
				break;
		}
	}

}
