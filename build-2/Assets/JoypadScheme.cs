using UnityEngine;
using System.Collections;

public class JoypadScheme : ControlScheme {
	
	public override void SetPlayerNumberControlScheme(int num){
		playerNumberControlScheme = num;
		switch(num){
		case 1:
			// P1 keyboard controls
			horizontal = "J_P1_Horizontal";
			vertical = "J_P1_Vertical";
			fireA = "J_Fire1a";
			fireB = "J_Fire1b";
			fireC = "J_Fire1c";
			jump = "J_P1_Jump";
			enter = "J_P1_Enter";
			break;
			
		case 2:
			// P2 keyboard controls
			horizontal = "K_P2_Horizontal";
			vertical = "K_P2_Vertical";
			fireA = "K_Fire2a";
			fireB = "K_Fire2b";
			fireC = "K_Fire2c";
			jump = "K_P2_Jump";
			enter = "K_P2_Enter";
			break;
		}
	}
	
}
