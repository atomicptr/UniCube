// Copyright (C) 2014 Christopher Kaster
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software
// and associated documentation files (the "Software"), to deal in the Software without restriction,
// including without limitation the rights to use, copy, modify, merge, publish, distribute,
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software
// is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
// BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
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
