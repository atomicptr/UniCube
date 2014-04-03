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
using InControl;

public class CameraRotator : MonoBehaviour {

	private float speed = 10f;

	private GameObject pivot;

	void Start() {
		pivot = new GameObject("camera pivot");
	}

	void LateUpdate () {
		Transform target = pivot.transform;

		float mouseDeltaX = 0f;
		float mouseDeltaY = 0f;

		mouseDeltaX = InputManager.ActiveDevice.RightStickX.Value * speed;
		mouseDeltaY = InputManager.ActiveDevice.RightStickY.Value * speed;

		Quaternion rotation = Quaternion.Euler(mouseDeltaY, mouseDeltaX, 0);
		transform.rotation = rotation;

		//Move the camera
		Vector3 position = rotation * this.transform.position + target.position;
		transform.position = position;

		// look at target
		transform.LookAt (target);
	}
}
