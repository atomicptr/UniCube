using UnityEngine;
using System.Collections;

public class TutorialGUI : MonoBehaviour {

	void OnGUI() {
		string text = "";

		if (PlayerController.Instance.InputDevice == PlayerInputDevice.KEYBOARD) {
			text = "Control cursor with Arrow keys, use W, A, S, D, Q and E to interact and 1 or 2 to rotate the cube";
		} else if(PlayerController.Instance.InputDevice == PlayerInputDevice.XBOX360_GAMEPAD){
			text = "Control with DPad or Left analog stick, use A, B, X, Y, RB and LB to interact and RT or LT to rotate the cube";
		}

		GUI.Label(new Rect(10, 10, 200, 200), text);
	}
}
